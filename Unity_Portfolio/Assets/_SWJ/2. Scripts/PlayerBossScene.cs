﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBossScene : MonoBehaviour
{
    Animator anim;
    CharacterController cc;
    public GameObject camPos1;
    public GameObject camPos2;
    public GameObject portal;
    public GameObject startPos;
    public GameObject boss;
    float curTime = 0f;
    float arriveTime = 4f;
    float moveSpeed = 0.6f;
    bool run = false;
    private void Start()
    {
        anim = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
    }

    private void Update()
    {
        anim.SetBool("Jump", false);
        anim.SetBool("Run", false);
        anim.SetBool("Walk", true);
        
        curTime += Time.deltaTime;
        if(curTime>arriveTime)
        {
            run = true ;
            anim.SetBool("Walk", false);
            GetComponent<PlayerBossScene>().enabled = false;
            camPos1.SetActive(false);
            camPos2.SetActive(true);
            boss.gameObject.SetActive(true);
            boss.GetComponent<BossMoveScene>().enabled = true;

        }
        if(!run)
        {
            Vector3 dir = (portal.transform.position - startPos.transform.position).normalized;
            dir.y = 0;
            cc.Move(dir * moveSpeed * Time.deltaTime);
        }
    }
}
