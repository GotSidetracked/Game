using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure : MonoBehaviour
{
    //Structures to make coding easier

    //The damage that the player and monsters take
    public struct Damage
    {
        public string type;
        public int damage;
    };
    
    //Monster statistics
    public struct MonsterStats
    {
        public int HP;
        public int PhysDef;
        public int MagDef;
        public int Attack;
        public int Iniative;
        public string[] skills;
    }

    //If the monster has found the player
    public struct didMonsterFind
    {
        public bool hasFound;
        public RaycastHit2D direction;
    }

}
