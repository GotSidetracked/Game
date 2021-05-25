using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityScript : MonoBehaviour
{
    /// <summary>
    /// Script for loading ability icons from resources to the hot bar
    /// </summary>

    private SpriteRenderer thisObject;
    private int thisAbility;
    private Sprite AbilityIcon;
    
    void Start()
    {
        thisObject = gameObject.GetComponent<SpriteRenderer>();
        //Gets which ability number it is
        thisAbility = Convert.ToInt32(gameObject.name.ToString().Split(' ')[1]);
    }

    void Update()
    {
        if (thisAbility == 1)
        {
            if (PlayerInfo.ability1 != "")
            {
                AbilityIcon = Resources.Load<Sprite>("Abilities/"+ PlayerInfo.ability1);
                thisObject.sprite = AbilityIcon;    
            }
        }
        if (thisAbility == 2)
        {
            if (PlayerInfo.ability2 != "")
            {
                AbilityIcon = Resources.Load<Sprite>("Abilities/" + PlayerInfo.ability2);
                thisObject.sprite = AbilityIcon;
            }
        }
        if (thisAbility == 3)
        {
            if (PlayerInfo.ability3 != "")
            {
                AbilityIcon = Resources.Load<Sprite>("Abilities/" + PlayerInfo.ability3);
                thisObject.sprite = AbilityIcon;
            }
        }
        if (thisAbility == 4)
        {
            if (PlayerInfo.ability4 != "")
            {
                AbilityIcon = Resources.Load<Sprite>("Abilities/" + PlayerInfo.ability4);
                thisObject.sprite = AbilityIcon;
            }
        }
        if (thisAbility == 5)
        {
            if (PlayerInfo.ability5 != "")
            {
                AbilityIcon = Resources.Load<Sprite>("Abilities/" + PlayerInfo.ability5);
                thisObject.sprite = AbilityIcon;
            }
        }
        if (thisAbility == 6)
        {
            if (PlayerInfo.ability6 != "")
            {
                AbilityIcon = Resources.Load<Sprite>("Abilities/" + PlayerInfo.ability6);
                thisObject.sprite = AbilityIcon;
            }
        }
    }
}
