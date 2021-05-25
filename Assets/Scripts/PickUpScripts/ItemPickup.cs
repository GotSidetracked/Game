using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    //Gives the player a random boost in a random stat
    private void OnTriggerEnter2D(Collider2D Other)
    {
        if (Other.name == "Character")
        {
            int WhatIncrease = Random.Range(0, 5);
            Debug.Log(WhatIncrease);
            if (WhatIncrease == 0)
            {
                PlayerInfo.Iniative += Random.Range(2, 5);
            }
            if (WhatIncrease == 1)
            {
                PlayerInfo.PhysDefense += Random.Range(1, 4);
            }
            if (WhatIncrease == 2)
            {
                PlayerInfo.maxHP += Random.Range(20, 51);
            }
            if (WhatIncrease == 3)
            {
                PlayerInfo.Damage += Random.Range(5, 11);
            }
            if (WhatIncrease == 4)
            {
                PlayerInfo.MagicDefense += Random.Range(1, 4);
            }
            Destroy(gameObject);
        }
    }
}
