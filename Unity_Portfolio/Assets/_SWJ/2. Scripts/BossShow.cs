using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShow : MonoBehaviour
{
    public GameObject playerStartPos;
    GameObject cameraRig;
    GameObject player;
    public GameObject UiInter;
    bool startShow = false;

    void Start()
    {
        player = GameObject.Find("Player");
        cameraRig = GameObject.Find("CameraRig");
    }


    
    void Update()
    {
        if(startShow)
        {
            UiInter.SetActive(false);
            cameraRig.GetComponent<FollowCam>().enabled = false;
            
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(!startShow)
            {
                startShow = true;
            }
            
        }
    }
}
