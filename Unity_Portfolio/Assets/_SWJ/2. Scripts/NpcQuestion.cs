using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class Dialogue
{
    [TextArea]
    public string dialogue;
}
public class NpcQuestion : MonoBehaviour
{
    public GameObject speak;
    public Transform camPos;
    GameObject cameraRig;
    GameObject player;

    public GameObject UiInter;
    public GameObject KUiInter;
    public GameObject hpBar;
    Vector3 temp;
    Quaternion temp1;
    
    [SerializeField] public Image DialogueBox;
    [SerializeField] public Button nextButton;
    [SerializeField] public Button exitButton;
    [SerializeField] public Button acceptButton;
    [SerializeField] public Button denyButton;
    [SerializeField] public Text txt_Dialogue;

    private bool isDialogue = false;
    public bool AcceptQuestion = false;
    public bool AcceptQuestion2 = false;
    public bool clear = false;
    public bool clear2 = false;
    private int count = 0;
    public bool isUse = false;
    bool nextQuestion = false;
    [SerializeField] private Dialogue[] dialogue;

    public void ShowDialogue()
    {
        OnOFF(true);
        if (AcceptQuestion && clear&&!AcceptQuestion2 && nextQuestion)
        {
            
            count = 5;
        }
        else if (AcceptQuestion && player.GetComponent<PlayerMoney>().isItem == true && !clear)
        {

            count = 4;
            Clear();
            clear = true;
        }
        else if(AcceptQuestion && player.GetComponent<PlayerMoney>().isItem == false)
        {
            count = 3;
        }
        else if (AcceptQuestion && clear && AcceptQuestion2 && player.GetComponent<PlayerMoney>().isBossKill == false)
        {
            count = 7;
        }
        else if (AcceptQuestion && clear && AcceptQuestion2 && player.GetComponent<PlayerMoney>().isBossKill == true)
        {
            count = 8;
        }
        else
        {
            count = 0;
        }
        
       
        NextDialogue();
    }

    public void NextDialogue()
    {
        txt_Dialogue.text = dialogue[count].dialogue;
        count++;
    }

    public void Next()
    {
        
        if (AcceptQuestion && clear && !AcceptQuestion2 && nextQuestion)
        {
            if (count < 7)
            {

                NextDialogue();
            }
        }
        
        else if (AcceptQuestion && player.GetComponent<PlayerMoney>().isItem == true && !clear)
        {
            if (count < 4)
            {
                
                NextDialogue();
            }
        }
        else if (AcceptQuestion && player.GetComponent<PlayerMoney>().isItem == false)
        {
            if (count < 3)
            {
                NextDialogue();
            }
        }
        else if (AcceptQuestion && clear && AcceptQuestion2 && player.GetComponent<PlayerMoney>().isBossKill == false)
        {
            if (count < 7)
            {

                NextDialogue();
            }
        }
        else if (AcceptQuestion && clear && AcceptQuestion2 && player.GetComponent<PlayerMoney>().isBossKill == true)
        {
            if (count < 8)
            {

                NextDialogue();
            }
        }
        else
        {
            if (count < 3)
            {
                NextDialogue();
            }
        }

    }
    public void Clear()
    {
        ItemDatabase.instance.money += 5000;

    }

    public void Deny()
    {
        OnOFF(false);
        isUse = false;
        hpBar.SetActive(true);
        Time.timeScale = 1f;
        player.SetActive(true);
        if (Imotal.instance.isKeyBorad == false)
        {
            UiInter.SetActive(true);
        }
        else
        {
            Cursor.visible = false;
            KUiInter.SetActive(true);
        }
        cameraRig.GetComponent<FollowCam>().enabled = true;
        Camera.main.transform.position = temp;
        Camera.main.transform.rotation = temp1;
    }
    public void Accept()
    {
        if (AcceptQuestion &&clear)
        {
            AcceptQuestion2 = true;
        }
        AcceptQuestion =true;

        isUse = false;
        OnOFF(false);
        hpBar.SetActive(true);
        Time.timeScale = 1f;
        player.SetActive(true);
        if (Imotal.instance.isKeyBorad == false)
        {
            UiInter.SetActive(true);
        }
        else
        {
            Cursor.visible = false;
            KUiInter.SetActive(true);
        }

        cameraRig.GetComponent<FollowCam>().enabled = true;
        Camera.main.transform.position = temp;
        Camera.main.transform.rotation = temp1;
    }

    private void OnOFF(bool _flag)
    {
        DialogueBox.gameObject.SetActive(_flag);
        txt_Dialogue.gameObject.SetActive(_flag);
        nextButton.gameObject.SetActive(_flag);
        exitButton.gameObject.SetActive(_flag);
        if(!AcceptQuestion)
        {
            acceptButton.gameObject.SetActive(_flag);
            denyButton.gameObject.SetActive(_flag);
        }
        else
        {
            if(AcceptQuestion && !AcceptQuestion2 && !clear)
            {
                acceptButton.gameObject.SetActive(false);
                denyButton.gameObject.SetActive(false);
            }
            else if (AcceptQuestion && !AcceptQuestion2 && clear)
            {
                acceptButton.gameObject.SetActive(_flag);
                denyButton.gameObject.SetActive(_flag);
            }
            else
            {
                acceptButton.gameObject.SetActive(false);
                denyButton.gameObject.SetActive(false);
            }
            
        }
        
        isDialogue = _flag;
    }

    public void HideDialogue()
    {
        OnOFF(false);
        isUse = false;
        
        hpBar.SetActive(true);
        Time.timeScale = 1f;
        player.SetActive(true);
        if (Imotal.instance.isKeyBorad == false)
        {
            UiInter.SetActive(true);
        }
        else
        {
            Cursor.visible = false;
            KUiInter.SetActive(true);
        }
        cameraRig.GetComponent<FollowCam>().enabled = true;
        Camera.main.transform.position = temp;
        Camera.main.transform.rotation = temp1;
        if(clear)
        {
            nextQuestion = true;
        }
       
    }


    void Start()
    {
        
        player = GameObject.Find("Player");
        cameraRig = GameObject.Find("CameraRig");
    }
    private void Update()
    {
        if(Imotal.instance.isKeyBorad==true)
        {
            if (speak.activeSelf == true)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    isUse = true;
                    temp = Camera.main.transform.position;
                    temp1 = Camera.main.transform.rotation;
                    Time.timeScale = 0;
                    hpBar.SetActive(false);
                    if (Imotal.instance.isKeyBorad == false)
                    {
                        UiInter.SetActive(false);
                    }
                    else
                    {
                        KUiInter.SetActive(false);
                    }
                    Cursor.visible = true;
                    player.SetActive(false);
                    speak.SetActive(false);
                    Camera.main.transform.position = camPos.transform.position;
                    Camera.main.transform.rotation = camPos.transform.rotation;
                    cameraRig.GetComponent<FollowCam>().enabled = false;

                    ShowDialogue();


                }


            }
        }
        
        
    }
    public void isQuestion()
    {
        if (speak.activeSelf == true)
        {
            isUse = true;
            hpBar.SetActive(false);
            temp = Camera.main.transform.position;
                temp1 = Camera.main.transform.rotation;
                Time.timeScale = 0;
            if (Imotal.instance.isKeyBorad == false)
            {
                UiInter.SetActive(false);
            }
            else
            {
                KUiInter.SetActive(false);
            }
            player.SetActive(false);
                speak.SetActive(false);
                Camera.main.transform.position = camPos.transform.position;
                Camera.main.transform.rotation = camPos.transform.rotation;
                cameraRig.GetComponent<FollowCam>().enabled = false;

                ShowDialogue();


            


        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            speak.SetActive(true);
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            speak.SetActive(false);
        }
    }
}
