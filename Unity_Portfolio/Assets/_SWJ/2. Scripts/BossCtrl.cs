using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCtrl : MonoBehaviour
{
    Transform player;
    Rigidbody rig;
    enum AttackPattern
    {
        idle,fly,thunder,laser
    }
    AttackPattern state;
    float speed = 1f;
    bool dash = false;
    Vector3 upLocate;


    void Start()
    {
        player = GameObject.Find("Player").transform;
        state = AttackPattern.idle;
        rig = GetComponent<Rigidbody>();
        
    }

    
    
    private void Update()
    {
        
        switch (state)
        {
            case AttackPattern.idle:
                Idle();
                break;
            case AttackPattern.fly:
                FlyAttack();
                break;
            case AttackPattern.thunder:
                Thunder();
                break;
            case AttackPattern.laser:
                Laser();
                break;
            default:
                break;
        }
        if(Input.GetKeyDown(KeyCode.T))
        {
            upLocate = transform.position + new Vector3(0, 10, 0);
            state=AttackPattern.fly;
        }
    }

    private void Idle()
    {
        
    }

    //날아서 돌진공격
    private void FlyAttack()
    {
        transform.position = Vector3.Lerp(transform.position, upLocate, speed * Time.deltaTime);
        if(Vector3.Distance(transform.position,upLocate)<0.1f)
        {
            dash = true;
            
        }
        
        if(dash)
        {
            Vector3 dir = (player.position - transform.position).normalized;
            transform.up = dir;
            transform.position = Vector3.Lerp(transform.position, player.position, speed * Time.deltaTime);
        }



    }


    //주위 번개떨구기
    private void Thunder()
    {

    }

    //추적레이저
    private void Laser()
    {
        
    }

    

    
}
