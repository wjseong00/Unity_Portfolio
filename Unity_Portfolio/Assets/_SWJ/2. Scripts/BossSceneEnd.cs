using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSceneEnd : MonoBehaviour
{
    float curTime = 0f;
    float startTime = 2f;
    public GameObject camPos2;
    public GameObject cameraRig;
    public GameObject player;
    public GameObject UiInter;
    public GameObject mainCamera;
    public GameObject startPos;
    public GameObject bossHp;
    public GameObject joyStick;
    void Start()
    {
        
    }
    

    void Update()
    {
        curTime += Time.deltaTime;
        if (curTime > startTime)
        {

            mainCamera.SetActive(true);
            camPos2.SetActive(false);
            Inventory.instance.bossShow = false;
            Inventory.instance.bossEnd = true;
            UiInter.SetActive(true);
            cameraRig.GetComponent<FollowCam>().enabled = true;
            player.GetComponent<PlayerMove>().enabled = true;
            joyStick.GetComponent<JoyStick>().enabled = false;
            player.GetComponent<PlayerAttack>().enabled = true;
            GetComponent<BossCtrl>().enabled = true;
            startPos.GetComponent<BossShow>().enabled = false;
            bossHp.SetActive(true);
            GetComponent<BossSceneEnd>().enabled = false;

            
        }
        
    }
}
