using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerDamage : MonoBehaviour
{
    public GameObject stunFactory;
    public float hp=100f;
    public float initHp=100f;
    public GameObject joystick;
    public Image hpBar;
    Animator anim;
    public bool isFire = false;
    public bool isFreeze = false;
    public bool isDie = false;
    public void updateHp()
    {
        hpBar.fillAmount = hp / initHp;

    }
    private void Start()
    {
        anim=GetComponent<Animator>();
    }
    void minusHp()
    {
        hp--;
        hpBar.fillAmount = hp / initHp;
        if (hp < 0)
        {
            isDie = true;
            StopAllCoroutines();
            anim.SetBool("Stun", false);
            CancelInvoke("minusHp");
            anim.SetTrigger("Die");
            Die();
        }

    }
    IEnumerator delay()
    {
        anim.SetBool("Damage", true);
        yield return new WaitForSeconds(0.1f);
        anim.SetBool("Damage", false);
    }

    public void hitDamage(float value)
    {

        if (!isDie)
        {
            
            for (float i = 0; i < value; i++)
            {
                Invoke("minusHp", i / value);
                hpBar.fillAmount = hp / initHp;
            }
            if (hp < 0)
            {
                anim.SetBool("Stun", false);
                isDie = true;
                CancelInvoke("minusHp");
                StopAllCoroutines();
                anim.SetTrigger("Die");
                Die();
            }
        }

    }

    private void Die()
    {
        StopAllCoroutines();
        
        StartCoroutine(DieProc());
    }
    IEnumerator DieProc()
    {
        GetComponent<PlayerAttack>().enabled =false;
        GetComponent<PlayerMove>().enabled = false;
        joystick.GetComponent<JoyStick>().enabled = false;
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
    public void StunPlayer(float value)
    {
        if(!isDie)
        {
            anim.SetBool("Stun", true);
            GameObject stun = Instantiate(stunFactory, transform);
            stun.transform.position = transform.position + new Vector3(0, 0.6f, 0);

            Destroy(stun, 1.5f);
            if (GetComponent<PlayerAttack>().stun == true)
            {
                StopAllCoroutines();
            }
            StartCoroutine(isStun(value));
        }
        
    }
    IEnumerator isStun(float value)
    {
        for (int i = 0; i < value; i++)
        {
            Invoke("minusHp", i / value);
            hpBar.fillAmount = hp / initHp;
        }
        
        GetComponent<PlayerAttack>().setStun(true);
        GetComponent<PlayerMove>().setStun(true);
        joystick.GetComponent<JoyStick>().stun=true;
        yield return new WaitForSeconds(2f);
        anim.SetBool("Stun", false);
        GetComponent<PlayerAttack>().setStun(false);
        GetComponent<PlayerMove>().setStun(false);
        joystick.GetComponent<JoyStick>().stun = false;

    }
    private void Update()
    {
        if (isFire)
        {
            hp-=0.2f;
            hpBar.fillAmount = hp / initHp;
            if (hp < 0)
            {
                isFire = false;
                anim.SetTrigger("Die");
                Die();
            }
        }
        if(hp>100)
        {
            hp = 100;
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
        joystick.GetComponent<JoyStick>().moveSpeed = 2.0f;
        joystick.GetComponent<JoyStick>().aniSpeed = 0.7f;
        yield return new WaitForSeconds(2f);
        GetComponent<PlayerMove>().frontSpeed = 4f;
        GetComponent<PlayerMove>().aniSpeed = 1.5f;
        joystick.GetComponent<JoyStick>().moveSpeed = 2.0f;
        joystick.GetComponent<JoyStick>().aniSpeed = 0.7f;
    }
}
