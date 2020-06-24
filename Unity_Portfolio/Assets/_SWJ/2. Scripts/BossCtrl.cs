﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCtrl : MonoBehaviour
{
    Transform player;
    CharacterController cc;
    public Transform firePos;
    enum AttackPattern
    {
        idle,fly,laser,frost
    }
    AttackPattern state;
    float speed = 1f;
    bool dash = false;

    Animator anim;

    #region "썬더공격에 대한 함수"
    
    public GameObject Thunder;
    public GameObject ThunderTarget;
    
    bool makeCircle=false;
    float curTime = 0;
    float AttTime = 3f;
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
        state = AttackPattern.idle;
        cc = GetComponent<CharacterController>();
        blast = Instantiate(blastFactory);
        blast.SetActive(false);
        anim= GetComponent<Animator>();
        shake = GameObject.Find("CameraRig").GetComponent<Shake>();
 


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
            
            case AttackPattern.laser:
                LaserAttack();
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
            state = AttackPattern.laser;
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
                state = (AttackPattern)Random.Range(1,4);
                

                Debug.Log("바뀜");
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
                
                Collider[] cols = Physics.OverlapSphere(transform.position - new Vector3(0, 3.5f, 0), 15f);
                if (cols.Length <= 0)
                {
                    Debug.Log("타겟이 없음");
                }
                else
                {
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
        
    }
    
    void ThunderAttack()
    {
        
        StartCoroutine(shake.ShakeCamera());
        GameObject Thun = Instantiate(Thunder);
        Thun.transform.position = preTarget;
        Destroy(Thun, 0.5f);
    }
    
    
    //추적불발사
    private void LaserAttack()
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
            blast.SetActive(false);
            ValueReset();
            state = (AttackPattern)Random.Range(1, 4);
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
            anim.SetBool("TornadoAttack", false);
            Invoke("changeState", 1f);
            
        }
        

    }
    void changeState()
    {
        state = (AttackPattern)Random.Range(1, 4);



    }


}
