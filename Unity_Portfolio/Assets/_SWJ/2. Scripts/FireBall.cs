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
            ef.hitDamage(5);
        }
        else if (collision.gameObject.name.Contains("Mushroom"))
        {
            Enemy2FSM ef = collision.gameObject.GetComponent<Enemy2FSM>();
            ef.hitDamage(5);
        }
        StartCoroutine(shake.ShakeCamera());
        Destroy(gameObject);
        GameObject exp = Instantiate(explosion);
        exp.transform.position = collision.transform.position;
        Destroy(exp, 1f);
        
    }
}
