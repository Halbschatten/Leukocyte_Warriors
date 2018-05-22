using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HighscorePrompt : MonoBehaviour
{
    public TMP_Text[] player1Name;
    public TMP_Text[] player2Name;
    public TMP_Text score;
    public TMP_Text highscoreListText;
    private List<Highscore> highscoresList = new List<Highscore>();
    private GameControllerScript gameControllerScript;

    void Awake ()
    {
        gameControllerScript = FindObjectOfType<GameControllerScript>();
        score.text += gameControllerScript.Score;
        highscoreListText.text = List10Highscores();
	}
	
    public void Ok()
    {
        string player1NameString = "", player2NameString = "";
        for (int i = 0; i < player1Name.Length; i++)
        {
            player1NameString = player1NameString + player1Name[i].text;
            player2NameString = player2NameString + player2Name[i].text;
        }
        highscoresList.Add(new Highscore(player1NameString, player2NameString, gameControllerScript.Score));
        SaveHighscoreInPlayerPrefs(highscoresList);
        highscoreListText.text = List10Highscores();
    }

    public void SaveHighscoreInPlayerPrefs(List<Highscore> input)
    {
        //input.Sort();
        for(int i = 0; i < input.Count; i++)
        {
            PlayerPrefs.SetString("Highscore_" + i + "_Player1Name", input[i].GetPlayer1Name());
            PlayerPrefs.SetString("Highscore_" + i + "_Player2Name", input[i].GetPlayer2Name());
            PlayerPrefs.SetInt("Highscore_" + i + "_Score", input[i].GetScore());
        }

    }

    public string List10Highscores()
    {
        string output = "PLAYERS\t\t\tSCORE\n";
        for (int i = 0; i < 10; i++)
        {
            output = output + PlayerPrefs.GetString("Highscore_" + i + "_Player1Name") + " | ";
            output = output + PlayerPrefs.GetString("Highscore_" + i + "_Player2Name") + " | ";
            output = output + string.Format("{0:D9}\n", PlayerPrefs.GetInt("Highscore_" + i + "_Score"));
        }
        return output;
    }
}
