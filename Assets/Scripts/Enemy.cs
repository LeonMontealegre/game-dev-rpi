using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float leftWall;
    public float rightWall;

    public float velocity;

    private float direction;

	public void Start () {
        this.Reset();
	}

	public void Update () {
        // Switch direction if we hit a border wall and move out so that we don't get stuck
        if (this.transform.position.x <= leftWall) {
            this.direction = +1;
            this.transform.position += new Vector3(this.velocity * Time.deltaTime, 0, 0);
        }
        if (this.transform.position.x >= rightWall) {
            this.direction = -1;
            this.transform.position -= new Vector3(this.velocity * Time.deltaTime, 0, 0);
        }

        // Move in the current direction
        this.transform.position += new Vector3(this.direction * this.velocity * Time.deltaTime, 0, 0);
    }

    public void Reset() {
        // Reset position
        this.transform.position = new Vector3(0, this.transform.position.y, 0);

        // Randomly choose direction (-1 or +1)
        this.direction = Mathf.Sign(Random.Range(-1, +1));

        // Reset coroutines
        StopAllCoroutines();
        StartCoroutine("ChangeDirection");

        // Reset children
        this.transform.GetChild(0).GetComponent<Spawner>().Reset();
        this.transform.GetChild(1).GetComponent<Spawner>().Reset();
    }

    private IEnumerator ChangeDirection() {
        while (true) {
            // Wait random time
            yield return new WaitForSeconds(Random.Range(1, 4));

            // Change direction
            this.direction *= -1;
        }
    }

}
