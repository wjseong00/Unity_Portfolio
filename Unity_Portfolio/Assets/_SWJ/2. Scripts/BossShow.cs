using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShow : MonoBehaviour
{
    public GameObject playerStartPos;
    public GameObject cameraRig;
    public GameObject player;
    public GameObject UiInter;
    public GameObject camPos1;
    bool startShow = false;

    void Start()
    {
 
    }


    
    void Update()
    {
        if(startShow)
        {
            UiInter.SetActive(false);

            cameraRig.GetComponent<FollowCam>().enabled = false;
            player.GetComponent<PlayerMove>().enabled = false;
            player.GetComponent<PlayerAttack>().enabled = false;
            player.GetComponent<PlayerBossScene>().enabled = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(!startShow)
            {
                Camera.main.gameObject.SetActive(false);
                camPos1.SetActive(true);
                player.transform.position = playerStartPos.transform.position;
                startShow = true;
            }
            
        }
    }
}
