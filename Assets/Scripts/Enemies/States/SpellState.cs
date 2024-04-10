using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellState : State
{
    private IAttackBehavior attackBehavior;

    public SpellState(IEnemy enemy, IAttackBehavior attackBehavior) : base(enemy)
    {
        this.attackBehavior = attackBehavior;
    }

    public override void Enter()
    {
        enemy.Animator.SetBool("IsCastingSpell", true);
    }

    public override void Exit()
    {
        enemy.Animator.SetBool("IsCastingSpell", false);
    }

    public void PerformAttack()
    {
        attackBehavior.Attack();
    }

    public void PerformHit(string attackname)
    {
        attackBehavior.PerformHit(attackname);
    }
}


