using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceWall : MonoBehaviour
{
    AudioSource sound;
    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Witch"))
        {
            sound.Play();
            EnemyFSM ef = other.gameObject.GetComponent<EnemyFSM>();
            ef.hitDamage(Random.Range(10,16) + GameObject.Find("Player").GetComponent<PlayerAttack>().att);
        }
        else if (other.gameObject.name.Contains("Mushroom") || other.gameObject.name.Contains("KeyMonster"))
        {
            sound.Play();
            Enemy2FSM ef = other.gameObject.GetComponent<Enemy2FSM>();
            ef.hitDamage(Random.Range(10, 16) + GameObject.Find("Player").GetComponent<PlayerAttack>().att);
        }
        else if (other.gameObject.name.Contains("Boss"))
        {
            sound.Play();
            BossCtrl bf = other.gameObject.GetComponent<BossCtrl>();
            bf.Damaged(Random.Range(10, 16)+GameObject.Find("Player").GetComponent<PlayerAttack>().att);
        }
    }
}
