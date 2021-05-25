using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomScript : MonoBehaviour
{
    //public information about the monster
    public Structure.MonsterStats Stats;
    public bool isTurn;
    public GameObject MushroomSpores;
    public GameObject BuffSpores;
    public GameObject MushScream;

    //private information about the monster
    private Animator anim;

    //Semi random range of stats of the monster
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        isTurn = false;
        Stats.HP = Random.Range(20, 31);
        Stats.PhysDef = Random.Range(-11, 1);
        Stats.MagDef = Random.Range(2, 11);
        Stats.Attack = Random.Range(1, 5);
        Stats.Iniative = Random.Range(1, 3);
    }


    void Update()
    {
        Structure.didMonsterFind temp = FindPlayer();
        if (isTurn == true)
        {
            if (temp.hasFound == true)
            {
                if (temp.direction.distance < 1.5)
                {
                    PlayerInfo.TakeDamage(50, "Magical");
                    GameObject MushroomParticles = Instantiate(MushroomSpores) as GameObject;
                    MushroomParticles.transform.position = transform.position;
                    Destroy(MushroomParticles, 1);
                    AttackShout("Bomb");
                }
            }
            else
            {
                PowerUP();
            }
            isTurn = false;
            TurnCalculator.isStopped = false;
        }
    }
    //Dumb way of doing a screen wide aoe that would need fixing as monsters get added but didn't find a way to increase the monsters stats any other way
    void PowerUP()
    {
        AttackShout("Buff");
        GameObject MushroomParticles = Instantiate(BuffSpores) as GameObject;
        MushroomParticles.transform.position = transform.position;
        Destroy(MushroomParticles, 1);
        GameObject[] enemies;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Debug.Log(enemies.Length);
        foreach (GameObject enemy in enemies)
        {
            if (enemy.name[0] == 'G')
            {
                if (enemy.GetComponent<GoblinScript>().Stats.Iniative <= 500)
                {
                    enemy.GetComponent<GoblinScript>().Stats.Iniative += 2;
                }
                enemy.GetComponent<GoblinScript>().Stats.HP += 10;

            }
            if (enemy.name[0] == 'E')
            {
                if (enemy.GetComponent<FlyingEye>().Stats.Iniative <= 500)
                {
                    enemy.GetComponent<FlyingEye>().Stats.Iniative += 5;
                }
                if (enemy.GetComponent<FlyingEye>().Stats.Attack <= 200)
                {
                    enemy.GetComponent<FlyingEye>().Stats.Attack += 10;
                }
            }
            else if (enemy.name[0] == 'M')
            {
                if (enemy.GetComponent<MushroomScript>().Stats.Iniative <= 500)
                {
                    enemy.GetComponent<MushroomScript>().Stats.Iniative += 2;
                }
                enemy.GetComponent<MushroomScript>().Stats.HP += 10;
            }
        }
    }

    //Monster specific shouts when they attack
    void AttackShout(string Type = "")
    {
        GameObject MushroomScreams = Instantiate(MushScream) as GameObject;
        float phrase = Random.Range(1, 6);
        if (Type == "Buff")
        {
            if (phrase == 1)
            {
                MushroomScreams.GetComponent<TextMesh>().text = "Grow my pretties";
            }
            else if (phrase == 2)
            {
                MushroomScreams.GetComponent<TextMesh>().text = "GROW!";
            }
            else if (phrase == 3)
            {
                MushroomScreams.GetComponent<TextMesh>().text = "Grow my pretties";
            }
            else if (phrase == 4)
            {
                MushroomScreams.GetComponent<TextMesh>().text = "GO FASTER";
            }
            else if (phrase == 5)
            {
                MushroomScreams.GetComponent<TextMesh>().text = "QUICKER";
            }
            MushroomScreams.transform.position = transform.position + Vector3.left / 2;
            Destroy(MushroomScreams, 1);
        }
        else if (Type == "Bomb")
        {
            if (phrase == 1)
            {
                MushroomScreams.GetComponent<TextMesh>().text = "Pfff";
            }
            else if (phrase == 2)
            {
                MushroomScreams.GetComponent<TextMesh>().text = "HEHE";
            }
            else if (phrase == 3)
            {
                MushroomScreams.GetComponent<TextMesh>().text = "Infested";
            }
            else if (phrase == 4)
            {
                MushroomScreams.GetComponent<TextMesh>().text = "SPORE TIME";
            }
            else if (phrase == 5)
            {
                MushroomScreams.GetComponent<TextMesh>().text = "EHEHE";
            }
            MushroomScreams.transform.position = transform.position + Vector3.left / 2;
            Destroy(MushroomScreams, 1);
        }
    }

    //General monster Scripts (Should make a class that would have all of these that they would inherit)
    //Checks if the player is either vertically or horizontally in the same line 
    Structure.didMonsterFind FindPlayer()
    {
        RaycastHit2D hit;
        Structure.didMonsterFind Found;
        hit = Physics2D.Raycast(transform.position, Vector2.right);
        Found.direction = hit;
        Found.hasFound = false;
        if (hit.collider.name == "Character")
        {
            Found.direction = hit;
            Found.hasFound = true;
            return Found;
        }
        hit = Physics2D.Raycast(transform.position, Vector2.left);
        if (hit.collider.name == "Character")
        {
            Found.direction = hit;
            Found.hasFound = true;
            return Found;
        }
        hit = Physics2D.Raycast(transform.position, Vector2.down);
        if (hit.collider.name == "Character")
        {
            Found.direction = hit;
            Found.hasFound = true;
            return Found;
        }
        hit = Physics2D.Raycast(transform.position, Vector2.up);
        if (hit.collider.name == "Character")
        {
            Found.direction = hit;
            Found.hasFound = true;
            return Found;
        }
        return Found;
    }
    
    //Methods to see if they should take damage if they get hit
    void OnTriggerEnter2D(Collider2D Other)
    {
        string[] nameOfAbility = Other.gameObject.name.Split('(');
        TakesDamage(nameOfAbility[0]);
    }
    
    //Since some abilities spawn inside the target have to use both trigger enter and collision enter
    void OnCollisionEnter2D(Collision2D Other)
    {
        string[] nameOfAbility = Other.gameObject.name.Split('(');
        TakesDamage(nameOfAbility[0]);
    }

    //Made to trigger animations when hit with skills that do not need to colide with the monster
    public void ForceFullDamageIntake(Structure.Damage abilityInfo)
    {
        anim.SetBool("isHit", true);
        TakeDamage.MonsterTakeDamage(ref Stats, abilityInfo);
        StartCoroutine(TakeDamageRest());

        if (Stats.HP <= 0)
        {
            PlayerInfo.Points += 100;
            Destroy(gameObject);
            TurnCalculator.GetMonsters();
        }
    }

    //Script to take damage and if the enemy dies adds points and regets the monster list
    void TakesDamage(string nameOfAbility)
    {
        Structure.Damage abilityInfo;
        abilityInfo = TakeDamage.GetInfo(nameOfAbility);
        if (abilityInfo.damage != null)
        {
            anim.SetBool("isHit", true);
            //Debug.Log(abilityInfo.type + " " + abilityInfo.damage);
            TakeDamage.MonsterTakeDamage(ref Stats, abilityInfo);
            StartCoroutine(TakeDamageRest());

            if (Stats.HP <= 0)
            {
                PlayerInfo.Points += 100;
                Destroy(gameObject);
                TurnCalculator.GetMonsters();
            }
        }
    }

    //Animation help
    IEnumerator TakeDamageRest()
    {
        yield return new WaitForSeconds(0.25f);
        anim.SetBool("isHit", false);
    }

}
