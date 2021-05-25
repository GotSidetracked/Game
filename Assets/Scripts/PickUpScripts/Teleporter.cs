using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleporter : MonoBehaviour
{
    //public variables
    public static GameObject[] enemies;

    //private variables
    private SpriteRenderer thisObject;
    private Sprite Stairs;
    private string currentScene;

    //When it spawns it checks how many enemies are on the level and as it updates checks if all monsters are dead
    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        thisObject = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        currentScene = SceneManager.GetActiveScene().name;
        if (enemies.Length == 0)
        {
            Stairs = Resources.Load<Sprite>("Stairs");
            thisObject.sprite = Stairs;
        }
        else
        {
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
        }
    }

    //When it sees there are no more enemies when the players enters it will send them to the next stage
    private void OnTriggerEnter2D(Collider2D Other)
    {
        if (Other.name == "Character" && enemies.Length == 0)
        {
            Debug.Log(SceneManager.GetActiveScene().name);
            if (currentScene == "CastleDungeonLevel7")
            {
                StartCoroutine(LoadEndScreen());
                Destroy(Other);
            }
            else
            {
                MovementAndScoring.isMoving = false;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
    //This will load to the end screen, did a timer because sometimes it wouldn't delete some objects if the stage changed too quickly
    IEnumerator LoadEndScreen()
    {
        yield return new WaitForSeconds(0.4f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
