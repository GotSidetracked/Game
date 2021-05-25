using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAndScoring : MonoBehaviour
{
    //Private variables
    private bool isBoosted = false;
    private bool isSlowed = false;
    private int LastHorizontalMove;
    private float elapsedTime = 0;
    private Vector3 origPos, targetPos;
    private Rigidbody2D rb2D;
    private Animator anim;
    private Collider coll;
   
    //Public variables
    public static bool isMoving;
    public double singleGridSpaceSize = 1;
    public int collectedPoints;
    public float timeToMove = 0.5f;
    public GameObject Mine;
    public GameObject DestroyedObjectParticles;

    void Start()
    {
        collectedPoints = 0;
        LastHorizontalMove = -1;
        rb2D = GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        coll = GetComponent<Collider>();
    }

    //This method works by taking an input from the player checking if there is a space to move 
    //with raycast and slowly transforming the players position to the one they wanted
    //If the player preses space they will jump back two squares in the oposing direction of their last move
    void Update()
    {
        if (TurnCalculator.isPlayersTurn == true)
        {
            if (isBoosted == true)
            {
                anim.SetBool("isDead", true);
                isMoving = true;
            }
            if (isSlowed == true)
            {
                //gravity way
                anim.enabled = false;
                isMoving = true;
            }

            //Movement up
            if (Input.GetAxis("Vertical") > 0 && !isMoving)
            {
                if (isBoosted == true)
                {
                    timeToMove = timeToMove / 2;
                    isBoosted = false;
                }
                if (isSlowed == true)
                {
                    timeToMove = timeToMove * 2;
                    isSlowed = false;
                }
                int playerLayer = 11;
                int layerMask = ~(1 << playerLayer);
                //Debug.Log(layerMask);
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up);
                float distance = Mathf.Abs(hit.point.y - transform.position.y);
                //Debug.Log("x - " + hit.point.x + " y - " + hit.point.y + " distance - " + hit.distance);
                //Debug.Log(hit.collider.name);

                if (hit.distance > singleGridSpaceSize)
                {
                    anim.SetBool("isRunning", true);
                    StartCoroutine(MovePlayer(Vector2.up));
                }
            }
            //Movement down
            if (Input.GetAxis("Vertical") < 0 && !isMoving)
            {
                if (isBoosted == true)
                {
                    timeToMove = timeToMove / 2;
                    isBoosted = false;
                }
                if (isSlowed == true)
                {
                    timeToMove = timeToMove * 2;
                    isSlowed = false;
                }
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down);
                float distance = Mathf.Abs(hit.point.y - transform.position.y);

                if (hit.distance > singleGridSpaceSize)
                {
                    anim.SetBool("isRunning", true);
                    StartCoroutine(MovePlayer(Vector3.down));
                }
            }

            //Movement right
            if (Input.GetAxis("Horizontal") > 0 && !isMoving)
            {
                if (isBoosted == true)
                {
                    timeToMove = timeToMove / 2;
                    isBoosted = false;
                }
                if (isSlowed == true)
                {
                    timeToMove = timeToMove * 2;
                    isSlowed = false;
                }
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right);
                float distance = Mathf.Abs(hit.point.y - transform.position.y);


                if (hit.distance > singleGridSpaceSize)
                {
                    anim.SetBool("isRunning", true);
                    LastHorizontalMove = 1;
                    StartCoroutine(MovePlayer(Vector3.right));
                }

            }

            //Movement left
            if (Input.GetAxis("Horizontal") < 0 && !isMoving)
            {
                if (isBoosted == true)
                {
                    timeToMove = timeToMove / 2;
                    isBoosted = false;
                }
                if (isSlowed == true)
                {
                    timeToMove = timeToMove * 2;
                    isSlowed = false;
                }
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left);
                float distance = Mathf.Abs(hit.point.y - transform.position.y);


                if (hit.distance > singleGridSpaceSize)
                {
                    anim.SetBool("isRunning", true);
                    LastHorizontalMove = -1;
                    StartCoroutine(MovePlayer(Vector3.left));
                }
            }
            //Roll back
            if (Input.GetKeyDown(KeyCode.Space) && !isMoving)
            {
                if (isBoosted == true)
                {
                    timeToMove = timeToMove / 2;
                    isBoosted = false;
                }
                if (isSlowed == true)
                {

                    timeToMove = timeToMove * 2;
                    isSlowed = false;
                }
                Debug.Log("Dashing");
                anim.SetBool("isRolling", true);
                Vector3 rollMove;
                RaycastHit2D hit;
                if (LastHorizontalMove == -1)
                {
                    hit = Physics2D.Raycast(transform.position, Vector2.right);
                    rollMove = Vector3.right * 2;
                }
                else
                {
                    hit = Physics2D.Raycast(transform.position, Vector2.left);
                    rollMove = Vector3.left * 2;
                }
                float distance = Mathf.Abs(hit.point.y - transform.position.y);
                Debug.Log(hit.collider.name);
                if (hit.distance > singleGridSpaceSize * 2)
                {
                    GameObject e = Instantiate(Mine) as GameObject;
                    e.transform.position = transform.position;
                    StartCoroutine(MovePlayer(rollMove));
                    Destroy(e, 2);
                }
                else if (hit.distance > singleGridSpaceSize)
                {
                    GameObject e = Instantiate(Mine) as GameObject;
                    e.transform.position = transform.position - Vector3.down;
                    StartCoroutine(MovePlayer(rollMove));
                    Destroy(e, 2);
                }
            }
        }
    }

    private IEnumerator MovePlayer(Vector3 direction)
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
        elapsedTime = 0;
        timeToMove = 0.5f;
        transform.position = targetPos;
        isMoving = false;
        anim.SetBool("isRunning", false);
        anim.SetBool("isRolling", false);
        TurnCalculator.isPlayersTurn = false;
    }

    //Checks if the player has colided with a point (Should move this to its own class)
    private void OnTriggerEnter2D(Collider2D Other)
    {
        if (Other.tag == "Point")
        {
            GameObject Skull = Instantiate(DestroyedObjectParticles) as GameObject;
            Skull.transform.position = Other.transform.position;
            PlayerInfo.Points += 1;
            Destroy(Other.gameObject);
        }
    }
}
