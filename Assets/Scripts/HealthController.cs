using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    private bool isDead = false;


    [SerializeField] private Animator animator;
    private EnemySpawner enemySpawner;

    [Header ("Audio")]
    public AudioClip[] deathSounds;
    public AudioClip[] hurtSounds;
    public float soundVolume = 0.3f;

    [Header ("UnityEvents")]
    // Events that can be triggered on health change or death
    public UnityEvent<float> onHealthChanged;
    public UnityEvent onDeath;



    private void Awake() {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        currentHealth = maxHealth;
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        onHealthChanged.Invoke(currentHealth / maxHealth); // Invoke with health percentage

        if (currentHealth > 0)
        {
            GetComponent<FlashEffect>().Flash(); // Trigger the flash effect.
            if (AudioManager.Instance && hurtSounds.Length > 0)
            {
                // Play a random hurt sound
                AudioManager.Instance.PlaySound(hurtSounds[Random.Range(0, hurtSounds.Length)], soundVolume);
            }
        }
        

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
        if (isDead) return;
        isDead = true;
        enemySpawner.OnEnemyKilled(); // To help keep track of no. of enemies on the stage in the enemy spawner.
       
        onDeath.Invoke();

        // Use SendMessage to call TriggerDeathState on any attached script
        SendMessage("TriggerDeathState", SendMessageOptions.DontRequireReceiver);
        
        if (AudioManager.Instance && deathSounds.Length > 0)
        {
            // Play a random Death sound
            AudioManager.Instance.PlaySound(deathSounds[Random.Range(0, deathSounds.Length)], soundVolume);
            
        }
        // Here we would add additional logic for the death of the character such as playing a death animation or disabling the game object
        FadeOutAfterDeathAnimation();
    }

    public void FadeOutAfterDeathAnimation()
    {
        GetComponent<FadeOutOnDeath>().FadeOut();
    }
}