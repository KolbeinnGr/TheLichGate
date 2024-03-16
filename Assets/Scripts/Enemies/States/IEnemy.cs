using UnityEngine;

public interface IEnemy
{
    Transform TargetDestination { get; }
    Animator Animator { get; }
    Rigidbody2D Rb { get; }
    float Speed { get; }

    void Attack();
    void StopAttack();
    void Walk();
    void StopWalk();
    void Idle();
}
