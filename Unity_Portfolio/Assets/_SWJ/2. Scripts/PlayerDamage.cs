using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    public GameObject stunFactory;
    public float hp=100f;
    public float initHp=100f;
    Animator anim;
    public bool isFire = false;
    public bool isFreeze = false;
    private void Start()
    {
        anim=GetComponent<Animator>();
    }
    void minusHp()
    {
        hp--;
        
    }
    IEnumerator delay()
    {
        anim.SetBool("Damage", true);
        yield return new WaitForSeconds(0.1f);
        anim.SetBool("Damage", false);
    }

    public void hitDamage(int value)
    {
        
        
        for (int i = 0; i < value; i++)
        {
            Invoke("minusHp", i / 10);
        }
        

    }
    public void StunPlayer(int value)
    {
        
        anim.SetBool("Stun", true);
        GameObject stun = Instantiate(stunFactory,transform);
        stun.transform.position = transform.position + new Vector3(0, 0.6f, 0);
        
        Destroy(stun, 1.5f);
        if (GetComponent<PlayerAttack>().stun == true)
        {
            StopAllCoroutines();
        }
        StartCoroutine(isStun(value));
    }
    IEnumerator isStun(int value)
    {
        for (int i = 0; i < value; i++)
        {
            Invoke("minusHp", i / 10);
        }
        GetComponent<PlayerAttack>().setStun(true);
        GetComponent<PlayerMove>().setStun(true);
        yield return new WaitForSeconds(2f);
        anim.SetBool("Stun", false);
        GetComponent<PlayerAttack>().setStun(false);
        GetComponent<PlayerMove>().setStun(false);
        
    }
    private void Update()
    {
        if (isFire)
        {
            hp-=0.2f;
           
        }
    }
    public void fire(bool _fire)
    {
        isFire = _fire;
    }
    public void freeze()
    {
        StartCoroutine(slow());
    }
    IEnumerator slow()
    {
        GetComponent<PlayerMove>().frontSpeed = 2.0f;
        GetComponent<PlayerMove>().aniSpeed = 0.7f;
        yield return new WaitForSeconds(2f);
        GetComponent<PlayerMove>().frontSpeed = 4f;
        GetComponent<PlayerMove>().aniSpeed = 1.5f;
    }
}
