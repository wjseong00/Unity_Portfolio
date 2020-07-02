using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CoolTime : MonoBehaviour
{
    public Image image;
    public Button button;
    public float coolTime = 10.0f;
    public bool isClicked = false;
    float leftTime = 10.0f;
    float speed = 5.0f;

    // Update is called once per frame
    void Update()
    {

        if (isClicked)
            if (leftTime > 0)
            {
                leftTime -= Time.deltaTime * speed;
                if (leftTime < 0)
                {
                    leftTime = 0;
                    if (button)
                        button.enabled = true;
                    isClicked = true;
                    image.gameObject.SetActive(false);
                }

                float ratio = (leftTime / coolTime);
                if (image)
                    image.fillAmount = ratio;
            }
    }

    public void StartCoolTime()
    {
        image.gameObject.SetActive(true);
        leftTime = coolTime;
        isClicked = true;
        if (button)
            button.enabled = false; // 버튼 기능을 해지함.
    }

   
}
