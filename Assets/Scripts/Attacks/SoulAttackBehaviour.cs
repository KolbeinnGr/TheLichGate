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

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * sc.speed * Time.deltaTime;
    }
}
