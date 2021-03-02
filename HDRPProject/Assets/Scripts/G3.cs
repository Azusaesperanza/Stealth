using UnityEngine;
using UnityEngine.AI;

public class G3 : MonoBehaviour
{
    [SerializeField] Transform[] movePoints;
    int destPoint = 0;
    NavMeshAgent agent;

    void Start()
    {
        Init();
    }

    void Update()
    {
        //  エージェントが目標地点に近づいてきたら、次の目標地点を選択
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GotoNextPoint();
        }
    }

    void Init()
    {
        agent = GetComponent<NavMeshAgent>();

        //  autoBrakingを無効にすると、目標地点の間を継続的に移動する(エージェントは目標地点に近づいても速度を落とさない)
        agent.autoBraking = false;

        GotoNextPoint();
    }

    void GotoNextPoint()
    {
        //  地点が何も設定されてないときに返す
        if (movePoints.Length == 0)
        {
            return;
        }

        //  エージェントが現在設定された目標地点に行くように設定
        agent.destination = movePoints[destPoint].position;

        //  配列内の次の位置を目標地点に設定して、必要なら出発地点に戻る
        destPoint = (destPoint + 1) % movePoints.Length;
    }
}
