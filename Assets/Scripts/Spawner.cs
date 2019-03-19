using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject spawnPrefab;

    public Vector2 spawnTimes;

	public void Start () {
        this.Reset();
	}

    public void Reset() {
        // Reset coroutines
        StopAllCoroutines();
        StartCoroutine("Spawn");
    }

    private IEnumerator Spawn() {
        // Do forever
        while (true) {
            // Get random time
            float time = Random.Range(this.spawnTimes.x, this.spawnTimes.y);

            // Wait that time
            yield return new WaitForSeconds(time);

            // Spawn new object
            Instantiate(spawnPrefab, this.transform.position, Quaternion.identity);
        }
    }

}
