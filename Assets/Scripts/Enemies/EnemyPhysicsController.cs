using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPhysicsController : MonoBehaviour
{
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Optional: Apply an additional force if you want more control over the "push" effect
            // Vector2 forceDirection = (transform.position - collision.transform.position).normalized;
            // rb.AddForce(forceDirection * pushStrength, ForceMode2D.Impulse);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Reset the velocity of the enemy when the player is no longer colliding with it
            rb.velocity = Vector2.zero;
        }
    }
}
