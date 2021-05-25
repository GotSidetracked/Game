using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D Other)
    {
        if (Other.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
