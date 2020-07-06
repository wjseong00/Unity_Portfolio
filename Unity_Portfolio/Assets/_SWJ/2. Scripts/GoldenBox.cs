using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenBox : MonoBehaviour
{
    public GameObject itemFactory;
    GameObject player;
    Animation anim;
    AudioSource sound;
    bool isOpen = false;
    bool isNear = false;
    void Start()
    {
        player = GameObject.Find("Player");
        sound = GetComponent<AudioSource>();
        anim = GetComponent<Animation>();
    }
    private void Update()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, 0.5f);
        for (int i = 0; i < cols.Length; i++)
        {
            if (cols[i].tag == "Player")
            {
                isNear = true;
                if (player.GetComponent<PlayerMoney>().isKey == true)
                {
                   
                    if(Input.GetKeyDown(KeyCode.F)&&!isOpen)
                    {
                        sound.Play();
                        isOpen = true;
                        anim.Play();
                        GameObject item = Instantiate(itemFactory);
                        item.transform.position = transform.position + new Vector3(0, 1f, 0);
                        Invoke("closeBox", 0.8f);
                    }
                    

                }
                
            }
            else
            {
                isNear = false;
            }
        }
    }
    public void openBox()
    {
        if (!isOpen&&isNear)
        {
            sound.Play();
            isOpen = true;
            anim.Play();
            GameObject item = Instantiate(itemFactory);
            item.transform.position = transform.position + new Vector3(0, 1f, 0);
            Invoke("closeBox", 0.8f);
        }
    }
    void closeBox()
    {
        anim.Stop();
    }


}
