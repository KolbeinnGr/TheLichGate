using UnityEngine;

public class MeleeAttackBehavior : MonoBehaviour
{
    public float damage; // The damage dealt by the melee attack

    private Animator animator; // To handle attack animations
    private Collider2D attackHitbox; // The Collider used as the hitbox
    


    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        attackHitbox = GetComponent<Collider2D>();
        attackHitbox.enabled = false; // Initially disable the hitbox
    }

    public virtual void PerformAttack()
    {
        // Optional: Trigger an attack animation
        animator.SetTrigger("Attack");
        

        // Enable the hitbox collider at the start of the attack
        attackHitbox.enabled = true;

        // Optionally, disable the hitbox after a delay, 
        // or you can disable it at the end of the attack animation using an Animation Event
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Apply damage to the enemy
            Health healthComponent = other.GetComponent<Health>();
            if (healthComponent != null)
            {
                healthComponent.TakeDamage(damage);
            }

            // Optionally, add logic to handle hit effects, sounds, etc.
        }
    }

    // Call this method to disable the hitbox, e.g., from an Animation Event
    public void DisableHitbox()
    {
        attackHitbox.enabled = false;
    }
}