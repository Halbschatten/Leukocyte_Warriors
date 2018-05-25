using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class HighscorePrompt : MonoBehaviour
{

    public Button okButton;
    public TMP_Text okInfoText;
    public TMP_Text[] player1Name;
    public TMP_Text[] player2Name;
    public TMP_Text score;
    public TMP_Text highscoreListText;
    public int numberOfHighscoresToDisplay = 10;
    private GameControllerScript gameControllerScript;
    public float timeToWaitBeforeReturningToMainMenu = 2.5f;

    void Awake ()
    {
        gameControllerScript = FindObjectOfType<GameControllerScript>();
        score.text += gameControllerScript.Score;
        highscoreListText.text = gameControllerScript.ListHighscoreFromPlayerPrefs(numberOfHighscoresToDisplay, false);
        okInfoText.gameObject.SetActive(false);
    }
	
    public void Ok()
    {
        string player1NameString = "", player2NameString = "";
        for (int i = 0; i < player1Name.Length; i++)
        {
            player1NameString = player1NameString + player1Name[i].text;
            player2NameString = player2NameString + player2Name[i].text;
        }
        gameControllerScript.HighscoreList.Add(new Highscore(player1NameString, player2NameString, gameControllerScript.Score));
        gameControllerScript.SaveHighscoreInPlayerPrefs(gameControllerScript.HighscoreList);
        highscoreListText.text = gameControllerScript.ListHighscoreFromPlayerPrefs(numberOfHighscoresToDisplay, false);
        StartCoroutine(WaitNSecondsAndReturnToMainMenu(timeToWaitBeforeReturningToMainMenu));
        okButton.interactable = false;
        okInfoText.text = "Returning to Main Menu...";
        okInfoText.gameObject.SetActive(true);
    }

    IEnumerator WaitNSecondsAndReturnToMainMenu(float n)
    {
        yield return new WaitForSeconds(n);
        SceneManager.LoadScene("MainMenu");
    }
}
