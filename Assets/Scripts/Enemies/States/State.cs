using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Assuming State does NOT inherit from MonoBehaviour
public abstract class State
{
    protected IEnemy enemy;

    public State(IEnemy enemy)
    {
        this.enemy = enemy;
    }

    public virtual void Enter() { }
    public virtual void Do() { }
    public virtual void FixedDo() { }
    public virtual void Exit() { }
}

