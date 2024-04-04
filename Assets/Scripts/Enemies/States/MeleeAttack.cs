using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : IAttackBehavior
{
    private Animator animator;
    private GameObject warningZone;
    private Transform enemyTransform;
    private Transform targetTransform;

    public MeleeAttack(Animator animator, GameObject warningZone, Transform enemyTransform, Transform targetTransform)
    {
        this.animator = animator;
        this.warningZone = warningZone;
        this.enemyTransform = enemyTransform;
        this.targetTransform = targetTransform;
    }

    public void Attack()
    {
        ShowWarningZone();
        // Trigger the animation that will call PerformAttack() via an animation event
        //animator.SetTrigger("Attack");
    }

    private void ShowWarningZone()
    {
        
        warningZone.SetActive(true);
        Vector3 direction = targetTransform.position - enemyTransform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90; // Adjust based on your sprite orientation
        warningZone.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        // Hide the warning zone after a delay or at the end of the attack animation
    }

    // Call this method from the animation event to perform the "hit" part of the attack
    public void PerformHit()
    {
        // Logic to deal damage, activate hitboxes, etc.
        // Make sure to hide the warning zone after the attack concludes
        warningZone.SetActive(false);
    }
}



