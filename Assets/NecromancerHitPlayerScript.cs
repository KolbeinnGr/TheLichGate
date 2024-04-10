using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromancerHitPlayerScript : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other) { // this does damage to the player every frame that they stay inside the lightning attack.
        if (other.CompareTag("Player"))
        {
            // Debug.Log("Lightning hit player this frame");
            other.GetComponent<Health>().TakeDamage(1f);
        }
    }
}
