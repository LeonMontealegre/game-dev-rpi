using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public GameObject pauseUI;
    public GameObject deathUI;
    public Text scoreText;
    public Image[] heartImages;

    public AudioClip buttonSound;
    public AudioClip scoreSound;
    public AudioClip hurtSound;

    public void Start() {
        this.deathUI.SetActive(false);
        this.Pause();
    }

	public void Update () {
        // Resume game if mouse button was pressed
        if (Input.GetMouseButtonDown(0)) {
            this.GetComponent<AudioSource>().PlayOneShot(this.buttonSound);
            this.Resume();
        }
    }

    public void Reset() {
        this.scoreText.text = "0";

        // Turn each life image back on
        foreach (Image i in heartImages)
            i.enabled = true;

        // Reset player and enemy
        FindObjectOfType<Player>().Reset();
        FindObjectOfType<Enemy>().Reset();

        // Destroy each droplet
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Drop"))
            Destroy(obj);
    }

    public void OnDeath() {
        SceneManager.LoadScene("HighScores");
        //this.deathUI.SetActive(true);
    }

    public void OnScoreChange(int totalScore) {
        this.scoreText.text = "" + totalScore;
        this.GetComponent<AudioSource>().PlayOneShot(this.scoreSound, 0.5f);
    }

    public void OnLifeChange(int totalLife) {
        // Enable/disable hearts depending on total life
        for (int i = 0; i < heartImages.Length; i++)
            heartImages[i].enabled = (i < totalLife);

        this.GetComponent<AudioSource>().PlayOneShot(this.hurtSound, 2.0f);

        if (totalLife == 0)
            this.OnDeath();
    }

    public void Pause() {
        Time.timeScale = 0;
        this.pauseUI.SetActive(true);
    }

    public void Resume() {
        Time.timeScale = 1;
        this.pauseUI.SetActive(false);
        this.deathUI.SetActive(false);
        this.Reset();
    }
}
