using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalMissile : MonoBehaviour
{
    public GameObject explosion;
    float speed = 10.0f;
    Vector3 dir;
    Vector3 target;
    float minDistance = 9999f;

    float curTime = 0;
    float limitTime = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        float minDistance = 9999f;
        curTime += Time.deltaTime;
       if(curTime>limitTime)
        {
            gameObject.SetActive(false);
            curTime = 0;
        }
        Collider[] cols = Physics.OverlapSphere(transform.position, 1f,1<<11);
        if (cols.Length <= 0)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        else
        {
            for (int i = 0; i < cols.Length; i++)
            {
                
                if(cols[i].tag =="Enemy")
                {
                    float distance = Vector3.Distance(cols[i].transform.position, transform.position);
                    if(distance<minDistance)
                    {
                        minDistance = distance;
                        dir = cols[i].transform.position - transform.position;
                        target = cols[i].transform.position;
                    }
                
                }
                
            }
            dir.y = 0;
            dir.Normalize();
            transform.position = Vector3.Lerp(transform.position, target, 5 * Time.deltaTime);


        }


    }
    private void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);
        
        GameObject exp = Instantiate(explosion);
        exp.transform.position =transform.position;
        Destroy(exp, 1f);
        if(collision.gameObject.name.Contains("Witch"))
        {
            EnemyFSM ef = collision.gameObject.GetComponent<EnemyFSM>();
            ef.hitDamage(1);
        }
        else if (collision.gameObject.name.Contains("Mushroom"))
        {
            Enemy2FSM ef = collision.gameObject.GetComponent<Enemy2FSM>();
            ef.hitDamage(1);
        }
    }
}
