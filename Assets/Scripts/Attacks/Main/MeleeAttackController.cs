using UnityEngine;

public class MeleeAttackController : MonoBehaviour
{
    [Header("Melee Attack Stats")]
    public float damage;
    public float attackRange;
    public float cooldownDuration;
    private float currentCooldown;

    protected virtual void Start()
    {
        currentCooldown = cooldownDuration;
    }

    protected virtual void Update()
    {
        currentCooldown -= Time.deltaTime;
        if (currentCooldown <= 0f)
        {
            PerformAttack();
        }
    }

    protected virtual void PerformAttack()
    {
        // This method will be overridden in derived classes
        currentCooldown = cooldownDuration;
    }
}