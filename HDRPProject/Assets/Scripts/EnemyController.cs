using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    [SerializeField] int destPoint = 0;
    [SerializeField] float searchAngle = 85f;  //  プレイヤーを見つける視界の角度(70の場合140度)
    [SerializeField] float rayLength = 8f;           //  レイの長さ
    [SerializeField] bool tracking = false;
    //  移動経路のTransformを格納する配列
    [SerializeField] Transform[] points;
    float playerDistance;   //  プレイヤーと敵の離れている距離
    float soundDistance;
    float surveyTimer;
    const float SURVEYMAXTIMER = 4f;
    float playerChaseTimer;  //  プレイヤーが視界外に行っても追いかける時間
    const float PLAYERCHASEMAXTIME = 4f;

    GameObject player;  //  プレイヤー
    GameObject soundObject;   //  音の鳴るオブジェクト
    Animator animator;
    NavMeshAgent agent;
    RaycastHit hit;
    EnemyState enemyState;

    /// <summary>
    /// 敵の状態
    /// </summary>
    enum EnemyState
    {
        WALK,
        SURVEY,
        PLAYERCHASE,
        SOUNDCHASE,
    }

    void Start()
    {
        Init();
        GotoNextPoint();
    }

    /// <summary>
    /// 次の目的地を設定する処理
    /// </summary>
    void GotoNextPoint()
    {
        //  地点が何も設定されてないときに返す
        if (points.Length == 0)
            return;

        //  エージェントが現在設定された目標地点に行くように設定
        agent.destination = points[destPoint].position;

        //  配列内の次の位置を目標地点に設定し、
        //  必要なら出発地点に戻す
        destPoint = (destPoint + 1) % points.Length;
        //Debug.Log("次の目標地点設定" + destPoint);
        if (gameObject.name == "GrandFather1")
        {
            if ((destPoint == 4 || destPoint == 6 || destPoint == 8 || destPoint == 11 || destPoint == 16))
            {
                enemyState = EnemyState.SURVEY;
            }
        }
    }

    void Update()
    {
        switch (enemyState)
        {
            case EnemyState.WALK:
                Walk();
                break;
            case EnemyState.SURVEY:
                Survey();
                break;
            case EnemyState.PLAYERCHASE:
                PlayerChase();
                break;
            case EnemyState.SOUNDCHASE:
                SoundChase();
                break;
        }
    }

    void Init()
    {
        surveyTimer = SURVEYMAXTIMER;
        playerChaseTimer = PLAYERCHASEMAXTIME;
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;        //  autoBrakingを無効にすると目標地点に近づいても速度を落とさない
        enemyState = EnemyState.WALK;
    }

    /// <summary>
    /// 通常パターンの処理
    /// </summary>
    void Walk()
    {
        if (tracking)
        {
            tracking = false;
        }
        if (agent.isStopped)
        {
            agent.isStopped = false;
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.walk a"))
        {
            animator.SetBool("Survey", false);
        }

        //  エージェントが現目標地点に近づいてきたら
        //  次の目標地点を選択する
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GotoNextPoint();
        }
    }

    /// <summary>
    /// 周辺を見渡す処理
    /// </summary>
    void Survey()
    {
        if (tracking)
        {
            tracking = false;
        }
        if (agent.isStopped == false)
        {
            agent.isStopped = true;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.look for a"))
        {
            animator.SetBool("Survey", true);
        }

        if (gameObject.name == "GrandFather1")
        {
            surveyTimer -= Time.deltaTime;
            if (surveyTimer <= 0f)
            {
                enemyState = EnemyState.WALK;
                surveyTimer = SURVEYMAXTIMER;
                agent.destination = points[destPoint].position;
            }
        }
        else if (gameObject.name == "GrandFather2")
        {
            surveyTimer -= Time.deltaTime;
            if (surveyTimer <= 0f)
            {
                enemyState = EnemyState.WALK;
                surveyTimer = SURVEYMAXTIMER;
                agent.isStopped = false;
                GotoNextPoint();
            }
        }
    }

    /// <summary>
    /// プレイヤーを見つけて追いかける処理
    /// </summary>
    void PlayerChase()
    {
        if (tracking == false)
        {
            tracking = true;
        }
        if (agent.isStopped)
        {
            agent.isStopped = false;
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.walk a"))
        {
            animator.SetBool("Survey", false);
        }

        if (playerChaseTimer >= 0)
        {
            //  プレイヤーを目標とする
            agent.destination = player.transform.position;
            playerChaseTimer -= Time.deltaTime;
        }
        else
        {
            enemyState = EnemyState.SURVEY;
            playerChaseTimer = PLAYERCHASEMAXTIME;
        }
    }

    /// <summary>
    /// 音の方に向かう処理
    /// </summary>
    void SoundChase()
    {
        if (tracking == false)
        {
            tracking = true;
        }
        if (agent.isStopped)
        {
            agent.isStopped = false;
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.walk a"))
        {
            animator.SetBool("Survey", false);
        }


    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "SoundCollider")
        {
            soundObject = col.gameObject;
            enemyState = EnemyState.SOUNDCHASE;
            tracking = true;
            Debug.Log("サウンド当たり判定に当たった");
            agent.isStopped = false;
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Vector3 distance = col.transform.position - transform.position; //  プレイヤーと敵の距離
            Vector3 direction = distance.normalized;
            var angle = Vector3.Angle(transform.forward, distance); //  敵から見たプレイヤーの方向
            if (angle <= searchAngle)
            {
                Debug.Log("プレイヤーが視界内に入った(距離関係なく)");
                Ray ray = new Ray(transform.position, direction);
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    Debug.DrawRay(transform.position, direction * rayLength, Color.red);
                    //Debug.Log("レイに当たっているオブジェクト名:" + hit.collider.gameObject.name);
                    if (hit.collider.gameObject.tag == "Player")
                    {
                        playerChaseTimer = PLAYERCHASEMAXTIME;
                        enemyState = EnemyState.PLAYERCHASE;
                        player = col.gameObject;
                    }
                }
            }
        }
    }
}
