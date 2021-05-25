using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPIncreasePickup : MonoBehaviour
{
    //Increases the player max hp
    private void OnTriggerEnter2D(Collider2D Other)
    {
        if (Other.name == "Character")
        {
            PlayerInfo.maxHP += 10;
            Destroy(gameObject);
        }
    }
}
