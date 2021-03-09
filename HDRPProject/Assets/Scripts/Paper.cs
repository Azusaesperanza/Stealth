using UnityEngine;

public class Paper : MonoBehaviour
{
    [SerializeField] GameObject paperSoundObject;
    [SerializeField] float expansionSpeed;
    [SerializeField] float expansionTimer;
    float maxExpansionTimer;
    bool hitFlag;
    void Start()
    {
        Init();
    }

    void Update()
    {
        CollisionExpansion();
    }

    void Init()
    {
        //paperSoundObject = GetComponentInChildren<GameObject>();
        Debug.Log(paperSoundObject.name);
        hitFlag = false;
        maxExpansionTimer = expansionTimer;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player" && hitFlag == false)
        {
            hitFlag = true;
            Debug.Log("新聞紙を踏んだ");
        }
    }

    void CollisionExpansion()
    {
        if (hitFlag)
        {
            paperSoundObject.transform.localScale += new Vector3(expansionSpeed, expansionSpeed, expansionSpeed);
            expansionTimer -= Time.deltaTime;
            if (expansionTimer <= 0)
            {
                expansionTimer = maxExpansionTimer;
                hitFlag = false;
                paperSoundObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            }
        }
    }
}
