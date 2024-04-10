using UnityEngine;

public class MeleeAttackController : MonoBehaviour
{
    [Header("Melee Attack Stats")]
    private float currentCooldown;
    
    private PlayerStats playerStats;

    protected virtual void Start()
    {
        playerStats = GameManager.Instance.GetPlayerStats();
        currentCooldown = playerStats.bodyAttackSpeed;
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
        currentCooldown = playerStats.bodyAttackSpeed;
    }
}