using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameOverScript : MonoBehaviour 
{
    private GameControllerScript gameControllerScript;

    public void ResumeFromLastCheckpointButton()
    {
        gameControllerScript.ResumeFromLastCheckpoint();
    }
    public void DisplayHighscoresButton()
    {

    }
	void Awake()
	{
        gameControllerScript = FindObjectOfType<GameControllerScript>();
	}

	// Update is called once per frame
	void Update () 
	{

	}
}
