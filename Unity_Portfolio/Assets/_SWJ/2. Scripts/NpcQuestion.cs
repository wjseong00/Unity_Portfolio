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
    public bool clear = false;
    private int count = 0;

    [SerializeField] private Dialogue[] dialogue;

    public void ShowDialogue()
    {
        OnOFF(true);
        if (AcceptQuestion && clear)
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
        
        if (AcceptQuestion && clear)
        {
            if (count < 5)
            {
                NextDialogue();
            }
        }
        else if (AcceptQuestion && player.GetComponent<PlayerMoney>().isItem == true)
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
        player.GetComponent<PlayerMoney>().coinCount += 50;

    }

    public void Deny()
    {
        OnOFF(false);
        Time.timeScale = 1f;
        player.SetActive(true);
        UiInter.SetActive(true);
        cameraRig.GetComponent<FollowCam>().enabled = true;
        Camera.main.transform.position = temp;
        Camera.main.transform.rotation = temp1;
    }
    public void Accept()
    {
        AcceptQuestion=true;
        OnOFF(false);
        Time.timeScale = 1f;
        player.SetActive(true);
        UiInter.SetActive(true);
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
            acceptButton.gameObject.SetActive(false);
            denyButton.gameObject.SetActive(false);
        }
        
        isDialogue = _flag;
    }

    public void HideDialogue()
    {
        OnOFF(false);
        Time.timeScale = 1f;
        player.SetActive(true);
         UiInter.SetActive(true);
        cameraRig.GetComponent<FollowCam>().enabled = true;
        Camera.main.transform.position = temp;
        Camera.main.transform.rotation = temp1;
       
    }


    void Start()
    {
        
        player = GameObject.Find("Player");
        cameraRig = GameObject.Find("CameraRig");
    }
    private void Update()
    {
        if(speak.activeSelf==true)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                
                temp = Camera.main.transform.position;
                temp1 = Camera.main.transform.rotation;
                Time.timeScale = 0;
                UiInter.SetActive(false);
                player.SetActive(false);
                speak.SetActive(false);
                Camera.main.transform.position = camPos.transform.position;
                Camera.main.transform.rotation = camPos.transform.rotation;
                cameraRig.GetComponent<FollowCam>().enabled = false;
                
                ShowDialogue();
                
               
            }
            
            
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
