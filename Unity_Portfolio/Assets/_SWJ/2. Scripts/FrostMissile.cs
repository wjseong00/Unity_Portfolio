using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostMissile : MonoBehaviour
{
    GameObject player;
    GameObject boss;
    public GameObject bombFactory;
    float speed =2.0f;
    Vector3 origin;
    
    void Start()
    {
        
        player = GameObject.Find("Player");
        boss = GameObject.Find("Boss");
        origin = transform.position;
        speed = Random.Range(0.7f, 2.1f);
    }

    void Update()
    {
        
        Vector3 dir = (player.transform.position - transform.position).normalized;
        dir.y = 0;
        transform.Translate(dir * speed * Time.deltaTime);
        //transform.position = Vector3.Slerp(transform.position, player.transform.position, speed * Time.deltaTime);
        
        Destroy(gameObject,13f);
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            player.GetComponent<PlayerDamage>().hitDamage(10);
            player.GetComponent<PlayerDamage>().freeze();
            GameObject bomb = Instantiate(bombFactory);
            bomb.transform.position = transform.position;
            Destroy(bomb, 1f);

        }

        if (other.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
            boss.GetComponent<BossCtrl>().Damaged(10);
            GameObject bomb = Instantiate(bombFactory);
            bomb.transform.position = transform.position;
            Destroy(bomb, 1f);
        }
    }
}
