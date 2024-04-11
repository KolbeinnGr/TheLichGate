using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;
    [SerializeField] private float currentHealth;
    private bool isDead = false;


    [SerializeField] private Animator animator;
    private EnemySpawner enemySpawner;

    [Header ("Audio")]
    public AudioClip[] deathSounds;
    public AudioClip[] hurtSounds;

    [Header ("UnityEvents")]
    // Events that can be triggered on health change or death
    public UnityEvent<float> onHealthChanged;
    public UnityEvent onDeath;

    [Header ("Ui for player")]
    public UiHealthBar healthBar;

    [Header ("Floating Text Prefab")]
    public GameObject floatingTextPrefab;

    
    private Canvas canvas;
    private Camera mainCamera;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        currentHealth = maxHealth;
        if(healthBar)
        {
            healthBar.SetMaxHealth(maxHealth);
            healthBar.SetHealth(maxHealth);
        }
        enemySpawner = FindObjectOfType<EnemySpawner>();
        canvas = FindObjectOfType<Canvas>();
        mainCamera = Camera.main;
    }
    

    private void ShowFloatingText(float amount, Color col)
    {
        if (floatingTextPrefab && canvas != null)
        {
            // World position for the text
            Vector3 textWorldPosition = transform.position + new Vector3(0, 1f, 0); // Adjust for offset

            // Instantiate and setup
            GameObject textObj = Instantiate(floatingTextPrefab, canvas.transform);
            FloatingText floatingText = textObj.GetComponent<FloatingText>();
            floatingText.SetText(amount.ToString(), col);
            floatingText.SetWorldPosition(textWorldPosition);
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if(healthBar)
        {
            healthBar.SetHealth(currentHealth);
        }
        onHealthChanged.Invoke(currentHealth / maxHealth); // Invoke with health percentage

        if (currentHealth > 0)
        {
            GetComponent<FlashEffect>().Flash(); // Trigger the flash effect.
            if (AudioManager.Instance && hurtSounds.Length > 0)
            {
                // Play a random hurt sound
                AudioManager.Instance.PlaySound(hurtSounds[Random.Range(0, hurtSounds.Length)]);
            }
        }
        
        if (gameObject.CompareTag("Player"))
        {
            ShowFloatingText(-amount, Color.red);
        }
        else
        {
            ShowFloatingText(-amount, Color.white);
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
        if(healthBar)
        {
            healthBar.SetHealth(currentHealth);
        }
        ShowFloatingText(amount, Color.green);
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
            AudioManager.Instance.PlaySound(deathSounds[Random.Range(0, deathSounds.Length)]);
            
        }
        // Here we would add additional logic for the death of the character such as playing a death animation or disabling the game object
        FadeOutAfterDeathAnimation();
    }

    public void FadeOutAfterDeathAnimation()
    {
        GetComponent<FadeOutOnDeath>().FadeOut();
    }
}