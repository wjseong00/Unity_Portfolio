using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    public GameObject stunFactory;
    float hp=100f;
    float initHp=100f;

    public bool isFire = false;
    void minusHp()
    {
        hp--;
        print("플레이어 피 : " + hp);
    }


    public void hitDamage(int value)
    {
        for (int i = 0; i < value; i++)
        {
            Invoke("minusHp", i / 10);
        }
    }
    public void StunPlayer()
    {
        GameObject stun = Instantiate(stunFactory,transform);
        stun.transform.position = transform.position + new Vector3(0, 0.6f, 0);
        Destroy(stun, 1.5f);
        if(GetComponent<PlayerAttack>().stun == true)
        {
            StopAllCoroutines();
        }
        StartCoroutine(isStun());
    }
    IEnumerator isStun()
    {
        GetComponent<PlayerAttack>().setStun(true);
        GetComponent<PlayerMove>().setStun(true);
        yield return new WaitForSeconds(2f);
        GetComponent<PlayerAttack>().setStun(false);
        GetComponent<PlayerMove>().setStun(false);
    }
    private void Update()
    {
        if (isFire)
        {
            hp-=0.2f;
            print("플레이어 피 : " + hp);
        }
    }
    public void fire(bool _fire)
    {
        isFire = _fire;
    }

}
