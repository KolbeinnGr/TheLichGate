using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttackBehavior : MonoBehaviour
{
    protected Vector3 direction;
    public float destroyAfterSeconds;
    private Animator animator;
    protected bool isMoving = true;
    
    // amount of enemies the projectile can pierce
    // ( think of it as the health of the projectile reduced by 1 after hitting an enemy )
    private int pierce;
    private float damage;
    protected PlayerStats playerStats;

    protected virtual void Start()
    {
        playerStats = GameManager.Instance.GetPlayerStats();
        animator = GetComponent<Animator>();
        Destroy(gameObject, destroyAfterSeconds);
    }
    
    public void InitializeProjectile(Vector3 dir)
    {
        DirectionChecker(dir);
        pierce = playerStats.soulAttackPierce;
        damage = playerStats.soulAttackDamage;
    }
    
    public void DirectionChecker(Vector3 dir) {
        direction = dir;
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            return;
        }

        Health healthComponent = other.GetComponent<Health>();
        if (healthComponent != null)
        {
            healthComponent.TakeDamage(damage); 
            pierce--;

            if (pierce <= 0)
            {
                // set the speed of the projectile to zero
                isMoving = false;
                animator.SetBool("IsEnding", true);
                Destroy(gameObject, 0.3f); // Destroy the projectile if pierce count is zero
            }
        }
    }
}
