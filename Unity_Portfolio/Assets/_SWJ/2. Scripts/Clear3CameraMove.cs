using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clear3CameraMove : MonoBehaviour
{
    public GameObject isEnemy;
    GameObject cameraRig;
    int enemyCount = 0;

    public GameObject UiInter;
    public GameObject KUiInter;
    public GameObject[] bridge;
    public GameObject smokeFactory;
    GameObject player;
    public GameObject joyStick;
    public GameObject joyStick2;
    public bool startCameraMove = false;
    public bool endCameraMove = false;
    bool setMainPos = false;
    Vector3 originPos;
    Quaternion originRot;
    int count = 0;
    bool checkEnemy = false;
    float curTime = 0f;
    float maxTime = 2f;
    bool isMove = false;
    bool isPlayer = false;
    bool start = false;
    public GameObject option;
    public GameObject hpBar;

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
            Collider[] cols = Physics.OverlapSphere(isEnemy.transform.position, 10f);
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

        if (setMainPos)
        {
            originPos = cameraRig.transform.position;
            originRot = cameraRig.transform.rotation;
            setMainPos = false;

        }
        if (startCameraMove)
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
                    else
                    {
                        KUiInter.SetActive(false);
                    }
                    option.SetActive(false);
                    player.SetActive(false);
                    hpBar.SetActive(false);
                    joyStick.GetComponent<JoyStick>().Reset();
                    joyStick2.GetComponent<CameraJoyStick>().Reset();
                    cameraRig.transform.position = Vector3.Lerp(cameraRig.transform.position, transform.position, 1f * Time.deltaTime);
                    cameraRig.transform.rotation = Quaternion.Lerp(cameraRig.transform.rotation, transform.rotation, 1f * Time.deltaTime);
                }
            }


            cameraRig.GetComponent<FollowCam>().enabled = false;

            if (Vector3.Distance(cameraRig.transform.position, transform.position) < 0.2f)
            {
                isMove = true;
                if(!start)
                {
                    StartCoroutine(showBlock());
                    start = true;
                }
                


            }
        }


    }
    IEnumerator showBlock()
    {
        for(int i = 0; i <16; i++)
        {
            bridge[i].SetActive(true);
            GameObject smoke = Instantiate(smokeFactory);
            smoke.transform.position = bridge[i].transform.position;
            Destroy(smoke,1f);
            yield return new WaitForSeconds(0.2f);
            if(i==15)
            {
                Invoke("EndMove", 0.5f);
                yield return new WaitForSeconds(0.2f);
            }
        }
        
       

    }
    
    void EndMove()
    {
        startCameraMove = false;
        endCameraMove = true;
        cameraRig.GetComponent<FollowCam>().enabled = true;
        if (Imotal.instance.isKeyBorad == false)
        {
            UiInter.SetActive(true);
        }
        else
        {
            KUiInter.SetActive(true);
        }
        player.SetActive(true);
        option.SetActive(true);
        hpBar.SetActive(true);
        cameraRig.transform.position = originPos;
        cameraRig.transform.rotation = originRot;



    }
}
