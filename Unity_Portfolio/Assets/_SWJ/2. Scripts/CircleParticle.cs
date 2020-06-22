using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CircleParticle : MonoBehaviour
{
    ParticleSystem parti;
    // Start is called before the first frame update
    void Start()
    {
        parti = GetComponent<ParticleSystem>();
        Invoke("stopParti", 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
    void stopParti()
    {
       
        parti.Pause();
    }
}
