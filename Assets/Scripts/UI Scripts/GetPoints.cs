using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetPoints : MonoBehaviour
{
    //Used to show how many points the player has currently
    private int OldScore;
    private Text myText;

    void Start()
    {
        OldScore = 0;
        myText = GameObject.Find("PointGUi").GetComponent<Text>();
        myText.text = "Points - " + PlayerInfo.Points + "!";

    }

    void Update()
    {
        if (OldScore != PlayerInfo.Points)
        {
            OldScore = PlayerInfo.Points;
            myText.text = "Points - " + PlayerInfo.Points + "!";
            Debug.Log("New Points");
        }
    }
}
