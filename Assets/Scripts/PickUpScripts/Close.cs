using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Close : MonoBehaviour
{
    /// <summary>
    /// Script to animate the trap and deal damage to the player when they step on it
    /// </summary>

    private Animator anim;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D Other)
    {
        if (Other.name == "Character")
        {
            StartCoroutine(Chomp());
            PlayerInfo.TakeDamage(25, "Physical");
            Destroy(gameObject, 1);
        }
    }

    IEnumerator Chomp()
    {
        anim.SetBool("isClosed", true);
        yield return new WaitForSeconds(5);
        anim.SetBool("isClosed", false);
    }
}
