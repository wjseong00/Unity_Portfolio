using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BossCtrl : MonoBehaviour
{
    Transform player;
    CharacterController cc;
    public Transform firePos;
    public Canvas canvas;
    enum AttackPattern
    {
        idle,fly,fire,frost
    }
    AttackPattern state;
    AttackPattern preState;
    float speed = 1f;
    bool dash = false;

    Animator anim;

    public Image Hpbar;
    float hp = 500f;//체력
    float initHp = 500f;
    public GameObject hudDamageText;
    public Transform hudPos;

    #region "썬더공격에 대한 함수"

    public GameObject Thunder;
    public GameObject ThunderTarget;
    
    bool makeCircle=false;
    float curTime = 0;
    float AttTime = 5f;
    float curTimeChange = 0f;
    float changeTime = 10f;
    float upTime = 0f;
    float DownTime = 0f;
    float fullTime = 2.5f;
    Vector3 target;
    Vector3 preTarget;
    private Shake shake;


   

    #endregion
    #region "파이어공격에 대한 함수"
    bool traceTarget = false;
   
    bool makeLaser = false;
    public GameObject blastFactory;
     GameObject blast;
    float fireTime = 0f;
    float usingFire = 5f;

    #endregion
    #region "얼음공격에 대한 함수"
    public GameObject FrostFactory;

    float curFrost = 0f;
    float endFrost = 2.0f;
    public Transform forLocate1;
    public Transform forLocate2;
    bool crefor = false;
    
    #endregion

    void Start()
    {
        player = GameObject.Find("Player").transform;
        state = (AttackPattern)Random.Range(1,4);
        cc = GetComponent<CharacterController>();
        blast = Instantiate(blastFactory);
        blast.SetActive(false);
        anim= GetComponent<Animator>();
        shake = GameObject.Find("CameraRig").GetComponent<Shake>();
        canvas = GameObject.Find("View").GetComponent<Canvas>();



    }
    


    private void Update()
    {
        
        switch (state)
        {
            case AttackPattern.idle:
                
                break;
            case AttackPattern.fly:
                FlyAttack();
                break;
            case AttackPattern.frost:
                FrostAttack();
                break;
            
            case AttackPattern.fire:
                FireAttack();
                break;
            default:
                break;
        }
        if(Input.GetKeyDown(KeyCode.T))
        {
            ValueReset();
            state =AttackPattern.fly;
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            ValueReset();
            state = AttackPattern.fire;
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            ValueReset();
            state = AttackPattern.frost;
        }
    }

    
    
    void ValueReset()
    {
        upTime = 0f;
        DownTime = 0f;
        crefor = false;
        curTime = 0f;
        curTimeChange = 0f;
        curFrost = 0f;
    }
    //날아서 번개공격
    private void FlyAttack()
    {
        Vector3 dir1 = (player.position - transform.position).normalized;
        dir1.y = 0;
        transform.rotation = Quaternion.LookRotation(dir1);
        if(curTimeChange > changeTime)
        {
            anim.SetBool("FlyAttack", false);
           
            cc.enabled = true;
            DownTime += Time.deltaTime;
            
            if (DownTime<fullTime)
            {
                
                transform.Translate(Vector3.down * 2 * Time.deltaTime);
            }
            else
            {
                
               
                
                ValueReset();
                state = AttackPattern.fire;
                
                
            }
            
        }
        else
        {
            upTime += Time.deltaTime;
            Vector3 dir = (player.position - transform.position).normalized;
            if (upTime < fullTime)
            {
                transform.Translate(Vector3.up * 2 * Time.deltaTime);
            }
            else
            {
                cc.enabled = false;
                
                curTimeChange += Time.deltaTime;
                
                Collider[] cols = Physics.OverlapSphere(transform.position - new Vector3(0, 3.5f, 0), 20f);
                
                
                    for (int i = 0; i < cols.Length; i++)
                    {
                        if (cols[i].tag == "Player")
                        {
                            
                            target = cols[i].transform.position;

                            curTime += Time.deltaTime;
                            if (curTime > AttTime)
                            {
                                anim.SetBool("FlyAttack", true);
                                curTime = 0f;
                                GameObject thunT = Instantiate(ThunderTarget);
                                thunT.transform.position = target + new Vector3(0, 0.3f, 0);
                                preTarget = thunT.transform.position;
                                Destroy(thunT, 0.7f);
                                Invoke("ThunderAttack", 0.7f);
                            }
                            else
                            {
                                anim.SetBool("FlyAttack", false);
                            }
                        }
                    }

                
            }
        }
        
    }
    
    void ThunderAttack()
    {
        
        StartCoroutine(shake.ShakeCamera());
        GameObject Thun = Instantiate(Thunder);
        Thun.transform.position = preTarget;
        Destroy(Thun, 0.5f);
    }
    
    
    //추적불발사
    private void FireAttack()
    {
        anim.SetBool("FireAttack",true);
        Vector3 dir = (player.position - firePos.position).normalized;
        dir.y = 0;
        blast.SetActive(true);

        blast.transform.position = firePos.position;
        blast.transform.rotation = Quaternion.LookRotation(dir);
        
        transform.rotation = Quaternion.LookRotation(dir);
        if (Vector3.Distance(transform.position,player.position)>5f)
        {
            transform.Translate(Vector3.forward * 1.5f * Time.deltaTime);
        }
        fireTime += Time.deltaTime;
        if(fireTime>usingFire)
        {
            fireTime = 0f;
            anim.SetBool("FireAttack", false);
            player.GetComponent<PlayerDamage>().fire(false);
            blast.SetActive(false);
            ValueReset();
            state = AttackPattern.frost;
            
            
        }
        //laser.transform.rotation = transform.rotation;
    }
    private void FrostAttack()
    {
        anim.SetBool("TornadoAttack", true);
        
        curFrost += Time.deltaTime;
        if (curFrost>endFrost)
        {
            
            GameObject frost1 = Instantiate(FrostFactory);
            GameObject frost2 = Instantiate(FrostFactory);
            curFrost = 0f;
            frost1.transform.position = forLocate1.position;
            frost2.transform.position = forLocate2.position;
            ValueReset();
            
            Invoke("changeState", 1f);
            
        }
        

    }
    void changeState()
    {
        anim.SetBool("TornadoAttack", false);
        state = AttackPattern.fly;

    }

    void minueDamaged()
    {
        hp--;
        Hpbar.fillAmount = hp / initHp;
        //hpBarImage.fillAmount = hp / initHp;

    }
    public void Damaged(int value)
    {
        for (float i = 0; i < value; i++)
        {
            Invoke("minueDamaged", i / 10);
        }
        GameObject hudText = Instantiate(hudDamageText); // 생성할 텍스트 오브젝트
        Vector3 ScreenPos = Camera.main.WorldToScreenPoint(hudPos.position);

        hudText.transform.localPosition = ScreenPos; // 표시될 위치
        hudText.transform.SetParent(canvas.transform);
        hudText.GetComponent<DamageText>().damage = value; // 데미지 전달
    }



}
