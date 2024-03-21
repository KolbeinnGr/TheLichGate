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

    protected void Awake()
    {
        animationPlayTime = attackAnimation.length;
    }

    protected override void Attack()
    {
        base.Attack();
        StartCoroutine(PerformAttacksWithDelay());
    }

    private IEnumerator PerformAttacksWithDelay()
    {
        for (int i = 0; i < attacks; i++)
        {
            Vector3 effectPosition = new Vector3(
                transform.position.x + (isFacingRight ? offsetX : -offsetX),
                transform.position.y + offsetY,
                0
            );
            Quaternion effectRotation = isFacingRight ? Quaternion.identity : Quaternion.Euler(0, 180, 0);

            GameObject spawnedSlash = Instantiate(slashEffectPrefab, effectPosition, effectRotation, transform);
            Destroy(spawnedSlash, animationPlayTime);

            // Alternate attack direction
            isFacingRight = !isFacingRight;

            // Wait for 'speed' seconds before performing the next attack
            yield return new WaitForSeconds(speed);
        }

        // Wait for the cooldown duration after completing all attacks
        yield return new WaitForSeconds(cooldownDuration);
    }
}