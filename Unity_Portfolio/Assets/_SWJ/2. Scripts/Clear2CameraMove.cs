using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clear2CameraMove : MonoBehaviour
{
    public GameObject isEnemy;
    GameObject cameraRig;
    int enemyCount = 0;

    public GameObject UiInter;
    public GameObject[] bridge;
    GameObject player;

    public bool startCameraMove = false;
    public bool endCameraMove = false;
    bool setMainPos = false;
    Vector3 originPos;
    Quaternion originRot;
    int count = 0;
    bool checkEnemy = false;
    bool isPlayer = false;
    float curTime = 0f;
    float maxTime = 2f;
    bool isMove = false;
    void Start()
    {
        player = GameObject.Find("Player");
        cameraRig = GameObject.Find("CameraRig");
    }

    // Update is called once per frame
    void Update()
    {
        if (!checkEnemy)
        {
            Collider[] cols = Physics.OverlapSphere(isEnemy.transform.position, 20f);
            for (int i = 0; i < cols.Length; i++)
            {

                if (cols[i].tag == "Enemy")
                {
                    enemyCount++;
                }
                else if (cols[i].tag=="Player")
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
                if(isPlayer)
                {
                    startCameraMove = true;
                    setMainPos = true;
                    checkEnemy = true;
                }
                
            }
        }

        if (setMainPos)
        {
            originPos = cameraRig.transform.position;
            originRot = cameraRig.transform.rotation;
            
            setMainPos = false;

        }
        if (startCameraMove)
        {
            if(!isMove)
            {
                curTime += Time.deltaTime;
                if (curTime > maxTime)
                {
                    UiInter.SetActive(false);
                    player.SetActive(false);
                    cameraRig.transform.position = Vector3.Lerp(cameraRig.transform.position, transform.position, 0.5f * Time.deltaTime);
                    cameraRig.transform.rotation = Quaternion.Lerp(cameraRig.transform.rotation, transform.rotation, 0.5f * Time.deltaTime);
                }
            }
            

            cameraRig.GetComponent<FollowCam>().enabled = false;

            if (Vector3.Distance(cameraRig.transform.position, transform.position) < 0.2f)
            {
                isMove = true;
                StartCoroutine(showBlock());
                

            }
        }
        

    }
    IEnumerator showBlock()
    {
        bridge[0].SetActive(true);
        yield return new WaitForSeconds(0.4f);
        bridge[1].SetActive(true);
        yield return new WaitForSeconds(0.4f);
        bridge[2].SetActive(true);
        yield return new WaitForSeconds(0.4f);
        bridge[3].SetActive(true);
        yield return new WaitForSeconds(0.4f);
        endCameraMove = true;
        
        cameraRig.GetComponent<FollowCam>().enabled = true;
        UiInter.SetActive(true);
        player.SetActive(true);
        
        cameraRig.transform.position = originPos;
        cameraRig.transform.rotation = originRot;
        startCameraMove = false;


    }
  
    
}
