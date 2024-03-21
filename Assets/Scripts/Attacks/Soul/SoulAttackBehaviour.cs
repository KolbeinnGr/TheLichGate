using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulAttackBehaviour : ProjectileAttackBehavior
{

    private SoulAttackController sc;

    protected override void Start()
    {
        base.Start();
        sc = FindObjectOfType<SoulAttackController>();
    }

    void Update()
    {
        if (isMoving)
        {
            transform.position += sc.speed * Time.deltaTime * direction;        

        }
    }
}
