using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {
    void Start() {
        Destroy(gameObject, 5f); // Destroys the arrow after 5 seconds
    }


    private void OnTriggerEnter2D(Collider2D other) { // this does damage to the player once if they enter while the arrow is active
        if (other.CompareTag("Player"))
        {
            //Debug.Log("Player hit by arrow");
            other.GetComponent<Health>().TakeDamage(21.3f);
            Destroy(gameObject);
        }
    }
}
