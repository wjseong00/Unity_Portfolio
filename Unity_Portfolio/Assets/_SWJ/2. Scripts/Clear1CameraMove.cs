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
    Vector3 originPos;
    Quaternion originRot;

    bool checkEnemy = false;

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



            }
            if (enemyCount != 0)
            {
                enemyCount = 0;
            }
            else
            {
                startCameraMove = true;
                setMainPos = true;
                checkEnemy = true;
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
            Invoke("StartMove", 2f);
            
            cameraRig.GetComponent<FollowCam>().enabled = false;
            
            if (Vector3.Distance(cameraRig.transform.position, transform.position)<0.2f)
            {
                Portal.SetActive(true);
               
                Invoke("EndMove", 2f);
            }
        }
        
        
    }
    void StartMove()
    {
        UiInter.SetActive(false);
        player.SetActive(false);
        cameraRig.transform.position = Vector3.Lerp(cameraRig.transform.position, transform.position, 0.5f * Time.deltaTime);
        cameraRig.transform.rotation = Quaternion.Lerp(cameraRig.transform.rotation, transform.rotation, 0.5f * Time.deltaTime);
    }
    void EndMove()
    {
        endCameraMove = true;
            cameraRig.GetComponent<FollowCam>().enabled = true;
        UiInter.SetActive(true);
        player.SetActive(true);
        cameraRig.transform.position = originPos;
            cameraRig.transform.rotation = originRot;
            startCameraMove = false;
        
        
    }

}
