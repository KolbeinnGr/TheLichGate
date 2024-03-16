using System;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    // This dictionary is used to store all available states for the enemy.
    private Dictionary<Type, State> availableStates;

    // This is the current state of the enemy.
    public State currentState { get; private set; }

    // This method is used to initialize the state machine.
    public void SetStates(Dictionary<Type, State> states)
    {
        availableStates = states;
    }

    // This method handles the changing of states. Exiting and entering states as necessary.
    public void ChangeState(Type newStateType)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = availableStates[newStateType];
        currentState.Enter();
    }

    private void Update()
    {
        currentState?.Do();
    }

    private void FixedUpdate()
    {
        currentState?.FixedDo();
    }
}
