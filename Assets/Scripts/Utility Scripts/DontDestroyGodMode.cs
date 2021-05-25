using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyGodMode : MonoBehaviour
{
    /// <summary>
    /// Special script made to not destroy the God Mode object until the player reaches the end
    /// Created because God mode is enabled in Menu and it would get instantly deleted if used the usual don't destroy game object script
    /// </summary>

    private string currentScene;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "EndScreen")
        {
            Destroy(this.gameObject);
        }
    }
}
