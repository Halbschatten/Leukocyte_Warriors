using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class HighscorePrompt : MonoBehaviour
{
    void HandleFunc(Highscore arg1)
    {
    }


    public TMP_Text[] player1Name;
    public TMP_Text[] player2Name;
    public TMP_Text score;
    public TMP_Text highscoreListText;
    private List<Highscore> highscoresList = new List<Highscore>();
    private GameControllerScript gameControllerScript;

    void Awake ()
    {
        highscoresList = ReadHighscoresFromPlayerPrefs();
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
        input = OrderByDescend(input);
        for(int i = 0; i < input.Count; i++)
        {
            PlayerPrefs.SetString("Highscore_" + i + "_Player1Name", input[i].GetPlayer1Name());
            PlayerPrefs.SetString("Highscore_" + i + "_Player2Name", input[i].GetPlayer2Name());
            PlayerPrefs.SetInt("Highscore_" + i + "_Score", input[i].GetScore());
            PlayerPrefs.SetInt("Highscore_Count", input.Count);
        }
    }

    public List<Highscore> ReadHighscoresFromPlayerPrefs()
    {
        int highscoreCount =  PlayerPrefs.GetInt("Highscore_Count");
        List<Highscore> aux = new List<Highscore>();
        for (int i = 0; i < highscoreCount; i++)
        {
            aux.Add(new Highscore(PlayerPrefs.GetString("Highscore_" + i + "_Player1Name"), PlayerPrefs.GetString("Highscore_" + i + "_Player2Name"), PlayerPrefs.GetInt("Highscore_" + i + "_Score")));
        }
        foreach (Highscore h in aux){
            print(string.Format("{0} | {1}  | {2}", h.GetPlayer1Name(), h.GetPlayer2Name(), h.GetScore()));
        }
        return aux;
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

    public List<Highscore> OrderByDescend (List<Highscore> input)
    {
        return input.OrderByDescending(h => h.GetScore()).ToList();
    }
}
