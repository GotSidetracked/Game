using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameOverInfo : MonoBehaviour
{
    /// <summary>
    /// Shows how many points the player has on the end screen
    /// </summary>

    private Text myText;

    // Start is called before the first frame update
    void Start()
    {
        myText = GameObject.Find("EndScreenText").GetComponent<Text>();
        myText.text = "Points - " + PlayerInfo.Points + "!";
    }
}
