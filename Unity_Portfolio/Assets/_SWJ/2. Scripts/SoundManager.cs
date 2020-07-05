using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource[] effect;
    public AudioClip notBossBg;
    public AudioClip BossBg;


    


    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    AudioSource bgAudio;
    
    public float bgVolume=1f;
    public float efVolume=0f;
    // Update is called once per frame
    private void Start()
    {
        bgAudio = GetComponent<AudioSource>();
        for (int i = 0; i < effect.Length; i++)
        {
            effect[i].GetComponent<AudioSource>();
        }
        
    }
    void Update()
    {
        if(BossShow.instance.bossStart ==false)
        {
            bgAudio.clip = notBossBg;
            if(!bgAudio.isPlaying)
            {
                bgAudio.Play();
            }
        }
        else if(BossShow.instance.bossStart == true)
        {
            bgAudio.clip = BossBg;
            if (!bgAudio.isPlaying)
            {
                bgAudio.Play();
            }
        }
        bgAudio.volume = bgVolume;
        for (int i = 0; i < effect.Length; i++)
        {
            effect[i].volume = efVolume;
        }
    }
}
