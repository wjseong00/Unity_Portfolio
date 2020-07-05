using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FadeIn : MonoBehaviour
{
    public Image fade;
    public Animator anim;
    float fades = 0.0f;
    float time = 0;
    public GameObject playerStartPos;
    public GameObject cameraRig;
    public GameObject player;
    public GameObject UiInter;
    public GameObject KUiInter;
    public GameObject camPos1;
    public GameObject joyStick;



    public GameObject hpBar;
    public GameObject option;

    void Update()
    {
        time += Time.deltaTime;
        if (fades < 1.0f && time >= 0.1f)
        {
            fades += 0.05f;
            fade.color = new Color(0, 0, 0, fades);
            time = 0f;
            cameraRig.GetComponent<FollowCam>().enabled = false;
            player.GetComponent<PlayerMove>().enabled = false;
            player.GetComponent<PlayerAttack>().enabled = false;
            joyStick.GetComponent<JoyStick>().Reset();
            joyStick.GetComponent<JoyStick>().enabled = false;
            
            hpBar.SetActive(false);

            option.SetActive(false);
            anim.SetBool("Run", false);
            anim.SetBool("Jump", false);
        }
        else if (fades >= 1.0f)
        {
            Camera.main.gameObject.SetActive(false);
            camPos1.SetActive(true);
            player.transform.position = playerStartPos.transform.position;

            if (Imotal.instance.isKeyBorad == false)
            {
                UiInter.SetActive(false);
            }
            else
            {
                KUiInter.SetActive(false);
            }
            player.GetComponent<PlayerBossScene>().enabled = true;
            GetComponent<FadeIn>().enabled = false;
            GetComponent<FadeOut>().enabled = true;
            time = 0f;
        }
    }
}
