using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    // Events that can be triggered on health change or death
    public UnityEvent<float> onHealthChanged;
    public UnityEvent onDeath;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        onHealthChanged.Invoke(currentHealth / maxHealth); // Invoke with health percentage

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth) 
        {
            currentHealth = maxHealth;
        }
        onHealthChanged.Invoke(currentHealth / maxHealth); // Invoke with health percentage
    }

    private void Die()
    {
        onDeath.Invoke();
        // Here we would add additional logic for the death of the character such as playing a death animation or disabling the game object
        Destroy(gameObject); // TODO , remove once better death logic is implemented
    }
}