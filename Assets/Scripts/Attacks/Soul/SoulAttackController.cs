using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulAttackController : AttackController
{
    private List<GameObject> enemiesInRange = new List<GameObject>();
    private AttackController ac; 
    
    private PlayerStats playerStats;
    public AudioClip soulAttackSound;

    protected override void Start()
    {
        base.Start();
        playerStats = GameManager.Instance.GetPlayerStats();
    }

    protected void Awake()
    {
        ac = GetComponent<AttackController>();
        
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
    
    protected override void Attack()
    {
        base.Attack();
        ac.cooldownDuration = playerStats.soulAttackSpeed;
        GameObject closestEnemy = FindClosestEnemy();
        if (closestEnemy)
        {
            StartCoroutine(AttackSequence(closestEnemy));
        }
    }

    private IEnumerator AttackSequence(GameObject target)
    {
        for (int i = 0; i < playerStats.soulAttackProjectiles; i++)
        {
            StartCoroutine(LaunchProjectilesWithDelay(target));
        }
        yield return new WaitForSeconds(playerStats.soulAttackSpeed);
    }

    private IEnumerator LaunchProjectilesWithDelay(GameObject target)
    {
        GameObject spawnedProjectile = Instantiate(prefab, transform.position, Quaternion.identity);
        Vector3 attackDirection = ((target.transform.position + (Vector3.up * 0.8f)) - transform.position).normalized;
        ProjectileAttackBehavior projectile = spawnedProjectile.GetComponent<ProjectileAttackBehavior>();
        if (projectile)
        {
            projectile.InitializeProjectile(attackDirection);
            if (AudioManager.Instance && soulAttackSound)
            {
                AudioManager.Instance.PlaySound(soulAttackSound);
            }
        }

        yield return new WaitForSeconds(0.2f); // Delay between projectiles within the same attack
    }
    
    
    private void SpreadShot(GameObject target)
    {
        float spreadAngle = 10f; // Total spread angle
        float angleStep = spreadAngle / (playerStats.soulAttackProjectiles - 1);

        for (int i = 0; i < playerStats.soulAttackProjectiles; i++)
        {
            GameObject spawnedProjectile = Instantiate(prefab, transform.position, Quaternion.identity);
            Vector3 attackDirection = ((target.transform.position + (Vector3.up * 0.8f)) - transform.position).normalized;

            // Calculate the rotation for the current projectile
            float currentAngle = -spreadAngle / 2 + angleStep * i;
            Quaternion rotation = Quaternion.Euler(0, 0, currentAngle);
            Vector3 rotatedDirection = rotation * attackDirection;

            ProjectileAttackBehavior projectile = spawnedProjectile.GetComponent<ProjectileAttackBehavior>();
            if (projectile)
            {
                projectile.InitializeProjectile(rotatedDirection);
            }
        }
    }
}
