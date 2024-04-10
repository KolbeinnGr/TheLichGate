using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpellAttack : IAttackBehavior
{
    private Animator animator;
    private GameObject warningZone;
    private Transform enemyTransform;
    private Transform targetTransform;
    private GameObject attackGameObject;
    private Animator attackAnim;
    private MonoBehaviour coroutineStarter;

    // private float fasterRotationSpeed = 20f;
    // private float slowerRotationSpeed = 5f;

    // private float initialRotationSpeedFast = 40f; // Degrees per second, example value
    // private float initialRotationSpeedSlow = 20f; // Degrees per second, example value
    // private float rotationSlowdownDuration = 1.2f; // Seconds, example value


    public SpellAttack(MonoBehaviour coroutineStarter, Animator animator, GameObject warningZone, Transform enemyTransform, Transform targetTransform, GameObject attackGameObject, Animator attackAnim)
    {
        this.coroutineStarter = coroutineStarter;
        this.animator = animator;
        this.warningZone = warningZone;
        this.enemyTransform = enemyTransform;
        this.targetTransform = targetTransform;
        this.attackGameObject = attackGameObject;
        this.attackAnim = attackAnim;
    }

    public void Attack()
    {
        ShowWarningZone();
        // Trigger the animation that will call PerformAttack() via an animation event
        //animator.SetTrigger("Attack");
    }

    private void ShowWarningZone()
    {
        warningZone.SetActive(true);
        //float initialAngle = CalculateAngleTowardsTarget();
        //warningZone.transform.rotation = Quaternion.Euler(new Vector3(0, 0, initialAngle));
        //coroutineStarter.StartCoroutine(RotateTowardsPlayer(warningZone, fasterRotationSpeed, initialRotationSpeedFast));
    }



    // Call this method from the animation event to perform the "hit" part of the attack
    public void PerformHit(string animationName)
    {
        // Hide the warning zone and prepare for the attack
        warningZone.SetActive(false);

        attackGameObject.SetActive(true);
        // Set the rotation to the final warning zone angle
        // attackGameObject.transform.rotation = warningZone.transform.rotation;
        
        // Continue rotating towards the player at a slower speed, using the last known warning zone angle as the initial angle
        //coroutineStarter.StartCoroutine(RotateTowardsPlayer(attackGameObject, slowerRotationSpeed, initialRotationSpeedSlow));
        
        attackAnim.Play(animationName);
        // Start a coroutine to disable the object after the animation
        coroutineStarter.StartCoroutine(DisableAfterAnimation(animationName));
    }


    // IEnumerator RotateTowardsPlayer(GameObject objectToRotate, float finalRotationSpeed, float initialSpeed)
    // {
    //     float currentSpeed = initialSpeed;
    //     float elapsedTime = 0f;

    //     while (objectToRotate.activeSelf)
    //     {
    //         float targetAngle = CalculateAngleTowardsTarget();

    //         if (elapsedTime < rotationSlowdownDuration)
    //         {
    //             currentSpeed = Mathf.SmoothStep(initialSpeed, finalRotationSpeed, elapsedTime / rotationSlowdownDuration);
    //             elapsedTime += Time.deltaTime;
    //         }
    //         else
    //         {
    //             currentSpeed = finalRotationSpeed;
    //         }

    //         Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);
    //         objectToRotate.transform.rotation = Quaternion.RotateTowards(
    //             objectToRotate.transform.rotation, 
    //             targetRotation, 
    //             currentSpeed * Time.deltaTime
    //         );

    //         yield return null; // Wait for the next frame
    //     }
    // }



    private float CalculateAngleTowardsTarget()
    {
        Vector3 direction = targetTransform.position + (Vector3.up * 0.65f) - warningZone.transform.position;
        return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
    }


    IEnumerator DisableAfterAnimation(string clipName)
    {
        // Get the length of the animation
        float clipLength = GetAnimationClipLength(clipName);
        
        // Wait for the animation to finish
        yield return new WaitForSeconds(clipLength);
        
        // Disable the GameObject
        attackGameObject.SetActive(false);
    }

    float GetAnimationClipLength(string clipName)
    {
        AnimationClip[] clips = attackAnim.runtimeAnimatorController.animationClips;
        foreach(AnimationClip clip in clips)
        {
            if(clip.name == clipName)
            {
                return clip.length;
            }
        }
        return 0f; // Return 0 if not found
    }

}



