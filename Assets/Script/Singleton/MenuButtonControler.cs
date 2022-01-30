using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonControler : MonoBehaviour
{
    public void OnPlayClicked()
    {
        Debug.Log("click");
        //SceneManager.LoadScene();
    }
    
    public void OnExitClicked()
    {
        Application.Quit();
    }

    public void OnMenuClicked()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
