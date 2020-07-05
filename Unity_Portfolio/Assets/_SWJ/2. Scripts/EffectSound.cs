using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EffectSound : MonoBehaviour
{
     Slider slider;
     private void Start()
     {
         slider = GetComponent<Slider>();
     }
     private void Update()
     {
         SoundManager.instance.efVolume = slider.value;
     }

}
