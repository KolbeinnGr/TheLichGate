using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public IdleState(IEnemy enemy) : base(enemy)
    {
    }

    public override void Enter()
    {
        enemy.Rb.velocity = Vector2.zero;
        enemy.Animator.SetBool("IsWalking", false);
        enemy.Animator.SetBool("IsAttacking", false);
        enemy.Animator.SetBool("IsCastingSpell", false);
        enemy.Animator.SetBool("IsIdle", true);
        // Additional idle behaviors like playing idle animation can be added here.
    }

    public override void Exit()
    {
        enemy.Animator.SetBool("IsIdle", false);
    }
}


