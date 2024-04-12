using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashLanding : MonoBehaviour
{
    public float damageAmount = 30f;
    public float pushForce = 1f;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Health>().TakeDamage(damageAmount);
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 forceDirection = collision.transform.position - transform.position;
                forceDirection.y = 0; // Optional: modify as needed to ensure appropriate force direction
                rb.AddForce(forceDirection.normalized * pushForce, ForceMode2D.Impulse);
            }
        }
    }
    
}
