using UnityEngine;
using UnityEngine.AI;

public class FindPlayer : MonoBehaviour
{
    NavMeshAgent agent;
    RaycastHit hit;
    GameObject player;
    [SerializeField] float searchAngle = 130f;
    [SerializeField] float rayLength;
    float playerChaseTimer;  //  プレイヤーが視界外に行っても追いかける時間
    const float PLAYERCHASEMAXTIME = 4f;
    bool playerChaseFlag;

    void Start()
    {
        Init();
    }

    void Init()
    {
        agent = GetComponent<NavMeshAgent>();
        playerChaseTimer = PLAYERCHASEMAXTIME;
        playerChaseFlag = false;
    }

    void Update()
    {
        if (playerChaseTimer > 0 && playerChaseFlag)
        {
            agent.destination = player.transform.position;
            Debug.Log("プレイヤーを追いかけている");
            playerChaseTimer -= Time.deltaTime;
        }
        else if (playerChaseTimer <= 0 && playerChaseFlag)
        {
            playerChaseFlag = false;
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Vector3 distance = col.transform.position - transform.position; //  自分と敵の距離
            Vector3 direction = distance.normalized;
            var angle = Vector3.Angle(transform.forward, distance); //  敵から見たプレイヤーの方向
            if (angle <= searchAngle)
            {
                Ray ray = new Ray(transform.position, direction);
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    Debug.DrawRay(transform.position, direction * rayLength, Color.red);
                    //Debug.Log("レイに当たっているオブジェクト名:" + hit.collider.gameObject.name);
                    if (hit.collider.gameObject.tag == "Player")
                    {
                        //Debug.Log("敵に見つかった");
                        playerChaseTimer = PLAYERCHASEMAXTIME;
                        playerChaseFlag = true;
                        player = col.gameObject;
                    }
                }
            }
        }
    }
}
