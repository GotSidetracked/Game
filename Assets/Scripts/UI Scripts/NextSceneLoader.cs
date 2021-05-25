using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneLoader : MonoBehaviour
{

    //Loads the next qued up scene

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("Player Has Quit");
        Application.Quit();
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
