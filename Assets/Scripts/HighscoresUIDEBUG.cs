using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighscoresUIDEBUG : MonoBehaviour 
{
    private GameControllerScript gameControllerScript;
    private List<Highscore> highscoresList;
    public TMP_Text highscoreListText;

	void Awake () 
    {
        gameControllerScript = FindObjectOfType<GameControllerScript>();
        gameControllerScript = FindObjectOfType<GameControllerScript>();
        highscoresList = new List<Highscore>(gameControllerScript.ReadHighscoresFromPlayerPrefs());
	}
	
	// Update is called once per frame
	void Update () 
    {
        highscoreListText.text = gameControllerScript.ListHighscoreFromPlayerPrefs(highscoresList.Count, true);
	}
}
