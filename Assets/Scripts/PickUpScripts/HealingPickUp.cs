using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingPickUp : MonoBehaviour
{
    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D Other)
    {
        if (Other.name == "Character")
        {
            PlayerInfo.Heal(20);
            Destroy(gameObject);
        }
    }
}
