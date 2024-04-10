using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : IAttackBehavior // implements IAttackBehavior to use in the state machine
{
    private GameObject arrowPrefab;
    private Transform shootingPoint;
    private float arrowSpeed;
    private Transform targetDestination;

    public RangedAttack(GameObject arrowPrefab, Transform shootingPoint, float arrowSpeed, Transform targetDestination)
    {
        this.arrowPrefab = arrowPrefab;
        this.shootingPoint = shootingPoint;
        this.arrowSpeed = arrowSpeed;
        this.targetDestination = targetDestination;
    }

    public void Attack()
    {
        Vector3 shotpos = shootingPoint.position + Vector3.up * 0.8f;
        GameObject arrow = GameObject.Instantiate(arrowPrefab, shotpos, Quaternion.identity);
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        Vector2 direction = (targetDestination.position - shootingPoint.position).normalized;
        rb.velocity = direction * arrowSpeed;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        arrow.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void PerformHit(string animationName)
    {
        
    }
}

