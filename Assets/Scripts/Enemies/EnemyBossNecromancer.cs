using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBossNecromancer : MonoBehaviour, IEnemy
{
    // initialize components
    [SerializeField] private Transform targetDestination;
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed = 3f;
    [SerializeField] private Transform enemyTransform;
    [SerializeField] private float minDistanceFromTarget = 1.5f;
    [Header("Range")]
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private float spellRange = 1.5f;
    [Header("Spell")]
    [SerializeField] private GameObject warningZoneSpell;
    [SerializeField] private GameObject SpellGameObject;
    private Animator SpellAnimation;
    [Header("Attack")]
    [SerializeField] private GameObject warningZoneAttack;
    [SerializeField] private GameObject AttackLightningGameObject;
    private Animator attackLightningAnimation;

    [Header("Cooldowns")]
    [SerializeField] private float attackCooldown = 10f;
    [SerializeField] private float spellCooldown = 10f;
    private float lastAttackTime = -Mathf.Infinity;
    private float lastSpellTime = -Mathf.Infinity;



    // state machine
    private StateMachine stateMachine;

    // IEnemy interface
    public Transform TargetDestination => targetDestination;
    public Animator Animator => animator;
    public Rigidbody2D Rb => rb;
    public float Speed => speed;
    private GameObject player;
    private bool IsCurrentlyAttacking = false;

    private void Awake()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        targetDestination = player.transform;
        // initialize components
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        attackLightningAnimation = AttackLightningGameObject.GetComponent<Animator>();
        SpellAnimation = SpellGameObject.GetComponent<Animator>();

        // state machine
        stateMachine = GetComponent<StateMachine>();
        InitializeStateMachine();
        
    }

    private void Update()
    {
        float distanceToTarget = Vector3.Distance(transform.position, TargetDestination.position);
        float timeSinceLastAttack = Time.time - lastAttackTime;
        float timeSinceLastSpell = Time.time - lastSpellTime;

        bool attackOnCooldown = timeSinceLastAttack < attackCooldown;
        bool spellOnCooldown = timeSinceLastSpell < spellCooldown;
        
        if (stateMachine.currentState is IdleState && distanceToTarget >= minDistanceFromTarget)
        {
            stateMachine.ChangeState(typeof(WalkState));
        }

        // Check and perform attack or spell if within range and cooldown has passed
        if (distanceToTarget <= attackRange && !attackOnCooldown && !(stateMachine.currentState is AttackState) && !(stateMachine.currentState is SpellState))
        {
            lastAttackTime = Time.time; // Reset the attack timer
            stateMachine.ChangeState(typeof(AttackState));
            return; // Exit early to prevent further logic from executing this frame
        }
        if (distanceToTarget <= spellRange && !spellOnCooldown && !(stateMachine.currentState is SpellState) && !(stateMachine.currentState is AttackState))
        {
            lastSpellTime = Time.time; // Reset the spell timer
            stateMachine.ChangeState(typeof(SpellState));
            return; // Exit early to prevent further logic from executing this frame
        }

        // Enforce minimum distance without overriding attack/spell actions
        if (distanceToTarget <= minDistanceFromTarget && !(stateMachine.currentState is SpellState) && !(stateMachine.currentState is AttackState))
        {
            stateMachine.ChangeState(typeof(IdleState));
            return; // Keeps the enemy from moving closer once within min distance
        }

        // Approach player if not close enough to attack/cast and not currently performing an action
        if (distanceToTarget > attackRange && !(stateMachine.currentState is WalkState) && !IsCurrentlyAttacking && !(stateMachine.currentState is DeathState))
        {
            stateMachine.ChangeState(typeof(WalkState));
        }

        // Orientation logic for flipping the sprite based on the player's position
        if (!(stateMachine.currentState is DeathState) && !IsCurrentlyAttacking)
        {
            // Flip sprite on the X axis when the enemy is on either side of the target
            if (targetDestination.position.x > transform.position.x) {
                // Target is to the right, ensure sprite is facing right
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            } else {
                // Target is to the left, flip sprite to face left
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
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
            { typeof(DeathState), new DeathState(this) },
            { typeof(AttackState), new AttackState(this, new MeleeAttack(this, animator, warningZoneAttack, enemyTransform, targetDestination, AttackLightningGameObject, attackLightningAnimation)) }, // MeleeAttack is a reference to the MeleeAttack script
            { typeof(SpellState), new SpellState(this, new SpellAttack(this, animator, warningZoneSpell, enemyTransform, targetDestination, SpellGameObject, SpellAnimation)) } // SpellAttack is a reference to the SpellAttack script}
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
            IsCurrentlyAttacking = true;
            attackState.PerformAttack();
        }
    }
    public void OnAttackPerformHitAnimationEvent()
    {
        if (stateMachine.currentState is AttackState attackState)
        {   
            // if the attack state is active, perform the attack
            attackState.PerformHit("Necromancer_Lightning_attack");
            // this is where we used to change IsCurrentlyAttacking to false;
            
        }
    }

    public void OnAttackAnimationEnd()
    {
        Idle();
        IsCurrentlyAttacking = false;
    }

    public void OnSpellHitboxAnimationEvent()
    {
        if (stateMachine.currentState is SpellState spellState)
        {   
            // if the attack state is active, perform the attack
            IsCurrentlyAttacking = true;
            spellState.PerformAttack();
        }
    }
    public void OnSpellPerformHitAnimationEvent()
    {
        if (stateMachine.currentState is SpellState spellState)
        {   
            // if the attack state is active, perform the attack
            spellState.PerformHit("CircleAttackWave");
            // this is where we used to change IsCurrentlyAttacking to false;
            
        }
    }

    public void OnSpellAnimationEnd()
    {
        Idle();
        IsCurrentlyAttacking = false;
    }

    public void TriggerDeathState()
    {
        animator.Play("Necromancer_DeadFlash");
        Destroy(warningZoneAttack);
        stateMachine.ChangeState(typeof(DeathState));
    }

    void OnDrawGizmosSelected() {
        // Set the color of the Gizmo
        Gizmos.color = Color.red;

        // Draw a wire sphere around the GameObject to visualize the attack range
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, spellRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, minDistanceFromTarget);
    }

}



