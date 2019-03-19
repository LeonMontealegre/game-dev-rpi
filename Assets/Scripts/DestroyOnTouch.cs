using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnTouch : MonoBehaviour {

    public void OnTriggerEnter2D(Collider2D other) {
        // Check if we collided with a drop
        if (other.tag == "Drop") {
            // Destroy the drop
            Destroy(other.gameObject);

            // Make player lose a life
            FindObjectOfType<Player>().LoseLife();
        }
    }

}
