using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoresController : MonoBehaviour {

    public GameObject scoresParent;

	void Start () {
        // Load scores from PlayerPrefs
        int[] scores = LoadScores();

        // Set scores in text
        for (int i = 0; i < 10; i++) {
            Text score = scoresParent.transform.GetChild(i).GetComponent<Text>();
            score.text = ((i < 9) ? "  " : "") + (i+1) + ") " + scores[i];
        }
	}

    public static int[] LoadScores() {
        int[] scores = new int[10];

        string data = PlayerPrefs.GetString("scores", "0,0,0,0,0,0,0,0,0,0");
        string[] stringScores = data.Split(',');

        // Convert string to ints
        for (int i = 0; i < 10; i++)
            scores[i] = int.Parse(stringScores[i]);

        return scores;
    }

}
