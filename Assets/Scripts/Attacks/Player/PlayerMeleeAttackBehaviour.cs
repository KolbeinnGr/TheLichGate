using UnityEngine;

public class PlayerMeleeAttackBehavior : MeleeAttackBehavior
{
    private Collider2D hitbox;

    protected override void Start()
    {
        base.Start();
        hitbox = GetComponent<Collider2D>();
        hitbox.enabled = true;

    }

    // Override the OnTriggerEnter2D to specify what happens when the hitbox collides with an enemy
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Health enemyHealth = other.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(playerStats.bodyAttackDamage);
            }
        }
    }

    // You can call this method from the PlayerMeleeAttackController when instantiating the slash effect
}