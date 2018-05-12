using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScore : MonoBehaviour 
{
	private GameObject gameControllerGameObject; //Reference to the Game Controller GameObject;
	private string gameControllerTag = "GameController"; //Game Controller's tag;

	void Awake()
	{
		gameControllerGameObject = GameObject.FindGameObjectWithTag (gameControllerTag);
	}

	// Update is called once per frame
	void Update () 
	{
		GetComponent<Text> ().text = string.Format ("Score: {0:D9}  ", gameControllerGameObject.GetComponent<GameControllerScript>().Score);
	}
}
