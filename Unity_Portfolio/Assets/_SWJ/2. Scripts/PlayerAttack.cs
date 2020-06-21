using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class PlayerAttack : MonoBehaviour,IPointerDownHandler, IPointerUpHandler 
{
    public GameObject fireBall;
    public GameObject iceWall;
    public GameObject normalAttack;
    public Transform attackPos;
    public GameObject fireAttackMotion;
    public GameObject iceAttackMotion;
    public GameObject normalAttackMotion;
    private Animator anim;

    


    float minDistance = 9999f; //가장 가까이있는 타겟 구하기
    Vector3 dir;        //타겟의 방향
    Vector3 target;     //타겟의 벡터값

    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        
    }

    // Update is called once per frame
    void Update()
    {
        Normal();
        //Fire();
        if (Input.GetKeyDown(KeyCode.P))
        {
            
            StartCoroutine(FireAttack());
        }
        Ice();
        
    }

    private void Ice()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            minDistance = 9999f;

            Collider[] cols = Physics.OverlapSphere(transform.position, 5f, 1 << 11);
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
                Destroy(ice, 1f);


            }
        }
    }

    private void Fire()
    {
        //if(Input.GetKeyDown(KeyCode.P))
        {
            
            minDistance = 9999f;

            Collider[] cols = Physics.OverlapSphere(transform.position, 5f, 1 << 11);
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
    IEnumerator FireAttack()
    {
        Fire();
        yield return new WaitForSeconds(0.2f);
        
        Fire();
        yield return new WaitForSeconds(0.2f);
        Fire();
        anim.SetBool("FireAttack", false);

    }


    private void Normal()
    {
        if(Input.GetMouseButtonDown(0))
        {
            anim.SetBool("NormalAttack", true);
            GameObject missile = Instantiate(normalAttack);
     
            GameObject motion = Instantiate(normalAttackMotion,GameObject.Find("Player").transform);
            missile.transform.position = attackPos.position;
            missile.transform.rotation = attackPos.rotation;
            motion.transform.position = transform.position + new Vector3(0,0.3f,0);

            Destroy(missile, 3f);
            Destroy(motion, 1f);
            
        }
        if(Input.GetMouseButtonUp(0))
        {
            anim.SetBool("NormalAttack",false);
            
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        anim.SetBool("NormalAttack", true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        anim.SetBool("NormalAttack", false);
    }
}
