using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public int lives;
    public float speed;
    public Sprite[] sprites;

    private int caughtDrops;
    private int currentLives;

    public void Start () {
        this.Reset();
    }

	public void Update () {
        // Don't allow movement if we're dead
        if (this.IsDead())
            return;

        // Get horizontal movement input
        float dx = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        // Move the player
        this.transform.position += new Vector3(dx, 0, 0);

        // Flip the player horizontally in the direction of motion
        if (dx != 0) {
            Vector3 scale = this.transform.localScale;
            this.transform.localScale = new Vector3(Mathf.Sign(dx)*Mathf.Abs(scale.x), scale.y, scale.z);
        }
    }

    public void LoseLife() {
        // Don't do anything if we're already dead
        if (this.IsDead())
            return;

        // Decrement life counter and change sprite
        this.currentLives--;
        this.GetComponent<SpriteRenderer>().sprite = sprites[this.currentLives];

        FindObjectOfType<GameController>().OnLifeChange(this.currentLives);
    }

    public bool IsDead() {
        return (this.currentLives <= 0);
    }

    public void Reset() {
        // Reset position
        this.transform.position = new Vector3(0, this.transform.position.y, 0);

        // Reset to initial sprite
        this.GetComponent<SpriteRenderer>().sprite = sprites[this.lives];

        // Reset score and lives
        this.caughtDrops = 0;
        this.currentLives = this.lives;
    }

    public void OnTriggerEnter2D(Collider2D collider) {
        // Don't do anything if we're already dead
        if (this.IsDead())
            return;

        // Check if we collided with a drop
        if (collider.tag == "Drop") {
            // Destroy the drop and increment the number of caught drops
            Destroy(collider.gameObject);
            this.caughtDrops++;
            FindObjectOfType<GameController>().OnScoreChange(this.caughtDrops);
        }
    }

}
