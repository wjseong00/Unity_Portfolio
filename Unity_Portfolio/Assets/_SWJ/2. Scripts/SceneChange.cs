using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
public class SceneChange : MonoBehaviour
{
    public void gameStart()
    {
        SceneManager.LoadScene("gameScene");
    }
    public void gameExit()
    {
        
        //EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
