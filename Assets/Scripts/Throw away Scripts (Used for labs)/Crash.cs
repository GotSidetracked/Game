using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crash : MonoBehaviour
{
    /// <summary>
    /// Script used for lab works
    /// </summary>

    public AudioSource audio;

    private void OnTriggerEnter2D(Collider2D Other)
    {
        audio.Play();
    }
}
