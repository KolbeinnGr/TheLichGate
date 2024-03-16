using UnityEngine;

public class WalkState : State
{
    public WalkState(IEnemy enemy) : base(enemy)
    {
    }

    public override void Enter()
    {
        enemy.Animator.SetBool("IsWalking", true);
    }

    public override void FixedDo()
    {
        Vector3 direction = (enemy.TargetDestination.position - enemy.Rb.transform.position).normalized;
        enemy.Rb.velocity = direction * enemy.Speed;
    }

    public override void Exit()
    {
        enemy.Animator.SetBool("IsWalking", false);
        enemy.Rb.velocity = Vector2.zero;
    }
}
