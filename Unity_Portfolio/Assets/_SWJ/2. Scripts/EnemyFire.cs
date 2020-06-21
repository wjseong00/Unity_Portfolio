using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    public GameObject explosion;
    float speed = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);

        GameObject exp = Instantiate(explosion);
        exp.transform.position = transform.position;
        Destroy(exp, 1f);
    }
}
