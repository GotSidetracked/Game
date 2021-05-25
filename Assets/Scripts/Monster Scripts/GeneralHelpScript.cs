using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralHelpScript : MonoBehaviour
{
   
    static public Structure.MonsterStats GetCorrectStats(GameObject target)
    {
        if (target.name[0] == 'G')
        {
            return target.GetComponent<GoblinScript>().Stats;
        }
        else if (target.name[0] == 'E')
        {
            return target.GetComponent<FlyingEye>().Stats;
        }
        else
        {
            return target.GetComponent<MushroomScript>().Stats;
        }
    }

    static public void SetTurn(GameObject target)
    {
        if (target.name[0] == 'G')
        {
            target.GetComponent<GoblinScript>().isTurn = true;
        }
        else if (target.name[0] == 'E')
        {
            target.GetComponent<FlyingEye>().isTurn = true;
        }
        else
        {
            target.GetComponent<MushroomScript>().isTurn = true;
        }
    }
}
