using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option : MonoBehaviour
{
    public GameObject canvas;
    public GameObject canvas2;

    public void OpenOption()
    {
        Cursor.visible = true;
        Time.timeScale = 0;
        canvas.SetActive(true);
        canvas2.SetActive(true);
    }
    public void ExitOption()
    {
        Cursor.visible = false;
        Time.timeScale = 1;
        canvas.SetActive(false);
        canvas2.SetActive(false);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(canvas.activeSelf==false)
            {
                Cursor.visible = true;
                Time.timeScale = 0;
                canvas.SetActive(true);
                canvas2.SetActive(true);
            }
            
        }

    }

}
