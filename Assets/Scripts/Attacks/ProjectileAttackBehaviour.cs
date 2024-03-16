using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttackBehavior : MonoBehaviour
{
    protected Vector3 direction;
    public float destroyAfterSeconds;
    
    // amount of enemies the projectile can pierce
    // ( think of it as the health of the projectile reduced by 1 after hitting an enemy )
    public int pierce;
    public float damage; 

    protected virtual void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
    }
    
    public void InitializeProjectile(Vector3 dir, int initialPierce, float initialDamage)
    {
        DirectionChecker(dir);
        pierce = initialPierce;
        damage = initialDamage;
    }
    
    public void DirectionChecker(Vector3 dir) {
        direction = dir;
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        Health healthComponent = other.GetComponent<Health>();
        if (healthComponent != null)
        {
            healthComponent.TakeDamage(damage); // Consider using a variable for damage
            pierce--;

            if (pierce <= 0)
            {
                Destroy(gameObject); // Destroy the projectile if pierce count is zero
            }
        }
    }
}
