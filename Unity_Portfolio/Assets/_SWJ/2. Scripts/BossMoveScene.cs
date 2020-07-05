using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMoveScene : MonoBehaviour
{
    Animator anim;
    //private Shake shake;

    float curTime = 0f;
   
    float arriveTime = 3f;
    float moveSpeed = 0.2f;
    bool hawling = false;
    
    
    
    private void Start()
    {
        anim = GetComponent<Animator>();
        //shake = GameObject.Find("Pos").GetComponent<Shake>();
    }

    
    void Update()
    {
        
        anim.SetBool("Walk", true);

        curTime += Time.deltaTime;
        if (curTime > arriveTime)
        {
            
            anim.SetBool("Walk", false);
            if(!hawling)
            {
                //StartCoroutine(shake.ShakeCamera());
                anim.SetTrigger("Hawling");
                hawling = true;

            }
            GetComponent<BossSceneEnd>().enabled = true;
            GetComponent<BossMoveScene>().enabled = false;
                
                
           
            //camPos2.SetActive(false);

        }
        else
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
    }
}
