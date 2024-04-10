using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromancerHitPlayerWaveScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {// this does damage to the player once if they enter while the wave is active
        if (other.CompareTag("Player"))
        {
            // Debug.Log("Player hit by wave attack");
            
            other.GetComponent<Health>().TakeDamage(30f);
        }
    }
}
