using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    //Information about the skills and how much damage do they do (Later on switched to writing monster skills inside the monsters instead)
    public static Structure.Damage GetInfo(string Name)
    {
        Structure.Damage ReturnInfo = new Structure.Damage();
        if (Name == "Slash")
        {
            ReturnInfo.damage = Convert.ToInt32(Math.Ceiling((double)PlayerInfo.Damage * 1.5));
            ReturnInfo.type = "Physical";
        }
        else if (Name == "GobSlash")
        {
            ReturnInfo.damage = 10;
            ReturnInfo.type = "Physical";
        }
        else if (Name == "Fireball")
        {
            ReturnInfo.damage = PlayerInfo.Damage;
            ReturnInfo.type = "Magical";
        }
        else if (Name == "Mine")
        {
            Debug.Log("Hit by mine");
            ReturnInfo.damage = PlayerInfo.Damage * 4;
            ReturnInfo.type = "Physical";
        }
        return ReturnInfo;
    }

    //Finds the correct resistance to use against damage type
    public static void MonsterTakeDamage(ref Structure.MonsterStats info, Structure.Damage damage)
    {
        if (damage.type == "Physical")
        {
            HpRecalculate(ref info, damage.damage, info.PhysDef);
        }
        if (damage.type == "Magical")
        {
            HpRecalculate(ref info, damage.damage, info.MagDef);
        }
        if (damage.type == "True")
        {
            HpRecalculate(ref info, damage.damage);
        }
    }
    //Sets the new hp for the monster
    public static void HpRecalculate(ref Structure.MonsterStats info, int damage,  int Defense = 0)
    {
        int calculatedDamage = damage - Defense;
        if (calculatedDamage < 0)
        {
            calculatedDamage = 0;
        }
        info.HP = info.HP - calculatedDamage;
    }
}
