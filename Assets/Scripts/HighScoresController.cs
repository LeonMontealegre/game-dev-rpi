using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HighScoresController : MonoBehaviour {

    public GameObject scoresParent;

    public GameObject tryAgainButton;
    public GameObject quitButton;

	void Start () {
        // Load scores from PlayerPrefs
        int[] scores = LoadScores();

        // Check if a message was sent
        Message msg = FindObjectOfType<Message>();
        if (msg != null) {
            // Check if the new score is higher than any other
            int score = msg.score;
            for (int i = 0; i < 10; i++) {
                if (score >= scores[i]) {
                    // Update scores with new high score
                    scores = UpdateScores(scores, i, score);

                    // Destroy message
                    Destroy(msg.gameObject);
                    break;
                }
            }

            // Set some UI elements visible
            this.tryAgainButton.SetActive(true);
            this.quitButton.SetActive(true);
        }

        // Set scores in text
        for (int i = 0; i < 10; i++) {
            Text score = scoresParent.transform.GetChild(i).GetComponent<Text>();
            score.text = ((i < 9) ? "  " : "") + (i+1) + ") " + scores[i];
        }
	}

    public void OnTryAgainButtonPress() {
        SceneManager.LoadScene("Game");
    }

    public void OnMenuButtonPress() {
        SceneManager.LoadScene("Menu");
    }

    public void OnQuitButtonPress() {
        Application.Quit();
    }

    private int[] UpdateScores(int[] oldScores, int rank, int newScore) {
        int[] scores = new int[10];
        string scoreString = "";
        for (int i = 0; i < 10; i++) {
            if (i == rank) {
                scores[i] = newScore;
            } else {
                // Shift scores down
                scores[i] = oldScores[(i < rank) ? i : i - 1];
            }

            // Create prefs string
            scoreString += scores[i] + (i < 9 ? "," : "");
        }

        // Save scores to player prefs
        PlayerPrefs.SetString("scores", scoreString);

        return scores;
    }

    private int[] LoadScores() {
        int[] scores = new int[10];

        string data = PlayerPrefs.GetString("scores", "0,0,0,0,0,0,0,0,0,0");
        string[] stringScores = data.Split(',');

        // Convert string to ints
        for (int i = 0; i < 10; i++)
            scores[i] = int.Parse(stringScores[i]);

        return scores;
    }

}
