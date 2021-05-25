using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{

    /// <summary>
    /// Sends the player back to the menu
    /// </summary>
    
    public void ToMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

}
