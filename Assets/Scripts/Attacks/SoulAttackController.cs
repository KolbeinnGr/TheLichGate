using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulAttackController : AttackController
{
    private List<GameObject> enemiesInRange = new List<GameObject>();
    
    protected override void Start()
    {
        base.Start();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemiesInRange.Add(other.gameObject);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemiesInRange.Remove(other.gameObject);
        }
    }

    protected override void Attack()
    {
        base.Attack();
        
        GameObject closestEnemy = FindClosestEnemy();
        if (closestEnemy)
        {
            GameObject spawnedProjectile = Instantiate(prefab, transform.position, Quaternion.identity);
            Vector3 attackDirection = (closestEnemy.transform.position - transform.position).normalized;
            spawnedProjectile.GetComponent<ProjectileAttackBehavior>().DirectionChecker(attackDirection);
        }
    }
    
    private GameObject FindClosestEnemy()
    {
        GameObject closestEnemy = null;
        float closestDistance = Mathf.Infinity;
        foreach (GameObject enemy in enemiesInRange)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }
        return closestEnemy;
    }
}
