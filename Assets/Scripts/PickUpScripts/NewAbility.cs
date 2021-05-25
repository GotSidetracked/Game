using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewAbility : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D Other)
    {
        if (Other.name == "Character")
        {
            PlayerInfo.GetNewAbility();
            Destroy(gameObject);
        }
    }
}
