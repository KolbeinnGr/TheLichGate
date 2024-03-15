using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySkeletonWarrior : MonoBehaviour
{
    [SerializeField] Transform targetDestination; // the player characters current location is the target destination
    GameObject targetGameObject; // this would be the player character
    [SerializeField] float speed; // how fast the enemy moves
    private Animator animator;
    [SerializeField] float attackRange = 1.5f; // Adjust this value as needed
    Rigidbody2D rb;

    private void Awake() { // get our components ready
        rb = GetComponent<Rigidbody2D>();
        targetGameObject = targetDestination.gameObject;
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate() {
        Vector3 direction = (targetDestination.position - transform.position).normalized; // Get the direction to the target
        float distanceToTarget = Vector3.Distance(transform.position, targetDestination.position); // Get the distance to the target
        rb.velocity = direction * speed; // Move the enemy in the direction of the target

        // Determine if the character is moving and not in attack range
        if (distanceToTarget > attackRange) { // if we are outside the attack range, we walk the enemy to the target
            if (rb.velocity.magnitude > 0) {
                animator.SetBool("IsWalking", true);
            } else {
                animator.SetBool("IsWalking", false);
            }
        }

        // Flip sprite on the X axis when the enemy is on either side of the target
        if (targetDestination.position.x > transform.position.x) {
            // Target is to the right, ensure sprite is facing right
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        } else {
            // Target is to the left, flip sprite to face left
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        // Check if the target is within attack range to trigger the attack
        if (distanceToTarget <= attackRange) {
            Attack();
        } else {
            StopAttack();
        }
    }

    private void Attack() {
        rb.velocity = Vector2.zero; // we stop the movement of the enemy
        animator.SetBool("IsAttacking", true); // then we play the attack animation
        // here would be additional logic for the attack animation such as a hurt box for the attack or we would call a damage player function here.
    }
    private void StopAttack() { // this is only temporary until I implement a different solution.
        animator.SetBool("IsAttacking", false);
    }

    void OnDrawGizmosSelected() {
        // Set the color of the Gizmo
        Gizmos.color = Color.red;

        // this is required because the pivot point is located at the feet of the sprite
        // Calculate the center position of the enemy based on the sprite's height
        // Assuming the sprite is approximately 2 units tall, adjust this value as needed
        float spriteHeight = 1f; // This is an example value; adjust it based on your sprite's actual height
        Vector3 gizmoPosition = transform.position + Vector3.up * (spriteHeight / 2);

        // Draw a wire sphere at the adjusted position to visualize the attack range centered on the enemy
        Gizmos.DrawWireSphere(gizmoPosition, attackRange);
    }


}
