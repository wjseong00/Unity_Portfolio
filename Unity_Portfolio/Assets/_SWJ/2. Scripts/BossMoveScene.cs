using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMoveScene : MonoBehaviour
{
    Animator anim;
    public GameObject camPos2;

    float curTime = 0f;
    float arriveTime = 3f;
    float moveSpeed = 0.2f;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    
    void Update()
    {
        anim.SetBool("Run", false);
        anim.SetBool("Walk", true);

        curTime += Time.deltaTime;
        if (curTime > arriveTime)
        {
            anim.SetBool("Walk", false);
            
            //camPos2.SetActive(false);

        }
        else
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
    }
}
