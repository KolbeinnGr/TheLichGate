using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/*
I was gonna write this myself, but I started yapping to much. so here is a description from chatgpt:

Overview of Classes and Their Interactions
IEnemy Interface: Defines the common functionalities and properties that any enemy character must implement, 
including movement, attacking, and idle behaviors. It abstracts the enemy's actions, allowing states to interact with any enemy type uniformly.

State Classes: These are non-MonoBehaviour classes that define the behavior for different states an enemy can be in (e.g., walking, attacking, idling). 
Each state has methods like Enter(), Do(), FixedDo(), and Exit() to manage the state's lifecycle and actions.

StateMachine: A MonoBehaviour attached to enemy GameObjects, responsible for managing transitions between states. 
It holds a reference to the current state and calls its Do() and FixedDo() methods appropriately.

EnemySkeletonArcher: This MonoBehaviour implements the IEnemy interface and controls the archer-specific behaviors and properties, 
such as its target, speed, and animations. It contains a StateMachine instance to manage its states.

Walking State Example
Let's follow the archer as it transitions into and operates within the WalkState.

Step 1: Initialization
When the game starts, EnemySkeletonArcher.Awake() is called. This method initializes components like the Rigidbody2D and Animator, 
and also sets up the state machine by calling InitializeStateMachine().
InitializeStateMachine() creates instances of the states the archer can be in, 
including WalkState, and associates them with the state machine.

Step 2: Transition to WalkState
The transition to WalkState can be triggered in various ways, typically based on game logic that checks conditions 
like the distance between the archer and its target. If the archer is not in attack range but needs to move closer to its target, 
the game logic decides it's time to walk.
This logic calls StateMachine.ChangeState(typeof(WalkState)), telling the state machine to switch to the WalkState.

Step 3: Entering WalkState
WalkState.Enter() is called by the state machine as part of the state transition. 
Here, the archer's Animator is set to enable the walking animation by setting a boolean like IsWalking to true. 
The Enter() method is where you set up everything needed for the state (e.g., animations, initial velocity).

Step 4: Continuous Walking Behavior
As long as the archer remains in WalkState, the state machine's FixedUpdate() method calls WalkState.FixedDo() on each physics update. 
This method calculates the direction to the target and sets the archer's velocity accordingly, making it move towards the target.
The continuous walking behavior is managed within FixedDo(), allowing for smooth, frame-rate independent movement.

Step 5: Exiting WalkState
If the game logic determines another state is now appropriate (e.g., the target is within attack range, so it's time to attack), 
the state machine will transition the archer out of WalkState.
WalkState.Exit() is called, where cleanup for leaving the state occurs. 
This might involve stopping the archer's movement by setting its velocity to zero and turning off the walking animation by setting IsWalking to false.

Summary
This structure allows for clear separation of concerns, where each class and state is responsible for a specific aspect of behavior. 
The StateMachine cleanly manages transitions based on game logic, while the IEnemy interface ensures that states can work with any enemy type. 
States encapsulate specific behaviors, such as walking towards a target, making it easy to add new behaviors or 
modify existing ones without affecting other parts of the enemy's logic.



*/

public class EnemySkeletonArcher : MonoBehaviour, IEnemy
{
    // initialize components
    [SerializeField] private Transform targetDestination;
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed = 3f;
    [SerializeField] private float attackRange = 5f;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private float arrowSpeed = 5f;

    // state machine
    private StateMachine stateMachine;

    // IEnemy interface
    public Transform TargetDestination => targetDestination;
    public Animator Animator => animator;
    public Rigidbody2D Rb => rb;
    public float Speed => speed;

    private void Awake()
    {
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
            { typeof(AttackState), new AttackState(this, new RangedAttack(arrowPrefab, shootingPoint, arrowSpeed, targetDestination)) } // RangedAttack is an example of an attack that uses a projectile.
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

    // This method is called by the Animation methods to trigger instantiating of the arrow at the correct time.
    public void OnShootArrowAnimationEvent()
    {
        if (stateMachine.currentState is AttackState attackState)
        {   
            // if the attack state is active, perform the attack
            attackState.PerformAttack();
        }
    }

}



