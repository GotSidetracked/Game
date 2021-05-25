using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    /// <summary>
    /// In this script the behavior of abilities is writen
    /// Information on skills that do damage on impact can be found in TakeDamage script
    /// </summary>

    private bool isWaitingForInput;
    private string QuedAbility;

    void Start()
    {
        isWaitingForInput = false;
    }

    void Update()
    {
        //Checks if an ability is qued
        if (isWaitingForInput == true)
        {
            //Gets a direction and tries to cast the ability
            Vector3 direction = WaitForInput();
            if (QuedAbility == "Fireball")
            {
                UseFireBall(direction);
            }
            else if (direction != Vector3.zero)
            {
                if (QuedAbility == "Slash")
                {
                    UseSlash(direction);
                }
                if (QuedAbility == "Mine")
                {
                    UseMine(direction);
                }
                FinishTurn();
            }
        }
        if (MovementAndScoring.isMoving == false && TurnCalculator.isPlayersTurn == true)
        {
            abilityKeyPress();
        }
    }

    //General scripts
    Vector3 WaitForInput()
    {
        //Attack right
        if (Input.GetAxis("Horizontal") > 0)
        {
            return Vector3.right;
        }
        //Attack left
        else if (Input.GetAxis("Horizontal") < 0)
        {
            return Vector3.left;
        }
        //Attack Up
        else if (Input.GetAxis("Vertical") > 0)
        {
            return Vector3.up;
        }
        //Attack down
        else if (Input.GetAxis("Vertical") < 0)
        {
            return Vector3.down;
        }
        return Vector3.zero;
    }

    void PrepareAbility(ref Vector3 direction, GameObject ability)
    {
        SpriteRenderer sprite = ability.GetComponent(typeof(SpriteRenderer)) as SpriteRenderer;
        if (direction == Vector3.left)
        {
            sprite.flipX = true;
        }
        else if (direction == Vector3.up)
        {
            ability.transform.Rotate(new Vector3(0, 0, 90f));
            direction = direction + Vector3.left / 4;
        }
        else if (direction == Vector3.down)
        {
            ability.transform.Rotate(new Vector3(0, 0, -90f));
            direction = direction + Vector3.left / 4;
        }
    }

    void abilityKeyPress()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            useOrQueAbility(PlayerInfo.ability1);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            useOrQueAbility(PlayerInfo.ability2);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            useOrQueAbility(PlayerInfo.ability3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            useOrQueAbility(PlayerInfo.ability4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            useOrQueAbility(PlayerInfo.ability5);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            useOrQueAbility(PlayerInfo.ability6);
        }
    }

    void FinishTurn()
    {
        isWaitingForInput = false;
        TurnCalculator.isStopped = false;
        TurnCalculator.isPlayersTurn = false;
        MovementAndScoring.isMoving = false;
    }

    void useOrQueAbility(string ability)
    {
        if (ability == "Slash") //Slice
        {
            QuedAbility = "Slash";
            MovementAndScoring.isMoving = true;
            TurnCalculator.isStopped = true;
            isWaitingForInput = true;
        }
        if (ability == "Fireball") //Fireball
        {
            QuedAbility = "Fireball";
            MovementAndScoring.isMoving = true;
            TurnCalculator.isStopped = true;
            isWaitingForInput = true;
        }
        if (ability == "Mine") //Mine
        {
            QuedAbility = "Mine";
            MovementAndScoring.isMoving = true;
            TurnCalculator.isStopped = true;
            isWaitingForInput = true;
        }
        if (ability == "Sacrifice")
        {
            UseSacrifice();
            FinishTurn();
        }
        if (ability == "Berserk")
        {
            UseBerserk();
            FinishTurn();
        }
        if (ability == "Random")
        {
            UseRandom();
            FinishTurn();
        }
    }

    //scruot for ability "Mine"
    private void UseMine(Vector3 direction)
    {
        GameObject e = Instantiate(GameObject.Find("Mine")) as GameObject;
        PrepareAbility(ref direction, e);
        e.transform.position = transform.position + direction;
    }
    //script for ability "Slash"
    private void UseSlash(Vector3 direction)
    {
        GameObject e = Instantiate(GameObject.Find("Slash")) as GameObject;
        PrepareAbility(ref direction, e);
        e.transform.position = transform.position + direction;
        Destroy(e, 1);
    }
    //script for ability "Fireball"
    private void UseFireBall(Vector3 direction)
    {
        if (direction == Vector3.left || direction == Vector3.right)
        {
            GameObject e = Instantiate(GameObject.Find("Fireball")) as GameObject;
            PrepareAbility(ref direction, e);
            e.transform.position = transform.position + direction;
            FireBallScript script = e.GetComponent<FireBallScript>();
            if (direction == Vector3.right)
            {
                script.FireRight();
            }
            else if (direction == Vector3.left)
            {
                script.FireLeft();
            }
            isWaitingForInput = false;
            TurnCalculator.isStopped = false;
            TurnCalculator.isPlayersTurn = false;
            MovementAndScoring.isMoving = false;
        }
    }
    //script for ability "Sacrifice"
    private void UseSacrifice()
    {
        PlayerInfo.TakeDamage(PlayerInfo.Damage * 2, "True");
        GameObject[] enemies;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Structure.Damage Info = new Structure.Damage();
        Info.type = "True";
        Info.damage = PlayerInfo.Damage * 5;
        foreach (GameObject enemy in enemies)
        {
            if (enemy.name[0] == 'G')
            {
                GoblinScript script = enemy.GetComponent<GoblinScript>();
                script.ForceFullDamageIntake(Info);
            }
            else if (enemy.name[0] == 'E')
            {
                FlyingEye script = enemy.GetComponent<FlyingEye>();
                script.ForceFullDamageIntake(Info);
            }
            else
            {
                MushroomScript script = enemy.GetComponent<MushroomScript>();
                script.ForceFullDamageIntake(Info);
            }
        }
    }

    //script for ability "Berserk"
    private void UseBerserk()
    {
        if (PlayerInfo.Iniative != 1)
        {
            PlayerInfo.Iniative -= 1;
            PlayerInfo.Damage += 5;
            PlayerInfo.Heal(10);
        }
    }

    //scripts for ability "Random"
    private void UseRandom()
    {
        for (int i = 0; i < 6; i++)
        {
            int whatAbility = Random.Range(0, 5);
            Vector3 WhatDirection = RandomDirection();
            if (whatAbility == 0) //Slash
            {
                GameObject e = Instantiate(GameObject.Find("Slash")) as GameObject;
                PrepareAbility(ref WhatDirection, e);
                e.transform.position = transform.position + WhatDirection;
                Destroy(e, 1);
            }
            else if (whatAbility == 1)
            {
                WhatDirection = LeftRightRandom();
                GameObject e = Instantiate(GameObject.Find("Fireball")) as GameObject;
                PrepareAbility(ref WhatDirection, e);
                e.transform.position = transform.position + WhatDirection;
                FireBallScript script = e.GetComponent<FireBallScript>();
                if (WhatDirection == Vector3.right)
                {
                    script.FireRight();
                }
                else if (WhatDirection == Vector3.left)
                {
                    script.FireLeft();
                }
            }
            else if (whatAbility == 2)
            {
                GameObject e = Instantiate(GameObject.Find("Mine")) as GameObject;
                PrepareAbility(ref WhatDirection, e);
                e.transform.position = transform.position + WhatDirection;
            }
            else if (whatAbility == 3)
            {
                UseSacrifice();
            }
            else if (whatAbility == 4)
            {
                UseBerserk();
            }
        }
    }

    Vector3 LeftRightRandom() {
        int RandomDirectionPick = Random.Range(0, 2);
        if (RandomDirectionPick == 0)
        {
            return Vector3.right;
        }
        else
        {
            return Vector3.left;
        }
    }

    Vector3 RandomDirection()
    {
        int RandomDirectionPick = Random.Range(0, 4);
        if (RandomDirectionPick == 0)
        {
            return Vector3.right;
        }
        else if (RandomDirectionPick == 1)
        {
            return Vector3.left;
        }
        else if (RandomDirectionPick == 2)
        {
            return Vector3.up;
        }
        else
        {
            return Vector3.down;
        }
    }
}
