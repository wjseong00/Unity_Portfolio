using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option : MonoBehaviour
{
    public GameObject canvas;
    public GameObject canvas2;

    public void OpenOption()
    {
        Time.timeScale = 0;
        canvas.SetActive(true);
        canvas2.SetActive(true);
    }
    public void ExitOption()
    {
        Time.timeScale = 1;
        canvas.SetActive(false);
        canvas2.SetActive(false);
    }
}
