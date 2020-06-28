using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceWall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Witch"))
        {
            EnemyFSM ef = other.gameObject.GetComponent<EnemyFSM>();
            ef.hitDamage(Random.Range(10,16));
        }
        else if (other.gameObject.name.Contains("Mushroom") || other.gameObject.name.Contains("KeyMonster"))
        {
            Enemy2FSM ef = other.gameObject.GetComponent<Enemy2FSM>();
            ef.hitDamage(Random.Range(10, 16));
        }
        else if (other.gameObject.name.Contains("Boss"))
        {
            BossCtrl bf = other.gameObject.GetComponent<BossCtrl>();
            bf.Damaged(Random.Range(10, 16));
        }
    }
}
