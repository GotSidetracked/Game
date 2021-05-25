using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{
    /// <summary>
    /// Stops the gameobject being destroyed on load to a different scene.
    /// </summary>

    string currentScene;
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "EndScreen" || currentScene == "Main Menu")
        {
            Destroy(this.gameObject);
        }
    }
}
