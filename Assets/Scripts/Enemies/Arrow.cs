using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {
    void Start() {
        Destroy(gameObject, 5f); // Destroys the arrow after 5 seconds
    }


    // private void OnTriggerEnter2D(Collider2D other) {
    //     // Implement what happens when the arrow hits something
    //     Destroy(gameObject); // For example, destroying the arrow
    // }
}
