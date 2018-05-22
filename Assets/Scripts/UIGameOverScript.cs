using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIGameOverScript : MonoBehaviour 
{
    private GameControllerScript gameControllerScript;
    public GameObject highscoresUI, checkpointUI;

    public void ResumeFromLastCheckpointButton()
    {
        gameControllerScript.ResumeFromLastCheckpoint();
    }
    public void DisplayHighscoresButton()
    {
        checkpointUI.SetActive(false);
        highscoresUI.SetActive(true);
    }
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
	void Awake()
	{
        gameControllerScript = FindObjectOfType<GameControllerScript>();
	}
}
