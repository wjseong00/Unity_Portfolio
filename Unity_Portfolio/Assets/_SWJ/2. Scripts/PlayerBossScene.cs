using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBossScene : MonoBehaviour
{
    Animator anim;
    public GameObject camPos1;
    public GameObject camPos2;
    public GameObject portal;
    public GameObject startPos;
    public GameObject boss;
    float curTime = 0f;
    float arriveTime = 6f;
    float moveSpeed = 0.4f;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        anim.SetBool("Jump", false);
        anim.SetBool("Run", false);
        anim.SetBool("Walk", true);
        
        curTime += Time.deltaTime;
        if(curTime>arriveTime)
        {
            anim.SetBool("Walk", false);
            GetComponent<PlayerBossScene>().enabled = false;
            camPos1.SetActive(false);
            camPos2.SetActive(true);
            boss.gameObject.SetActive(true);

        }
        else
        {
            Vector3 dir = (portal.transform.position - startPos.transform.position).normalized;
            dir.y = 0;
            transform.Translate(dir * moveSpeed * Time.deltaTime);
        }
    }
}
