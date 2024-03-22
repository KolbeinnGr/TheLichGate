using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySkeletonWarrior : MonoBehaviour, IEnemy
{
    // initialize components
    [SerializeField] private Transform targetDestination;
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed = 3f;
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private GameObject warningZone;
    [SerializeField] private Transform enemyTransform;


    // state machine
    private StateMachine stateMachine;

    // IEnemy interface
    public Transform TargetDestination => targetDestination;
    public Animator Animator => animator;
    public Rigidbody2D Rb => rb;
    public float Speed => speed;
    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        targetDestination = player.transform;
        // initialize components
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        // state machine
        stateMachine = GetComponent<StateMachine>();
        InitializeStateMachine();
        
    }

    private void Update()
    {
        // check if the target is within attack range
        float distanceToTarget = Vector3.Distance(transform.position, TargetDestination.position);

        // if the target is within attack range and the enemy is not in attack state, change to attack state
        if (distanceToTarget <= attackRange && !(stateMachine.currentState is AttackState))
        {
            stateMachine.ChangeState(typeof(AttackState));
        } // else if the target is not within attack range and the enemy is not in walk state, change to walk state
        else if (distanceToTarget > attackRange && !(stateMachine.currentState is WalkState))
        {
            stateMachine.ChangeState(typeof(WalkState));
        }


        // Flip sprite on the X axis when the enemy is on either side of the target
        if (targetDestination.position.x > transform.position.x) {
            // Target is to the right, ensure sprite is facing right
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        } else {
            // Target is to the left, flip sprite to face left
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    // initialize state machine
    private void InitializeStateMachine()
    {
        // create states
        var states = new Dictionary<Type, State>
        {   // WalkState is responsible for moving the enemy towards its target.
            { typeof(WalkState), new WalkState(this) },
            { typeof(IdleState), new IdleState(this) },
            { typeof(AttackState), new AttackState(this, new MeleeAttack(animator, warningZone, enemyTransform, targetDestination)) } // MeleeAttack is a reference to the MeleeAttack script
        };
        // set states
        stateMachine.SetStates(states);
    }

    // IEnemy interface
    // These methods are called by the state machine to transition the enemy into different states.
    public void Attack()
    {
        stateMachine.ChangeState(typeof(AttackState));
    }

    public void StopAttack()
    {
        // Logic to stop attacking if needed
    }

    public void Walk()
    {
        stateMachine.ChangeState(typeof(WalkState));
    }

    public void StopWalk()
    {
        // Possibly transition to IdleState
    }

    public void Idle()
    {
        stateMachine.ChangeState(typeof(IdleState));
    }

    public void OnAttackHitboxAnimationEvent()
    {
        if (stateMachine.currentState is AttackState attackState)
        {   
            // if the attack state is active, perform the attack
            attackState.PerformAttack();
        }
    }
    public void OnAttackPerformHitAnimationEvent()
    {
        if (stateMachine.currentState is AttackState attackState)
        {   
            // if the attack state is active, perform the attack
            attackState.PerformHit();
        }
    }
}



