using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touched : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D Other)
    {
        Debug.Log("Touched");
    }
}
