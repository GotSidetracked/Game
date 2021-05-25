using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnCalculator : MonoBehaviour
{
    /// <summary>
    /// This class works by getting the iniative of all creatures and every update when its not someone elses turn adding it to their specific iniative pool
    /// when their iniative array reaches 1000 their turn starts, if its the players turn the process stops until the player chooses an action to do
    /// </summary>
    // 0 - player, everything else monsters
    public static float[] iniativeArray = new float[100];
    public static GameObject[] enemies;
    public static int OneTurn;
    public static bool isPlayersTurn;
    public static bool isStopped;
  
    private static int enemyCount;



    void Start()
    {
        isPlayersTurn = true;
        isStopped = false;
        OneTurn = 1000;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        iniativeArray[0] = 0;
        enemyCount = 1;
        foreach (GameObject enemy in enemies)
        {
            iniativeArray[enemyCount] = 0;
            enemyCount += 1;
        }
    }

    public static void GetMonsters()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemyCount = 1;
        foreach (GameObject enemy in enemies)
        {
            iniativeArray[enemyCount] = 0;
            enemyCount += 1;
        }
    }

    void Update()
    {
        if (isPlayersTurn == false && isStopped == false)
        {
            iniativeArray[0] = iniativeArray[0] + PlayerInfo.Iniative;
            if (iniativeArray[0] >= OneTurn)
            {
                iniativeArray[0] = 0;
                isPlayersTurn = true;
            }
            enemyCount = 1;
            foreach (GameObject enemy in enemies)
            {
                if (enemy != null)
                {
                    Structure.MonsterStats tempStats = GeneralHelpScript.GetCorrectStats(enemy);
                    int timer = tempStats.Iniative;
                    //Debug.Log(timer);
                    iniativeArray[enemyCount] += timer;
                    //Debug.Log(iniativeArray[enemyCount]);
                    if (iniativeArray[enemyCount] >= OneTurn)
                    {
                        isStopped = true;
                        GeneralHelpScript.SetTurn(enemy);
                        iniativeArray[enemyCount] = 0;
                    }
                }
                enemyCount += 1;
            }
        }
    }
}
