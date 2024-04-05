using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    [Header ("Attack Stats")]
    public GameObject prefab;
    public float damage;
    public float speed; 
    public float cooldownDuration; // time between attacks
    private float currentCooldown; // time until next attack
    public int pierce; // amount of enemies the projectile can pierce
    public int attacks; // amount of projectiles or attacks to spawn

    
    
    // Start is called before the first frame update
    protected virtual void Start()
    {
        currentCooldown = cooldownDuration;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        currentCooldown -= Time.deltaTime;
        if (currentCooldown < 0f)
        {
            Attack();
        }
    }
    
    protected virtual void Attack()
    {
        currentCooldown = cooldownDuration;
    }
}
