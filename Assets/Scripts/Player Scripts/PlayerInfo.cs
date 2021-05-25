using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInfo : MonoBehaviour
{
    /// <summary>
    /// Storing player information
    /// All variables have to be static to keep them between stages
    /// Stats are the character statistics (Like in a typical RPG)
    /// Flags are used for items and abilities
    /// Abilities are called from the Attack script
    /// </summary>


    //Public stat variables
    public static int Points;
    public static int maxHP;
    public static int HP;
    public static int Damage;
    public static float Iniative;
    public static int PhysDefense;
    public static int MagicDefense;

    //Combat based public variables
    public static string ability1;
    public static string ability2;
    public static string ability3;
    public static string ability4;
    public static string ability5;
    public static string ability6;
    public static bool abilityHasChanged;

    private static string ability1Color;
    private static string ability2Color;
    private static string ability3Color;
    private static string ability4Color;
    private static string ability5Color;
    private static string ability6Color;
    string currentScene;
    //Private Flages  (Item system still to be done)
    private static bool isDodging;

    //Private Variables for classes and abilities
    private static string[] AllAbilities = { "Slash", "Fireball", "Mine", "Sacrifice", "Berserk", "Random" };
    private static int GreenBonus = 0;
    private static int BlueBonus = 0;
    private static int RedBonus = 0;
    private static bool SetNewAbilityPlace = false;
    private static string QuedAbility = "";

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        abilityHasChanged = true;
        if (GodMode.GodModeEnabled == true)
        {
            SetOPStats();
            SetOPFlags();
            SetOPAbilities();
        }
        else {
            SetBaseStats();
            SetBaseAbilities();
            SetBaseFlags();

        }
    }

    //Scripts that run at the start to set the stats and bools for the character

    void SetBaseStats()
    {
        maxHP = 100;
        Points = 0;
        HP = maxHP;
        Damage = 10;
        Iniative = 5;
        PhysDefense = 2;
        MagicDefense = 2;
    }

    void SetBaseAbilities()
    {
        ability1 = "Slash";
        ability2 = "";
        ability3 = "";
        ability4 = "";
        ability5 = "";
        ability6 = "";
    }

    void SetBaseFlags()
    {
        isDodging = false;
    }

    void SetOPStats()
    {
        maxHP = 5000;
        Points = 0;
        HP = maxHP;
        Damage = 10;
        Iniative = 40;
        PhysDefense = 2;
        MagicDefense = 2;
    }

    void SetOPFlags()
    {
        isDodging = true;
    }

    void SetOPAbilities()
    {
        ability1 = "Slash";
        ability2 = "Fireball";
        ability3 = "Mine";
        ability4 = "Sacrifice";
        ability5 = "Berserk";
        ability6 = "Random";
    }


    //Functions used in Update
    void Update()
    {
        //Makes the character delete itself if it reaches the Menu or Endscreens 
        currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "EndScreen" || currentScene == "Main Menu")
        {
            Destroy(this.gameObject);
        }
        if (SetNewAbilityPlace == true)
        {
            SetAbility(QuedAbility);
        }
        if (abilityHasChanged == true)
        {
            ResetBonuses();
            GetColors(ability1, ability1Color);
            GetColors(ability2, ability2Color);
            GetColors(ability3, ability3Color);
            GetColors(ability4, ability4Color);
            GetColors(ability5, ability5Color);
            GetColors(ability6, ability6Color);
            GetBonuses();
            abilityHasChanged = false;
        }
    }

//Methods for a basic class system
    void GetBonuses()
    {
        CalculateRed();
        CalculateBlue();
        CalculateGreen();
        if (RedBonus > 0)
        {
            maxHP = maxHP + (10 * RedBonus);
            PhysDefense = PhysDefense + (5 * RedBonus);
            MagicDefense = MagicDefense + (5 * RedBonus);
        }
        if (BlueBonus > 0)
        {
            Damage = Damage + (10 * BlueBonus);
        }
        if (GreenBonus > 0)
        {
            Iniative = Iniative + (15 * GreenBonus);
        }
    }

    //Calculates how many of each color bonuses the player has
    void CalculateRed()
    {
        if (BothRed(ability1Color, ability2Color))
        {
            RedBonus += 1;
        }
        if (BothRed(ability3Color, ability4Color))
        {
            RedBonus += 1;
        }
        if (BothRed(ability5Color, ability6Color))
        {
            RedBonus += 1;
        }
    }

    void CalculateBlue()
    {
        if (BothBlue(ability1Color, ability2Color))
        {
            BlueBonus += 1;
        }
        if (BothBlue(ability3Color, ability4Color))
        {
            BlueBonus += 1;
        }
        if (BothBlue(ability5Color, ability6Color))
        {
            BlueBonus += 1;
        }
    }

    void CalculateGreen()
    {
        if (BothGreen(ability1Color, ability2Color))
        {
            GreenBonus += 1;
        }
        if (BothGreen(ability3Color, ability4Color))
        {
            GreenBonus += 1;
        }
        if (BothGreen(ability5Color, ability6Color))
        {
            GreenBonus += 1;
        }
    }

    //Checks if the colors of the first/second, third/fouth and fifth/sixth ability are the same
    bool BothRed(string fColor, string sColor)
    {
        if (fColor == "Red" && sColor == "Red")
        {
            return true;
        }
        else
            return false;
    }

    bool BothBlue(string fColor, string sColor)
    {
        if (fColor == "Blue" && sColor == "Blue")
        {
            return true;
        }
        else
            return false;
    }

    bool BothGreen(string fColor, string sColor)
    {
        if (fColor == "Green" && sColor == "Green")
        {
            return true;
        }
        else
            return false;
    }
 
    //Resets the bonuses gained from abilities
    void ResetBonuses()
    {
        if (RedBonus > 0)
        {
            maxHP = maxHP - (10 * RedBonus);
            PhysDefense = PhysDefense - (5 * RedBonus);
            MagicDefense = MagicDefense - (5 * RedBonus);
        }
        if (BlueBonus > 0)
        {
            Damage = Damage - (10 * BlueBonus);
        }
        if (GreenBonus > 0)
        {
            Iniative = Iniative - (15 * GreenBonus);
        }
    }
    
    //Ability colors 
    void GetColors(string ability, string color)
    {
        if (ability == "Fireball" || ability == "Random")
        {
            color = "Blue";
        }
        if (ability == "Slash" || ability == "Mine")
        {
            color = "Green";
        }
        if (ability == "Berserk" || ability == "Sacrifice")
        {
            color = "Red";
        }
    }


    //Damage taken functions
    public static void TakeDamage(int RawDamage, string type)
    {
        if (isDodging == true)
        {
            int chance = UnityEngine.Random.Range(1, 5);
            if (chance == 5)
            {
                RawDamage = 0;
                type = "Dodged";
            }
        }
        if (type == "Physical")
        {
            HpRecalculate(RawDamage, PhysDefense);
        }
        if (type == "Magical")
        {
            HpRecalculate(RawDamage, MagicDefense);
        }
        if (type == "True")
        {
            HpRecalculate(RawDamage);
        }
    }

    private static void HpRecalculate(int RawDamage, int defense = 0)
    {
        int calculatedDamage = RawDamage - defense;
        if (calculatedDamage < 0)
        {
            calculatedDamage = 0;
        }
        HP = HP - calculatedDamage;
        
        if (HP <= 0)
        {
            SceneManager.LoadScene("EndScreen");
        }
    }

    //Healing function
    public static void Heal(int ammount)
    {
        if ((HP + ammount) > maxHP)
        {
            HP = maxHP;
        }
        else
            HP += ammount;
    }

    //Functions for finding and placing abilities
    public static void GetNewAbility()
    {
        int abilitySpot = UnityEngine.Random.Range(0, AllAbilities.Length);
        abilityHasChanged = true;
        Debug.Log(AllAbilities[abilitySpot]);
        FindSpot(AllAbilities[abilitySpot]);
    }

    public static void FindSpot(string ability)
    {
        if (AbilityCheck(ability) == false)
        {
            QuedAbility = ability;
            SetNewAbilityPlace = true;
            TurnCalculator.isStopped = true;
            MovementAndScoring.isMoving = true;
        }
    }

    public static bool AbilityCheck(string ability)
    {
        if (ability1 == "")
        {
            ability1 = ability;
            return true;
        }
        else if (ability2 == "")
        {
            ability2 = ability;
            return true;
        }
        else if (ability3 == "")
        {
            ability3 = ability;
            return true;
        }
        else if (ability4 == "")
        {
            ability4 = ability;
            return true;
        }
        else if (ability5 == "")
        {
            ability5 = ability;
            return true;
        }
        else if (ability6 == "")
        {
            ability6 = ability;
            return true;
        }
        else
            return false;
    }

    private static void SetAbility(string ability)
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ability1 = ability;
            ContinueGame();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            ability2 = ability;
            ContinueGame();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            ability3 = ability;
            ContinueGame();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ability4 = ability;
            ContinueGame();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ability5 = ability;
            ContinueGame();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ability6 = ability;
            ContinueGame();
        }
    }

    //Function for reseting flags so the game can go on
    private static void ContinueGame()
    {
        SetNewAbilityPlace = false;
        TurnCalculator.isStopped = false;
        MovementAndScoring.isMoving = false;
    }
}
