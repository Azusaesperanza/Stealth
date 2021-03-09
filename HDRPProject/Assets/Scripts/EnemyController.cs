using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    //  移動経路のTransformを格納する配列
    [SerializeField] Transform[] points;
    [SerializeField] int destPoint = 0;
    [SerializeField] float trackingRange = 3f;
    [SerializeField] float quitRange = 5f;
    [SerializeField] float searchAngle = 85f;  //  プレイヤーを見つける視界の角度(70の場合140度)
    [SerializeField] float rayLength = 8f;           //  レイの長さ
    [SerializeField] bool tracking = false;
    bool playerChaseFlag;
    bool soundChaseFlag;
    bool surveyFlag;    //  周辺を見渡すときのフラグ
    NavMeshAgent agent;
    Vector3 playerPos;
    //  プレイヤー
    [SerializeField] GameObject player;
    GameObject sound;
    //  それぞれの警察の最初のポジション
    //Transform policeFirstPosition;
    Vector3 policeFirstPosition;
    //  プレイヤーと警察の離れてる距離
    float playerDistance;
    //  移動経路のTransformを格納する配列
    [SerializeField] Transform points;
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
        //  配列内の次の位置を目標地点に設定し、
        //  必要なら出発地点に戻す
        destPoint = (destPoint + 1) % points.Length;
        Debug.Log("次の目標地点設定" + destPoint);
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
        if (surveyFlag == false)
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
    }

    void Init()
    {
        policeFirstPosition = gameObject.transform.position;
        agent = GetComponent<NavMeshAgent>();

        //  autoBrakingを無効にすると目標地点に近づいても速度を落とさない
        agent.autoBraking = false;
        playerChaseFlag = true;
        surveyFlag = false;
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
        //  Playerとの距離を測る
        playerPos = player.transform.position;
        playerDistance = Vector3.Distance(this.transform.position, playerPos);
        if (tracking)
        {
            if (soundChaseFlag == false)
            {
                //  プレイヤーを追いかけてる時
                if (playerChaseFlag)
                {
                    //Debug.Log("プレイヤーを追いかけてる");
                    //  追跡の時、quitRangeより距離が離れたら中止
                    if (playerDistance > quitRange)
                    {
                        tracking = false;
                    }

                    //  Playerを目標とする
                    agent.destination = playerPos;
                }
            }
            else
            {
                soundDistance = Vector3.Distance(this.transform.position, sound.transform.position);
                agent.destination = sound.transform.position;
                if (soundDistance < 2)
                {
                    tracking = false;
                    soundChaseFlag = false;
                    Debug.Log("サウンドまでの移動解除");
                }
            }
            tracking = false;
        }
        else
        if (agent.isStopped)
        {
            if (playerChaseFlag)
            {
                //  PlayerがtrackingRangeより近づいたら追跡開始
                if (playerDistance < trackingRange)
                {
                    tracking = true;
                }

                //  エージェントが現目標地点に近づいてきたら
                //  次の目標地点を選択する
                if (!agent.pathPending && agent.remainingDistance < 0.5f)
                {
                    GotoNextPoint();
                }
                agent.isStopped = false;
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.walk a"))
            {
                animator.SetBool("Survey", false);
            }

            if (soundChaseFlag)
            {
                tracking = true;
            }
        }
        //  エージェントが現目標地点に近づいてきたら
        //  次の目標地点を選択する
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GotoNextPoint();
        }
    }

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
            if ((destPoint == 4 || destPoint == 6 || destPoint == 10) && surveyFlag == false)
                surveyTimer -= Time.deltaTime;
            if (surveyTimer <= 0f)
            {
                surveyFlag = true;
                agent.isStopped = true;
                Debug.Log("周辺を見渡す");
                animator.SetBool("Survey", true);
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
                surveyTimer -= Time.deltaTime;
                if (surveyTimer <= 0f)
                {
                    surveyFlag = false;
                    surveyTimer = SURVEYMAXTIMER;
                    agent.isStopped = false;
                    GotoNextPoint();
                    Debug.Log("徘徊再開");
                    animator.SetBool("Survey", false);
                }
                enemyState = EnemyState.WALK;
                surveyTimer = SURVEYMAXTIMER;
                agent.isStopped = false;
                GotoNextPoint();
            }
        }
        //else if (gameObject.name == "GrandFather2")
        //{
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

        //}
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
        //  trackingRangeの範囲を赤いワイヤーフレームで示す
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, trackingRange);
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
            sound = col.gameObject;
            soundChaseFlag = true;
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
