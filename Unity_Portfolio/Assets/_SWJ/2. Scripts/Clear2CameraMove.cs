using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clear2CameraMove : MonoBehaviour
{
    public GameObject isEnemy;
    GameObject cameraRig;
    int enemyCount = 0;

    public GameObject UiInter;
    public GameObject KUiInter;
    public GameObject[] bridge;
    public GameObject smokeFactory;
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
                    if (Imotal.instance.isKeyBorad == false)
                    {
                        UiInter.SetActive(false);
                    }
                    else
                    {
                        KUiInter.SetActive(false);
                    }
                    hpBar.SetActive(false);

                    option.SetActive(false);
                    player.SetActive(false);
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
                    start =true;
                }
                
                

            }
        }
        

    }
    IEnumerator showBlock()
    {
        bridge[0].SetActive(true);
        GameObject smoke = Instantiate(smokeFactory);
        smoke.transform.position = bridge[0].transform.position;
        Destroy(smoke, 1f);
        yield return new WaitForSeconds(0.4f);
        bridge[1].SetActive(true);
        GameObject smoke1 = Instantiate(smokeFactory);
        smoke1.transform.position = bridge[1].transform.position;
        Destroy(smoke1, 1f);
        yield return new WaitForSeconds(0.4f);
        bridge[2].SetActive(true);
        GameObject smoke2 = Instantiate(smokeFactory);
        smoke2.transform.position = bridge[2].transform.position;
        Destroy(smoke2, 1f);
        yield return new WaitForSeconds(0.4f);
        bridge[3].SetActive(true);
        GameObject smoke3 = Instantiate(smokeFactory);
        smoke3.transform.position = bridge[3].transform.position;
        Destroy(smoke3, 1f);
        yield return new WaitForSeconds(0.4f);
        endCameraMove = true;
        
        cameraRig.GetComponent<FollowCam>().enabled = true;
        if(Imotal.instance.isKeyBorad== false)
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
        startCameraMove = false;


    }
  
    
}
