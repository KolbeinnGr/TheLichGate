using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : IAttackBehavior // implements IAttackBehavior to use in the state machine
{
    // Assuming the warrior might have some properties for a melee attack, like a hitbox transform
    private Animator animator;

    public MeleeAttack(Animator animator)
    {
        this.animator = animator;
    }

    public void Attack() // this is unfinished, here would be a call to placing an attack hurtbox, dealing damage, etc.
    {
        // Implement the melee attack logic here
        animator.SetTrigger("Attack");
        // Possibly activate hitboxes, deal damage, etc.
    }
}


