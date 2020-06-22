using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    public GameObject explosion;
    float speed = 10.0f;
    int att =5;
    Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
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
        if(collision.collider.name == "Player")
        {
            //플레이어의 필요한 스크립트 컴포넌트를 가져와서 데미지를 주면 된다
            player.GetComponent<PlayerDamage>().hitDamage(att);
        }
        
    }
}
