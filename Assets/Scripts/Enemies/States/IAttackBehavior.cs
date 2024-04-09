using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackBehavior // this is an interface for all different attack behaviours to use in the state machine
{
    void Attack();
    void PerformHit(string attackname);
}

