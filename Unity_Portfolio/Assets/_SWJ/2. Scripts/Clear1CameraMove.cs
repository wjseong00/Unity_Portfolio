using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Clear1CameraMove : MonoBehaviour
{
    public GameObject isEnemy;
    GameObject cameraRig;
    int enemyCount = 0;

    public GameObject UiInter;
    public GameObject Portal;
    GameObject player;

    public bool startCameraMove = false;
    public bool endCameraMove = false;
    bool setMainPos = false;
    bool isPlayer = false;
    float curTime = 0f;
    float maxTime = 2f;
    bool isMove = false;
    Vector3 originPos;
    Quaternion originRot;
    public GameObject option;
    bool checkEnemy = false;
    public GameObject hpBar;
    public GameObject mpBar;
    void Start()
    {
        player = GameObject.Find("Player");
        cameraRig = GameObject.Find("CameraRig");
    }

    // Update is called once per frame
    void Update()
    {
        if(!checkEnemy)
        {
            Collider[] cols = Physics.OverlapSphere(isEnemy.transform.position, 20f);
            for (int i = 0; i < cols.Length; i++)
            {
                if (cols[i].tag == "Enemy")
                {
                    enemyCount++;
                }
                else if (cols[i].tag == "Player")
                {
                    isPlayer = true;
                }


            }
            if (enemyCount != 0)
            {
                enemyCount = 0;
            }
            else
            {
                if (isPlayer)
                {
                    startCameraMove = true;
                    setMainPos = true;
                    checkEnemy = true;
                }
            }
        }
        
        if(setMainPos)
        {
            originPos = cameraRig.transform.position;
            originRot = cameraRig.transform.rotation;
            setMainPos = false;

        }
        if(startCameraMove)
        {
            if (!isMove)
            {
                curTime += Time.deltaTime;
                if (curTime > maxTime)
                {
                    if (Imotal.instance.isKeyBorad == false)
                    {
                        UiInter.SetActive(false);
                    }
                    option.SetActive(false);
                    player.SetActive(false);
                    hpBar.SetActive(false);
                    mpBar.SetActive(false);
                    cameraRig.transform.position = Vector3.Lerp(cameraRig.transform.position, transform.position, 1f * Time.deltaTime);
                    cameraRig.transform.rotation = Quaternion.Lerp(cameraRig.transform.rotation, transform.rotation, 1f * Time.deltaTime);
                }
            }


            cameraRig.GetComponent<FollowCam>().enabled = false;
            
            if (Vector3.Distance(cameraRig.transform.position, transform.position)<0.2f)
            {
                isMove = true;
                Portal.SetActive(true);
               
                Invoke("EndMove", 2f);
            }
        }
        
        
    }
    
    void EndMove()
    {
        endCameraMove = true;
            cameraRig.GetComponent<FollowCam>().enabled = true;
        if (Imotal.instance.isKeyBorad == false)
        {
            UiInter.SetActive(true);
        }
        player.SetActive(true);
        option.SetActive(true);
        hpBar.SetActive(true);
        mpBar.SetActive(true);
        cameraRig.transform.position = originPos;
            cameraRig.transform.rotation = originRot;
            startCameraMove = false;
        
        
    }

}
