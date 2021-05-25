using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinScript : MonoBehaviour
{
    /// <summary>
    /// Goblin script, made to be the standart enemy throught the floors
    /// </summary>


    //public information about the monster
    public Structure.MonsterStats Stats;
    public bool isTurn;
    public GameObject GoblinBoom;
    public GameObject GoblinScream;

    //private information about the monster
    private bool isMoving;
    private Vector3 origPos, targetPos;
    private float timeToMove = 0.5f;
    private float elapsedTime = 0;
    private Animator anim;

    //Semi random range of stats of the monster
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        isMoving = false;
        isTurn = false;
        Stats.HP = Random.Range(40,61);
        Stats.PhysDef = Random.Range(-2,3);
        Stats.MagDef = Random.Range(-2, 3);
        Stats.Attack = Random.Range(1, 5);
        Stats.Iniative = Random.Range(1, 5);
    }

    void Update()
    {
        Structure.didMonsterFind temp = FindPlayer();
        if (isTurn == true)
        {
            if (temp.hasFound == true)
            {
                if (temp.direction.distance >= 1.5)
                {
                    PlayerInfo.TakeDamage(10, "Physical");
                    AttackShout("Arrow");
                }
                else if (temp.direction.distance < 1.5 && Stats.HP <= 15)
                {
                    Destroy(gameObject);
                    PlayerInfo.TakeDamage(50, "Magical");
                    GameObject GoblinParticles = Instantiate(GoblinBoom) as GameObject;
                    GoblinParticles.transform.position = transform.position;
                    Destroy(GoblinParticles, 1);
                    AttackShout("Bomb");
                }
                else if (temp.direction.distance < 1.5)
                {
                    anim.SetBool("MelleeAttack", true);
                    PlayerInfo.TakeDamage(20, "Physical");
                    AttackShout("Slash");
                    StartCoroutine(MeleeReset());

                }
            }
            else
            {
                Wander();
            }
            isTurn = false;
            TurnCalculator.isStopped = false;
        }
    }

    //Monster specific shouts when they attack
    void AttackShout(string Type = "")
    {
        GameObject GoblinScreams = Instantiate(GoblinScream) as GameObject;
        float phrase = Random.Range(1, 6);
        if (Type == "")
        {
            if (phrase == 1)
            {
                GoblinScreams.GetComponent<TextMesh>().text = "The goblin socio economic crysis is at hand";
            }
            else if (phrase == 2)
            {
                GoblinScreams.GetComponent<TextMesh>().text = "This goblin is from a wealthy family so do not kill me!";
            }
            else if (phrase == 3)
            {
                GoblinScreams.GetComponent<TextMesh>().text = "Goblin smart, use slashy thing to slash enemy";
            }
            else if (phrase == 4)
            {
                GoblinScreams.GetComponent<TextMesh>().text = "There are many goblins like me, but I got a lot of bugs";
            }
            else if (phrase == 5)
            {
                GoblinScreams.GetComponent<TextMesh>().text = "<Insert Text>?";
            }
        }
        else if (Type == "Slash")
        {
            if (phrase == 1)
            {
                GoblinScreams.GetComponent<TextMesh>().text = "AHAHAHA";
            }
            else if (phrase == 2)
            {
                GoblinScreams.GetComponent<TextMesh>().text = "KNEECAPPED";
            }
            else if (phrase == 3)
            {
                GoblinScreams.GetComponent<TextMesh>().text = "YESSSS";
            }
            else if (phrase == 4)
            {
                GoblinScreams.GetComponent<TextMesh>().text = "MORE";
            }
            else if (phrase == 5)
            {
                GoblinScreams.GetComponent<TextMesh>().text = "GOLD";
            }
            GoblinScreams.transform.position = transform.position + Vector3.left / 2;
            Destroy(GoblinScreams, 1);
        }
        else if (Type == "Arrow")
        {
            if (phrase == 1)
            {
                GoblinScreams.GetComponent<TextMesh>().text = "PEW";
            }
            else if (phrase == 2)
            {
                GoblinScreams.GetComponent<TextMesh>().text = "NO ESCAPE";
            }
            else if (phrase == 3)
            {
                GoblinScreams.GetComponent<TextMesh>().text = "MURDER";
            }
            else if (phrase == 4)
            {
                GoblinScreams.GetComponent<TextMesh>().text = "POINTY END FOR YOU";
            }
            else if (phrase == 5)
            {
                GoblinScreams.GetComponent<TextMesh>().text = "HEHEEHE";
            }
            GoblinScreams.transform.position = transform.position + Vector3.left / 2;
            Destroy(GoblinScreams, 1);
        }
        else if (Type == "Bomb")
        {
            if (phrase == 1)
            {
                GoblinScreams.GetComponent<TextMesh>().text = "FOR YOU";
            }
            else if (phrase == 2)
            {
                GoblinScreams.GetComponent<TextMesh>().text = "KABOOM";
            }
            else if (phrase == 3)
            {
                GoblinScreams.GetComponent<TextMesh>().text = "TSSSS";
            }
            else if (phrase == 4)
            {
                GoblinScreams.GetComponent<TextMesh>().text = "PRESENTS";
            }
            else if (phrase == 5)
            {
                GoblinScreams.GetComponent<TextMesh>().text = "AHAHAA";
            }
            GoblinScream.transform.position = transform.position + Vector3.left / 2;
            Destroy(GoblinScream, 1);
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
                PlayerInfo.Points += 100;
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
