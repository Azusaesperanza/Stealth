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
    [SerializeField] bool tracking = false;
    bool playerChaseFlg;
    bool soundChaseFlg;
    NavMeshAgent agent;
    Vector3 playerPos;
    //  プレイヤー
    [SerializeField] GameObject player;
    GameObject sound;
    Rigidbody rigidbody;
    //  それぞれの警察の最初のポジション
    //Transform policeFirstPosition;
    Vector3 policeFirstPosition;
    //  プレイヤーと警察の離れてる距離
    float playerDistance;
    float soundDistance;
    void Start()
    {
        Init();
        GotoNextPoint();
    }

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
        Debug.Log("次の目標地点設定" + destPoint);
    }

    void Update()
    {
        AutoMove();
    }

    void Init()
    {
        policeFirstPosition = gameObject.transform.position;
        agent = GetComponent<NavMeshAgent>();

        //  autoBrakingを無効にすると目標地点に近づいても速度を落とさない
        agent.autoBraking = false;
        rigidbody = GetComponent<Rigidbody>();
        playerChaseFlg = true;
    }

    void AutoMove()
    {
        //  Playerとの距離を測る
        playerPos = player.transform.position;
        playerDistance = Vector3.Distance(this.transform.position, playerPos);
        if (tracking)
        {
            if (soundChaseFlg == false)
            {
                //  プレイヤーを追いかけてる時
                if (playerChaseFlg)
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
                    soundChaseFlg = false;
                    Debug.Log("サウンドまでの移動解除");
                }
            }
        }
        else
        {
            if (playerChaseFlg)
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

                if (soundChaseFlg)
                {
                    tracking = true;
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        //  trackingRangeの範囲を赤いワイヤーフレームで示す
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, trackingRange);

        //  quitRangeの範囲を青いワイヤーフレームで示す
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, quitRange);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Sound")
        {
            sound = col.gameObject;
            soundChaseFlg = true;
            Debug.Log("サウンド当たり判定に当たった");
        }
    }
}





//using UnityEngine;
//using UnityEngine.AI;

//[RequireComponent(typeof(NavMeshAgent))]
//public class EnemyController : MonoBehaviour
//{
//    //  移動経路のTransformを格納する配列
//    [SerializeField] Transform[] points;
//    [SerializeField] int destPoint = 0;
//    [SerializeField] float trackingRange = 3f;
//    [SerializeField] float quitRange = 5f;
//    [SerializeField] bool tracking = false;
//    bool playerChaseFlg;
//    bool soundChaseFlg;
//    NavMeshAgent agent;
//    Vector3 playerPos;
//    //  プレイヤー
//    [SerializeField] GameObject player;
//    GameObject sound;
//    Rigidbody rigidbody;
//    //  それぞれの警察の最初のポジション
//    //Transform policeFirstPosition;
//    Vector3 policeFirstPosition;
//    //  プレイヤーと警察の離れてる距離
//    float playerDistance;
//    float soundDistance;
//    void Start()
//    {
//        Init();
//        GotoNextPoint();
//    }

//    void GotoNextPoint()
//    {
//        //  地点が何も設定されてないときに返す
//        if (points.Length == 0)
//            return;

//        //  エージェントが現在設定された目標地点に行くように設定
//        agent.destination = points[destPoint].position;

//        //  配列内の次の位置を目標地点に設定し、
//        //  必要なら出発地点に戻す
//        destPoint = (destPoint + 1) % points.Length;
//        Debug.Log("次の目標地点設定" + destPoint);
//    }

//    void Update()
//    {
//        AutoMove();
//    }

//    void Init()
//    {
//        policeFirstPosition = gameObject.transform.position;
//        agent = GetComponent<NavMeshAgent>();

//        //  autoBrakingを無効にすると目標地点に近づいても速度を落とさない
//        agent.autoBraking = false;
//        rigidbody = GetComponent<Rigidbody>();
//        playerChaseFlg = true;
//    }

//    void AutoMove()
//    {
//        //  Playerとの距離を測る
//        playerPos = player.transform.position;
//        playerDistance = Vector3.Distance(this.transform.position, playerPos);
//        if (tracking)
//        {
//            if (soundChaseFlg == false)
//            {
//                //  プレイヤーを追いかけてる時
//                if (playerChaseFlg)
//                {
//                    //Debug.Log("プレイヤーを追いかけてる");
//                    //  追跡の時、quitRangeより距離が離れたら中止
//                    if (playerDistance > quitRange)
//                    {
//                        tracking = false;
//                    }

//                    //  Playerを目標とする
//                    agent.destination = playerPos;
//                }
//            }
//            else
//            {
//                soundDistance = Vector3.Distance(this.transform.position, sound.transform.position);
//                agent.destination = sound.transform.position;
//                if (soundDistance < 1)
//                {
//                    tracking = false;
//                    soundChaseFlg = false;
//                    Debug.Log("サウンドまでの移動解除");
//                }
//            }
//        }
//        else
//        {
//            if (playerChaseFlg)
//            {
//                //  PlayerがtrackingRangeより近づいたら追跡開始
//                if (playerDistance < trackingRange)
//                {
//                    tracking = true;
//                }

//                //  エージェントが現目標地点に近づいてきたら
//                //  次の目標地点を選択する
//                if (!agent.pathPending && agent.remainingDistance < 0.5f)
//                {
//                    GotoNextPoint();
//                }

//                if (soundChaseFlg)
//                {
//                    tracking = true;
//                }
//            }
//        }
//    }

//    void OnDrawGizmosSelected()
//    {
//        //  trackingRangeの範囲を赤いワイヤーフレームで示す
//        Gizmos.color = Color.red;
//        Gizmos.DrawWireSphere(transform.position, trackingRange);

//        //  quitRangeの範囲を青いワイヤーフレームで示す
//        Gizmos.color = Color.blue;
//        Gizmos.DrawWireSphere(transform.position, quitRange);
//    }

//    void OnTriggerEnter(Collider col)
//    {
//        if (col.gameObject.tag == "Sound")
//        {
//            sound = col.gameObject;
//            soundChaseFlg = true;
//            Debug.Log("サウンド当たり判定に当たった");
//        }
//    }
//}
