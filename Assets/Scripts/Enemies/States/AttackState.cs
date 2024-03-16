using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    private IAttackBehavior attackBehavior;

    public AttackState(IEnemy enemy, IAttackBehavior attackBehavior) : base(enemy)
    {
        this.attackBehavior = attackBehavior;
    }

    public override void Enter()
    {
        enemy.Animator.SetBool("IsAttacking", true);
    }

    public override void Exit()
    {
        enemy.Animator.SetBool("IsAttacking", false);
    }

    public void PerformAttack()
    {
        attackBehavior.Attack();
    }
}


