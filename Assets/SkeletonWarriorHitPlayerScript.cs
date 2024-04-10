using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonWarriorHitPlayerScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) { // this does damage to the player once if they enter while the swing is active
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Health>().TakeDamage(10f);

        }
    }
}
