using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public GameObject startUI;
    public GameObject pauseUI;
    public GameObject deathUI;

    public Slider soundVolumeSlider;
    public Slider musicVolumeSlider;

    public Text scoreText;
    public Image[] heartImages;

    public GameObject messagePrefab;

    public AudioClip buttonSound;
    public AudioClip scoreSound;
    public AudioClip hurtSound;

    public void Start() {
        // Set UI active
        Time.timeScale = 0;
        this.startUI.SetActive(true);
        this.pauseUI.SetActive(false);
        this.deathUI.SetActive(false);

        // Load volume settings
        this.soundVolumeSlider.value = FindObjectOfType<AudioManager>().GetSoundVolume();
        this.musicVolumeSlider.value = FindObjectOfType<AudioManager>().GetMusicVolume();
    }

	public void Update () {
        // Start game if mouse button was pressed
        if (Input.GetMouseButtonDown(0) && this.startUI.activeInHierarchy) {
            FindObjectOfType<AudioManager>().PlaySound(this.buttonSound);
            this.StartGame();
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
        this.deathUI.SetActive(true);

        this.StartCoroutine("DeathWait");
    }

    public void OnScoreChange(int totalScore) {
        this.scoreText.text = "" + totalScore;

        FindObjectOfType<AudioManager>().PlaySound(this.scoreSound, 0.5f);
    }

    public void OnLifeChange(int totalLife) {
        // Enable/disable hearts depending on total life
        for (int i = 0; i < heartImages.Length; i++)
            heartImages[i].enabled = (i < totalLife);

        FindObjectOfType<AudioManager>().PlaySound(this.hurtSound, 2.0f);

        if (totalLife == 0)
            this.OnDeath();
    }

    public void OnSoundVolumeSliderChange() {
        FindObjectOfType<AudioManager>().SetSoundVolume(this.soundVolumeSlider.value);
    }

    public void OnMusicVolumeSliderChange() {
        FindObjectOfType<AudioManager>().SetMusicVolume(this.musicVolumeSlider.value);
    }

    public void StartGame() {
        Time.timeScale = 1;
        this.startUI.SetActive(false);
        this.Reset();
    }

    public void Pause() {
        Time.timeScale = 0;
        this.pauseUI.SetActive(true);
    }

    public void Resume() {
        Time.timeScale = 1;
        this.pauseUI.SetActive(false);
    }

    private IEnumerator DeathWait() {
        yield return new WaitForSeconds(3);

        // Load message with score
        Message message = Instantiate(messagePrefab).GetComponent<Message>();
        message.score = FindObjectOfType<Player>().GetScore();
        DontDestroyOnLoad(message);

        // Load scene
        SceneManager.LoadScene("HighScores");
    }

}
