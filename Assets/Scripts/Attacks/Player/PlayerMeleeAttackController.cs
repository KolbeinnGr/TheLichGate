using System.Collections;
using UnityEngine;

public class PlayerMeleeAttackController : AttackController
{
    public GameObject slashEffectPrefab; // Assign this in the Inspector
    private bool isFacingRight = true; // Tracks the current direction of the attack
    public float offsetX; // Offsets for the slash effect
    public float offsetY;
    public AnimationClip attackAnimation; // Assign this in the Inspector
    private float animationPlayTime; // Duration of the attack animation
    private AttackController ac; 
    
    [Header ("Audio")]
    public AudioClip[] swingSounds;
    //public float swingSoundVolume = 0.5f;

    private PlayerStats playerStats;

    protected void Awake()
    {
        ac = GetComponent<AttackController>();
        GameManager.Instance.InitializePlayerStats();
        playerStats = GameManager.Instance.GetPlayerStats();
        animationPlayTime = attackAnimation.length;
    }

    protected override void Attack()
    {
        ac.cooldownDuration = playerStats.bodyAttackSpeed;
        base.Attack();
        
        StartCoroutine(PerformAttacksWithDelay());
    }

    private IEnumerator PerformAttacksWithDelay()
    {
        for (int i = 0; i < playerStats.bodyAttacks; i++)
        {
            Vector3 effectPosition = new Vector3(
                transform.position.x + (isFacingRight ? offsetX : -offsetX),
                transform.position.y + offsetY,
                0
            );
            Quaternion effectRotation = isFacingRight ? Quaternion.identity : Quaternion.Euler(0, 180, 0);

            GameObject spawnedSlash = Instantiate(slashEffectPrefab, effectPosition, effectRotation, transform);
            
            // Scale the effect based on bodyAttackSize
            float sizeMultiplier = playerStats.bodyAttackSize;
            spawnedSlash.transform.localScale = new Vector3(sizeMultiplier, sizeMultiplier, sizeMultiplier);

            // Offset the effect to align with the player's position
            spawnedSlash.transform.localPosition -= new Vector3(0, spawnedSlash.transform.localScale.y / 2, 0);
            
            if (AudioManager.Instance)
            {
                // Play a random swing sound
                AudioManager.Instance.PlaySound(swingSounds[Random.Range(0, swingSounds.Length)]);
            }
            
            Destroy(spawnedSlash, animationPlayTime);

            // Alternate attack direction
            isFacingRight = !isFacingRight;

            // Wait for 'speed' seconds before performing the next attack
            yield return new WaitForSeconds(playerStats.bodyAttackSpeed / playerStats.bodyAttacks);
        }
    
        // Wait for the cooldown duration after completing all attacks
        yield return new WaitForSeconds(playerStats.bodyAttackSpeed);
    }
}