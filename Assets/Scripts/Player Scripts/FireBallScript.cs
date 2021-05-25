using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallScript : MonoBehaviour
{
    /// <summary>
    /// Script made for the fireball ability, the firing is in the attack script of the character
    /// </summary>

    private Rigidbody2D rb;
    public GameObject Explosion;

    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D Other)
    {
        if (Other.tag == "Enemy" || Other.tag == "Walls")
        {
            GameObject ExplosionParticles = Instantiate(Explosion) as GameObject;
            ExplosionParticles.transform.position = transform.position;
            Destroy(ExplosionParticles, 1);
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D Other)
    {
        if (Other.gameObject.tag == "Enemy" || Other.gameObject.tag == "Walls")
        {
            GameObject ExplosionParticles = Instantiate(Explosion) as GameObject;
            ExplosionParticles.transform.position = transform.position;
            Destroy(ExplosionParticles, 1);
            Destroy(gameObject);
        }
    }
    public void FireLeft()
    {
        rb.velocity = new Vector2(-5.0f, 0.0f);
    }
    public void FireRight()
    {
        rb.velocity = new Vector2(5.0f, 0.0f);
    }
    public void FireDown()
    {
        rb.velocity = new Vector2(1.0f, -5.0f);
    }

    public void FireUp()
    {
        rb.velocity = new Vector2(0.0f, 5.0f);
    }
}
