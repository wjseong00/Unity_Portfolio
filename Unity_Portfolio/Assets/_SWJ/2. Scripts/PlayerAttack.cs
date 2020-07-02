using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class PlayerAttack : MonoBehaviour
{
    public GameObject fireBall;
    public GameObject iceWall;
    public GameObject normalAttack;
    public Transform attackPos;
    public GameObject fireAttackMotion;
    public GameObject iceAttackMotion;
    public GameObject normalAttackMotion;
    private Animator anim;

    public float mp = 100f;
    public float initMp = 100f;
    float minDistance = 9999f; //가장 가까이있는 타겟 구하기
    Vector3 dir;        //타겟의 방향
    Vector3 target;     //타겟의 벡터값

    public int att = 0;
    public bool stun=false;

    public List<GameObject> normalMissile;
    int missileSize = 20;
    int missileIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        normalMissile = new List<GameObject>();
        for (int i = 0; i < missileSize; i++)
        {
            GameObject bullet = Instantiate(normalAttack, GameObject.Find("PlayerMissile").transform);
            bullet.SetActive(false);
            normalMissile.Add(bullet);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(!stun)
        {
            //Normal();
            //Fire();
            if (Input.GetKeyDown(KeyCode.P))
            {

                StartCoroutine(FireAttack());
            }
            //Ice();
        }
        
        
    }

    public void Ice()
    {
        //if(Input.GetKeyDown(KeyCode.O))
        {
            minDistance = 9999f;

            Collider[] cols = Physics.OverlapSphere(transform.position, 8f, 1 << 11);
            if (cols.Length <= 0)
            {
                Debug.Log("대상이 존재하지 않음");
            }
            else
            {
                //anim.SetBool("FireAttack", true);
                GameObject motion = Instantiate(iceAttackMotion, GameObject.Find("Player").transform);
                motion.transform.position = transform.position + new Vector3(0, 0.3f, 0);
                Destroy(motion, 1f);
                GameObject ice = Instantiate(iceWall);
                for (int i = 0; i < cols.Length; i++)
                {

                    if (cols[i].tag == "Enemy")
                    {
                        float distance = Vector3.Distance(cols[i].transform.position, transform.position);
                        if (distance < minDistance)
                        {
                            minDistance = distance;
                            dir = cols[i].transform.position - transform.position;
                            target = cols[i].transform.position;
                        }
                    }
                }
                ice.transform.position = target;
                //Destroy(ice, 1f);


            }
        }
    }
    public void FireBall()
    {
        StartCoroutine(FireAttack());
    }
    public void Fire()
    {
        //if(Input.GetKeyDown(KeyCode.P))
        {
            
            minDistance = 9999f;

            Collider[] cols = Physics.OverlapSphere(transform.position, 8f, 1 << 11);
            if (cols.Length <= 0)
            {
                Debug.Log("대상이 존재하지 않음");
            }
            else
            {
                anim.SetBool("FireAttack", true);
                GameObject motion = Instantiate(fireAttackMotion, GameObject.Find("Player").transform);
                motion.transform.position = transform.position + new Vector3(0, 0.3f, 0);
                Destroy(motion, 1f);
                GameObject fire = Instantiate(fireBall);
                for (int i = 0; i < cols.Length; i++)
                {

                    if (cols[i].tag == "Enemy")
                    {
                        float distance = Vector3.Distance(cols[i].transform.position, transform.position);
                        if (distance < minDistance)
                        {
                            minDistance = distance;
                            dir = cols[i].transform.position - transform.position;
                            target = cols[i].transform.position;
                        }
                    }
                }
                fire.transform.position = target + new Vector3(Random.Range(2, 10), 10, Random.Range(2, 10));
                Vector3 fdir =target - fire.transform.position;
                fdir.Normalize();
                Quaternion qdir = Quaternion.LookRotation(fdir);
                fire.transform.rotation = qdir;
                Destroy(fire,1f);
                
                
            }
        }
    }
    public IEnumerator FireAttack()
    {
        Fire();
        yield return new WaitForSeconds(0.2f);
        
        Fire();
        yield return new WaitForSeconds(0.2f);
        Fire();
        anim.SetBool("FireAttack", false);

    }


    public void Normal()
    {
        //if(Input.GetMouseButtonDown(0))
        {
            //anim.SetBool("NormalAttack", true);
            anim.SetTrigger("Attack");
            normalMissile[missileIndex].SetActive(true);
            normalMissile[missileIndex].transform.position = attackPos.position;
            normalMissile[missileIndex].transform.rotation = attackPos.rotation;
            missileIndex++;

            if (missileIndex >= missileSize) missileIndex = 0;
            GameObject motion = Instantiate(normalAttackMotion,GameObject.Find("Player").transform);
            
            motion.transform.position = transform.position + new Vector3(0,0.3f,0);

            
            Destroy(motion, 1f);
            
        }
        //if(Input.GetMouseButtonUp(0))
        {
           // anim.SetBool("NormalAttack",false);
            
        }
    }
    
    public void setStun(bool _stun)
    {
        stun = _stun;
    }

    
}
