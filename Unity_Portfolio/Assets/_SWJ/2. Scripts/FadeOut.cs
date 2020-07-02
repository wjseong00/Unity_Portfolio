using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FadeOut : MonoBehaviour
{
    public Image fade;
    float fades= 1.0f;
    float time = 0;

    
    void Update()
    {
        time += Time.deltaTime;
        if (fades > 0.0f && time >= 0.1f)
        {
            fades -= 0.05f;
            fade.color = new Color(0, 0, 0, fades);
            time = 0f;
        }
        else if (fades <= 0.0f)
        {

            time = 0f;
        }
    }
}
