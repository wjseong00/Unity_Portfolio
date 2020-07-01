using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public GameObject explosion;
    float speed = 20.0f;
    //카메라흔들기
    private Shake shake;
    void Start()
    {
        shake = GameObject.Find("CameraRig").GetComponent<Shake>();
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("Witch"))
        {
            EnemyFSM ef = collision.gameObject.GetComponent<EnemyFSM>();
            ef.hitDamage(Random.Range(5,11)+GameObject.Find("Player").GetComponent<PlayerAttack>().att);
        }
        else if (collision.gameObject.name.Contains("Mushroom") || collision.gameObject.name.Contains("KeyMonster"))
        {
            Enemy2FSM ef = collision.gameObject.GetComponent<Enemy2FSM>();
            ef.hitDamage(Random.Range(5, 11)+ GameObject.Find("Player").GetComponent<PlayerAttack>().att);
        }
        else if (collision.gameObject.name.Contains("Boss"))
        {
            BossCtrl bf = collision.gameObject.GetComponent<BossCtrl>();
            bf.Damaged(Random.Range(5, 11)+ GameObject.Find("Player").GetComponent<PlayerAttack>().att);
        }
        StartCoroutine(shake.ShakeCamera());
        Destroy(gameObject);
        GameObject exp = Instantiate(explosion);
        exp.transform.position = collision.transform.position;
        Destroy(exp, 1f);
        
    }
}
