using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playercont : MonoBehaviour
{
    const float SpecialPower = 5f;
    public float speed = 10.0f;
    Rigidbody rb;
    [SerializeField]
    private GameObject player;
    Animator Animator;
    [SerializeField]
    float MouseSensivity;
    [SerializeField]
    GameObject camera;
    public bool Attacking;
    int Level;
    [SerializeField]
    float playerHP;
    int MaxHp = 10;
    float NextLevel;
    //[SerializeField]    
    //HPbar hpbar;
    GameObject Menu;
    //[SerializeField]
    //Expget expget;
    float Exp;
    [SerializeField,NonEditable]
    float ChargeTime;
    float Animationtime;
    public float i_frame;
    float f_SpecialAttack;
    [SerializeField]
    GameObject SlashParticle;
    [SerializeField,NonEditable]
    bool SpecialMode;

    public enum Attack
    {
        Normal,
        Damage,
        Attack,
        Shop,
        Freeze
    }
    public Attack stats = Attack.Normal;
    void Start()
    {
        Animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;

    }

    private void SceneManager_sceneLoaded(UnityEngine.SceneManagement.Scene arg0, LoadSceneMode arg1)
    {
        player.transform.position = GameObject.FindGameObjectWithTag("Spawn").transform.position;
        player.transform.rotation = GameObject.FindGameObjectWithTag("Spawn").transform.rotation;


    }
    
    void Awake()
    {
        //DontDestroyOnLoad(gameObject);
    }
    void Update()
    {


    }

    void FixedUpdate()
    {
        if (stats == Attack.Normal)
        {
            //float Mouse_X = Input.GetAxisRaw("JoyStickRightX");
            //float Mouse_Y = Input.GetAxisRaw("JoyStickRightY");
            //float Player_rot_x = 0f;
            //Player_rot_x -= Mouse_X * 0.5f;
            //Player_rot_x = Mathf.Clamp(Player_rot_x, -90, 90);
            //rb.angularVelocity = new Vector3(0, (Player_rot_x * MouseSensivity) * -1f, 0);
            //  入力をxとzに代入
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            if (Attacking != true)
            {
                //Debug.Log(dia);
                //transform.Rotate(0, dia, 0);

            }
            if (Input.GetAxis("Vertical")==0  && Input.GetAxis("Horizontal") == 0)
            {
                x = 0;
                z = 0;
            }
            if (Input.GetAxis("Horizontal") > -0.6f) Animator.SetFloat("X", Input.GetAxis("Horizontal"));
            if (Input.GetAxis("Horizontal") < 0.6f) Animator.SetFloat("X", Input.GetAxis("Horizontal"));
            if (Input.GetAxis("Vertical") > -0.1f) Animator.SetFloat("Y", Input.GetAxis("Vertical"));
            if (Input.GetAxis("Vertical") < 0.1f) Animator.SetFloat("Y", Input.GetAxis("Vertical"));

        }
        if(stats==Attack.Freeze)
        {
            Animator.SetFloat("X", 0f);
            Animator.SetFloat("Y", 0f);

        }
        if (stats == Attack.Shop)
        {

        }
        if (Input.GetButtonDown("Start"))
        {
            if (Menu.activeSelf == false)
            {

                SetState(Attack.Freeze);
                Menu.SetActive(true);
 


            }
            else
            {
                SetState(Attack.Normal);

                Menu.SetActive(false);

            }
        }
    }
    private void OnAnimatorMove()
    {
        if (stats == Attack.Normal)
        {
            transform.position = GetComponent<Animator>().rootPosition;
            transform.rotation = GetComponent<Animator>().rootRotation;
        }
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Enemy")
        {

        }
        if (col.gameObject.tag == "door")
        {
            SceneManager.LoadScene("scene2");
            player.transform.position = new Vector3(0, 0, 0);
        }
    }
    void OnTriggerEnter(Collider shop)
    {
        if (shop.gameObject.tag == "shop")
        {
            if (Input.GetKeyDown("e"))
            {

            }
        }
    }
    

    public void Slashinsta()
    {
        if(ChargeTime>=0.4f)
        {
            GameObject WaveObj = Instantiate(SlashParticle, transform.parent);
            WaveObj.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1f, gameObject.transform.position.z);
            wavecol.SetMag(ChargeTime * 1f);
            WaveObj.transform.rotation = gameObject.transform.rotation;
            WaveObj.GetComponent<Rigidbody>().AddForce(transform.forward * 10, ForceMode.Impulse);
        }

    }
    public void DamageHp(int Damage)
    {
        playerHP -= Damage;
        Debug.Log("ダメージ判定後"+playerHP); 
    }
    public float PlayerHp()
    {
        return playerHP;
    }
    public void SetState(Attack tempState)
    {
        stats = tempState;
    }
    public void SetPlayerLevel(int LevelUp)
    {
        Level += LevelUp;
        PlayerPrefs.SetInt("Level", Level);
    }
    ///////////だよよよよよおよおよよよ
    public int GetLevel()
    {
        return Level;
    }
    /////////////////////////////////////ここだよ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void SetExp(float ExpNumber)
    {
        Exp = (int)ExpNumber;
    }
    public float GetExp()
    {
        return Exp;
    }
    public void SetNextLv(float NextLv)
    {
        NextLevel += NextLv;
        Debug.Log("aaaa");

    }
    public float GetNextLv()
    {
        return NextLevel;   
    }
    private void OnDestroy()
    {
        SceneManager.LoadScene(Gameover);
        Debug.Log(22);
    }
    void Blocking()
    {
    }
    public void BlockEnemy()
    {
        BlockPlayer block = gameObject.GetComponentInChildren<BlockPlayer>();
        block.ShieldHit();
    }
        public void resetMag()
    {
        if(!SpecialMode)
        {
            ChargeTime = 0;
        }
    }
    
}