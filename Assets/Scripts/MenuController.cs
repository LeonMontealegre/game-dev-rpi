using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    public void OnStartButtonClick() {
        SceneManager.LoadScene("Game");
    }

    public void OnHighScoreButtonClick() {
        SceneManager.LoadScene("HighScores");
    }

    public void OnQuitButtonClick() {
        Application.Quit();
    }

}
