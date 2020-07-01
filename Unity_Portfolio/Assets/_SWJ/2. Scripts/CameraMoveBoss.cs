using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveBoss : MonoBehaviour
{
    float curTime = 0;
    float curTime2 = 0;
    float limitTime = 2f;
    public GameObject isClear;
    public GameObject upBlack;
    public GameObject downBlack;

    void Update()
    {
        if (Inventory.instance.bossShow == true)
        {

            curTime += Time.deltaTime;
            if (curTime < limitTime)
            {
                upBlack.transform.Translate(Vector3.down * 25 * Time.deltaTime);
                downBlack.transform.Translate(Vector3.up * 25 * Time.deltaTime);
            }

        }
        else if (Inventory.instance.bossEnd == true)
        {
            curTime2 += Time.deltaTime;
            if (curTime2 < limitTime)
            {
                upBlack.transform.Translate(Vector3.up * 25 * Time.deltaTime);
                downBlack.transform.Translate(Vector3.down * 25 * Time.deltaTime);
            }


        }

    }
}
