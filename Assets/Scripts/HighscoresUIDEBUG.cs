using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighscoresUIDEBUG : MonoBehaviour 
{
    private GameControllerScript gameControllerScript;
    public TMP_Text highscoreListText;

	void Awake () 
    {
        gameControllerScript = FindObjectOfType<GameControllerScript>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        highscoreListText.text = gameControllerScript.ListHighscoreFromPlayerPrefs(gameControllerScript.HighscoreList.Count, true);
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetButton("MENU") && Input.GetButtonDown("YELLOW0"))
        {
            gameControllerScript.HighscoreList.Add(new Highscore("DBG", "DBG", Random.Range(0, 1000000000)));
            gameControllerScript.SaveHighscoreInPlayerPrefs(gameControllerScript.HighscoreList);
        }
        if (Input.GetKeyDown(KeyCode.KeypadMinus) || Input.GetButton("MENU") && Input.GetButtonDown("WHITE0"))
        {
            gameControllerScript.HighscoreList.Clear();
            gameControllerScript.SaveHighscoreInPlayerPrefs(gameControllerScript.HighscoreList);
        }
    }
}
