using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChange : MonoBehaviour
{
    public void gameStart()
    {
        SceneManager.LoadScene("gameScene");
    }
    public void gameExit()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
