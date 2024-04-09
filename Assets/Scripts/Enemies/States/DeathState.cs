using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : State
{
    public DeathState(IEnemy enemy) : base(enemy)
    {
    }

    public override void Enter()
    {
        enemy.Rb.velocity = Vector2.zero;
        enemy.Animator.SetBool("IsWalking", false);
        enemy.Animator.SetBool("IsDead", true);
    }
}


