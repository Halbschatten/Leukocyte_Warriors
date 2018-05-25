using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class HighscorePrompt : MonoBehaviour
{

    public TMP_Text[] player1Name;
    public TMP_Text[] player2Name;
    public TMP_Text score;
    public TMP_Text highscoreListText;
    public int numberOfHighscoresToDisplay = 10;
    private GameControllerScript gameControllerScript;
    public float timeToWaitBeforeReturningToMainMenu = 5.0f;

    void Awake ()
    {
        gameControllerScript = FindObjectOfType<GameControllerScript>();
        score.text += gameControllerScript.Score;
        highscoreListText.text = gameControllerScript.ListHighscoreFromPlayerPrefs(numberOfHighscoresToDisplay, false);
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
        WaitNSecondsAndReturnToMainMenu(timeToWaitBeforeReturningToMainMenu);
    }

    IEnumerator WaitNSecondsAndReturnToMainMenu(float n)
    {
        yield return new WaitForSeconds(n);
        SceneManager.LoadScene("MainMenu");
    }
}
