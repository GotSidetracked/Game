using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodMode : MonoBehaviour
{
    //Bool value that is saved to set different stats to the player on load
    public static bool GodModeEnabled;
    void Start()
    {
        GodModeEnabled = false;
    }

    public void TurnOnGodMode()
    {
        Debug.Log("GodMode Enabled");
        GodModeEnabled = true;
    }

    void Update()
    {
        
    }
}
