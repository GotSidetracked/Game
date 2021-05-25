using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEye : MonoBehaviour
{
    /// <summary>
    /// Flying eye script, made for the boss on level 7
    /// </summary>

    //public information about the monster
    public Structure.MonsterStats Stats;
    public bool isTurn;
    public GameObject EyeScream;


    //private information about the monster
    private bool isMoving;
    private Vector3 origPos, targetPos;
    private float timeToMove = 0.5f;
    private float elapsedTime = 0;
    private Animator anim;

    //Sets the stats and bools for the monster stats
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        isMoving = false;
        isTurn = false;
        Stats.HP = 100;
        Stats.PhysDef = 15;
        Stats.MagDef = 5;
        Stats.Attack = 20;
        Stats.Iniative = 0;
    }


    void Update()
    {
        Structure.didMonsterFind temp = FindPlayer();
        if (isTurn == true)
        {
            if (temp.hasFound == true)
            {
                if (Stats.HP < 30)
                {
                    //Target tries to run away
                    Wander();
                    AttackShout("Run");
                }
                else if (temp.direction.distance >= 1.5)
                {
                    //Magical beam
                    PlayerInfo.TakeDamage(Stats.Attack * 3, "Magical");
                    AttackShout("Beam");
                }
                else if (temp.direction.distance < 1.5)
                {
                    //Melee attack
                    anim.SetBool("MelleeAttack", true);
                    PlayerInfo.TakeDamage(Stats.Attack * 5, "Physical");
                    AttackShout("Slash");
                    StartCoroutine(MeleeReset());
                }
            }
            else
            {
                //If the player is not seen they will just do damage to them and move around 
                PlayerInfo.TakeDamage(Stats.Attack, "Magical");
                Wander();
            }
            isTurn = false;
            TurnCalculator.isStopped = false;
        }
    }
    //Monster specific shouts when they attack
    void AttackShout(string Type = "")
    {
        GameObject EyeScreams = Instantiate(EyeScream) as GameObject;
        float phrase = Random.Range(1, 6);
        if (Type == "")
        {
            if (phrase == 1)
            {
                EyeScreams.GetComponent<TextMesh>().text = "The Goblin economy is in my hands!";
            }
            else if (phrase == 2)
            {
                EyeScreams.GetComponent<TextMesh>().text = "Anyone has some eyedrops?";
            }
            else if (phrase == 3)
            {
                EyeScreams.GetComponent<TextMesh>().text = "Does nobody really have eyedrops?";
            }
            else if (phrase == 4)
            {
                EyeScreams.GetComponent<TextMesh>().text = "Awwwwww .... Here we go again";
            }
            else if (phrase == 5)
            {
                EyeScreams.GetComponent<TextMesh>().text = "<Insert Text>?";
            }
        }
        else if (Type == "Slash")
        {
            if (phrase == 1)
            {
                EyeScreams.GetComponent<TextMesh>().text = "NOM";
            }
            else if (phrase == 2)
            {
                EyeScreams.GetComponent<TextMesh>().text = "DELICIOUS";
            }
            else if (phrase == 3)
            {
                EyeScreams.GetComponent<TextMesh>().text = "CHOMP";
            }
            else if (phrase == 4)
            {
                EyeScreams.GetComponent<TextMesh>().text = "MORE";
            }
            else if (phrase == 5)
            {
                EyeScreams.GetComponent<TextMesh>().text = "CRUNCH";
            }
            EyeScreams.transform.position = transform.position + Vector3.left / 2;
            Destroy(EyeScreams, 1);
        }
        else if (Type == "Beam")
        {
            if (phrase == 1)
            {
                EyeScreams.GetComponent<TextMesh>().text = "PEW";
            }
            else if (phrase == 2)
            {
                EyeScreams.GetComponent<TextMesh>().text = "PEW PEW";
            }
            else if (phrase == 3)
            {
                EyeScreams.GetComponent<TextMesh>().text = "LASER TIME";
            }
            else if (phrase == 4)
            {
                EyeScreams.GetComponent<TextMesh>().text = "BEAAAAAM";
            }
            else if (phrase == 5)
            {
                EyeScreams.GetComponent<TextMesh>().text = "AHAHA";
            }
            EyeScreams.transform.position = transform.position + Vector3.left / 2;
            Destroy(EyeScreams, 1);
        }
        else if (Type == "Run")
        {
            if (phrase == 1)
            {
                EyeScreams.GetComponent<TextMesh>().text = "NO";
            }
            else if (phrase == 2)
            {
                EyeScreams.GetComponent<TextMesh>().text = "STOP";
            }
            else if (phrase == 3)
            {
                EyeScreams.GetComponent<TextMesh>().text = "I WILL NOT";
            }
            else if (phrase == 4)
            {
                EyeScreams.GetComponent<TextMesh>().text = "NO";
            }
            else if (phrase == 5)
            {
                EyeScreams.GetComponent<TextMesh>().text = "STOP";
            }
            EyeScreams.transform.position = transform.position + Vector3.left / 2;
            Destroy(EyeScreams, 1);
        }
    }

    //General monster Scripts (Should make a class that would have all of these that they would inherit)

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
                PlayerInfo.Points += 500;
                Destroy(gameObject);
                TurnCalculator.GetMonsters();
            }
        }
    }


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

    //Movement scripts
    void Wander(int Change = 0)
    {
        //The change value is there because had problems where the monster would try and move to one direction 
        //too much and this would get a higher chance of choosing a different direction instead of trying to get to the same direction
        anim.SetBool("isRunning", true);
        int direction = Random.Range(1, 4) + Random.Range(0, Change);
        while (direction > 4)
        {
            direction -= 4;
        }
        //up 
        if (isMoving == false)
        {
            if (direction == 1)
            {
                Debug.Log("Moving Up");
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up);
                float distance = Mathf.Abs(hit.point.y - transform.position.y);
                if (hit.distance > 1)
                {
                    StartCoroutine(MoveCreature(Vector3.up));
                }
                else
                    Wander(4);

            }
            //right
            if (direction == 2)
            {
                Debug.Log("Moving Right");

                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right);
                float distance = Mathf.Abs(hit.point.y - transform.position.y);
                if (hit.distance > 1)
                {
                    StartCoroutine(MoveCreature(Vector3.right));
                }
                else
                    Wander(4);

            }
            //down
            if (direction == 3)
            {
                Debug.Log("Moving Down");

                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down);
                float distance = Mathf.Abs(hit.point.y - transform.position.y);
                if (hit.distance > 1)
                {
                    StartCoroutine(MoveCreature(Vector3.down));
                }
                else
                    Wander(4);

            }
            //left
            if (direction == 4)
            {
                Debug.Log("Moving Left");

                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left);
                float distance = Mathf.Abs(hit.point.y - transform.position.y);
                //temp
                if (hit.distance > 1)
                {
                    StartCoroutine(MoveCreature(Vector3.left));
                }
                else
                    Wander(4);
            }
        }
    }

    private IEnumerator MoveCreature(Vector3 direction)
    {
        isMoving = true;
        origPos = transform.position;
        targetPos = origPos + direction;
        Debug.Log(origPos + " + " + direction + "  =  " + targetPos);
        while (elapsedTime < timeToMove)
        {
            transform.position = Vector3.Lerp(origPos, targetPos, (elapsedTime / timeToMove));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        anim.SetBool("isRunning", false);
        elapsedTime = 0;
        timeToMove = 0.5f;
        transform.position = targetPos;
        isMoving = false;
    }

        //Helping animation methods
    IEnumerator MeleeReset()
    {
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("MelleeAttack", false);
    }

    IEnumerator TakeDamageRest()
    {
        yield return new WaitForSeconds(0.25f);
        anim.SetBool("isHit", false);
    }
}
