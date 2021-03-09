using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
<<<<<<< HEAD
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
=======
    //  移動経路のTransformを格納する配列
    [SerializeField] Transform[] points;
    [SerializeField] int destPoint = 0;
    [SerializeField] float trackingRange = 3f;
    [SerializeField] float quitRange = 5f;
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
>>>>>>> 003f17bb6eab16a8830ec2456a479a4f782a7582
    float soundDistance;
    float surveyTimer;
    const float SURVEYMAXTIMER = 4f;
    Animator animator;
    void Start()
    {
        Init();
        GotoNextPoint();
    }

    void GotoNextPoint()
    {
        //  地点が何も設定されてないときに返す
        //  配列内の次の位置を目標地点に設定し、
        //  必要なら出発地点に戻す
        destPoint = (destPoint + 1) % points.Length;
        Debug.Log("次の目標地点設定" + destPoint);
<<<<<<< HEAD
        //Debug.Log("次の目標地点設定" + destPoint);
        if (gameObject.name == "GrandFather1")
        {
            if ((destPoint == 4 || destPoint == 6 || destPoint == 8 || destPoint == 11 || destPoint == 16))
            {
                enemyState = EnemyState.SURVEY;
            }
        }
=======
>>>>>>> 003f17bb6eab16a8830ec2456a479a4f782a7582
    }

    void Update()
    {
        if (surveyFlag == false)
        {
<<<<<<< HEAD
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
=======
            AutoMove();
>>>>>>> 003f17bb6eab16a8830ec2456a479a4f782a7582
        }
        Survey();
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
        animator = GetComponent<Animator>();
    }

    void AutoMove()
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
<<<<<<< HEAD
            tracking = false;
        }
        else
        if (agent.isStopped)
=======
        }
        else
>>>>>>> 003f17bb6eab16a8830ec2456a479a4f782a7582
        {
            if (playerChaseFlag)
            {
                //  PlayerがtrackingRangeより近づいたら追跡開始
                if (playerDistance < trackingRange)
                {
                    tracking = true;
                }
<<<<<<< HEAD

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
=======

                //  エージェントが現目標地点に近づいてきたら
                //  次の目標地点を選択する
                if (!agent.pathPending && agent.remainingDistance < 0.5f)
                {
                    GotoNextPoint();
                }

                if (soundChaseFlag)
                {
                    tracking = true;
                }
            }
>>>>>>> 003f17bb6eab16a8830ec2456a479a4f782a7582
        }
    }

    /// </summary>
    void Survey()
    {
        if (gameObject.name == "GrandFather1")
        {
            if ((destPoint == 4 || destPoint == 6 || destPoint == 10) && surveyFlag == false)
<<<<<<< HEAD
                surveyTimer -= Time.deltaTime;
            if (surveyTimer <= 0f)
=======
>>>>>>> 003f17bb6eab16a8830ec2456a479a4f782a7582
            {
                surveyFlag = true;
                agent.isStopped = true;
                Debug.Log("周辺を見渡す");
                animator.SetBool("Survey", true);
<<<<<<< HEAD
                enemyState = EnemyState.WALK;
                surveyTimer = SURVEYMAXTIMER;
                agent.destination = points[destPoint].position;
=======
>>>>>>> 003f17bb6eab16a8830ec2456a479a4f782a7582
            }
            if (surveyFlag)
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
<<<<<<< HEAD
                enemyState = EnemyState.WALK;
                surveyTimer = SURVEYMAXTIMER;
                agent.isStopped = false;
                GotoNextPoint();
=======
>>>>>>> 003f17bb6eab16a8830ec2456a479a4f782a7582
            }
        }
        //else if (gameObject.name == "GrandFather2")
        //{
<<<<<<< HEAD
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
=======

        //}
    }

    void OnDrawGizmosSelected()
>>>>>>> 003f17bb6eab16a8830ec2456a479a4f782a7582
    {
        //  trackingRangeの範囲を赤いワイヤーフレームで示す
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, trackingRange);
<<<<<<< HEAD
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
=======

        //  quitRangeの範囲を青いワイヤーフレームで示す
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, quitRange);
>>>>>>> 003f17bb6eab16a8830ec2456a479a4f782a7582
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Sound")
        {
            sound = col.gameObject;
            soundChaseFlag = true;
<<<<<<< HEAD
            soundObject = col.gameObject;
            enemyState = EnemyState.SOUNDCHASE;
            tracking = true;
=======
>>>>>>> 003f17bb6eab16a8830ec2456a479a4f782a7582
            Debug.Log("サウンド当たり判定に当たった");
        }
    }
}
