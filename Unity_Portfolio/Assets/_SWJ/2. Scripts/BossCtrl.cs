using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCtrl : MonoBehaviour
{
    Transform player;
    Rigidbody rig;
    enum AttackPattern
    {
        idle,fly,laser
    }
    AttackPattern state;
    float speed = 1f;
    bool dash = false;
    Vector3 upLocate;
    Vector3 origin;
    #region "썬더공격에 대한 함수"
    public GameObject Circle;
    public GameObject Thunder;
    public GameObject ThunderTarget;
    GameObject cir;
    bool makeCircle=false;
    float curTime = 0;
    float AttTime = 1f;
    float curTimeChange = 0f;
    float changeTime = 6f;
    Vector3 target;
    Vector3 preTarget;
    #endregion
    #region "레이저공격에 대한 함수"
    bool traceTarget = false;
    public GameObject Laser;
    bool makeLaser = false;
    GameObject laser;
    float laserTime = 0f;
    float locateTime = 0f;
    float updateLaser = 0.4f;
    float updateLocate = 0.2f;
    Vector3 preTar;
    Transform preRot;

    #endregion
    #region "회오리공격에 대한 함수"

    #endregion

    void Start()
    {
        player = GameObject.Find("Player").transform;
        state = AttackPattern.idle;
        rig = GetComponent<Rigidbody>();
        
    }

    
    
    private void Update()
    {
        
        switch (state)
        {
            case AttackPattern.idle:
                Idle();
                break;
            case AttackPattern.fly:
                FlyAttack();
                break;
            
            
            case AttackPattern.laser:
                LaserAttack();
                break;
            default:
                break;
        }
        if(Input.GetKeyDown(KeyCode.T))
        {
            origin = transform.position;
            upLocate = transform.position + new Vector3(0, 5, 0);
            curTime = 0f;
            curTimeChange = 0f;
            state =AttackPattern.fly;
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            
            state = AttackPattern.laser;
        }
    }

    private void Idle()
    {
        
    }

    //날아서 돌진공격
    private void FlyAttack()
    {
        
        if(curTimeChange > changeTime)
        {
            rig.useGravity = true;
            makeCircle = false;
            Destroy(cir);
            if (Vector3.Distance(transform.position, origin) < 0.1f)
            {
                
                state = AttackPattern.laser;
                Debug.Log("바뀜");
                
            }
            
        }
        else
        {
            rig.useGravity = false;
        }
        
        transform.position = Vector3.Lerp(transform.position, upLocate, speed * Time.deltaTime);
        if(Vector3.Distance(transform.position,upLocate)<0.1f)
        {
            
            if (!makeCircle)
            {
                cir = Instantiate(Circle);
                cir.transform.position = transform.position - new Vector3(0,4.47f,0);
                makeCircle = true;
                
            }

            
            
        }
        if(makeCircle)
        {
            curTimeChange += Time.deltaTime;
            Collider[] cols = Physics.OverlapSphere(transform.position - new Vector3(0, 4.47f, 0), 5f);
            if(cols.Length<=0)
            {
                Debug.Log("타겟이 없음");
            }
            else
            {
                for (int i = 0; i < cols.Length; i++)
                {
                    if(cols[i].tag == "Player")
                    {
                        target = cols[i].transform.position;
                        
                        curTime += Time.deltaTime;
                        if (curTime > AttTime)
                        {
                            curTime = 0f;
                            GameObject thunT = Instantiate(ThunderTarget);
                            thunT.transform.position = target + new Vector3(0, 0.4f, 0);
                            preTarget = thunT.transform.position;
                            Destroy(thunT, 0.7f);
                            Invoke("ThunderAttack", 0.7f);
                        }
                    }
                }
                
            }
        }



    }
    void ThunderAttack()
    {
        GameObject Thun = Instantiate(Thunder);
        Thun.transform.position = preTarget;
        Destroy(Thun, 0.5f);
    }
    void trace()
    {
        Vector3 dir = player.position - transform.position;
        transform.rotation = Quaternion.LookRotation(dir);
        preTar = transform.position;
        preRot = transform;
    }

    private void LateUpdate()
    {
        if(traceTarget)
        {
            
            if (!makeLaser)
            {
                laser = Instantiate(Laser);
                laser.transform.up = transform.forward;
                makeLaser = true;
            }
            laser.transform.position = transform.position;
            laser.transform.up = Vector3.Lerp(laser.transform.up, transform.forward, 40 * Time.deltaTime);
        }
        
    }
    //추적레이저
    private void LaserAttack()
    {
        traceTarget = true;
        laserTime += Time.deltaTime;
        Invoke("trace", 0.3f);
        if(laserTime>updateLaser)
        {
            laserTime = 0f;
            
            
            
        }
        

        //laser.transform.rotation = transform.rotation;
    }

    

    
}
