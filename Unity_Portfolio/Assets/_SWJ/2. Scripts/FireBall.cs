using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public GameObject explosion;
    float speed = 5.0f;
    
    void Start()
    {
       
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        GameObject exp = Instantiate(explosion);
        exp.transform.position = collision.transform.position;
        Destroy(exp, 1f);
    }
}
