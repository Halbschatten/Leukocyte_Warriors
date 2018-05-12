using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILife : MonoBehaviour 
{
	private GameObject gameControllerGameObject; //Reference to the Game Controller GameObject;
	private string gameControllerTag = "GameController"; //Game Controller's tag;
	public GameObject player;
	public int playerNumber;

	void Awake()
	{
		gameControllerGameObject = GameObject.FindGameObjectWithTag (gameControllerTag);
	}

	// Update is called once per frame
	void Update () 
	{
		GetComponent<Text> ().text = string.Format ("Player {0} HP: {1}", playerNumber + 1, gameControllerGameObject.GetComponent<GameControllerScript>().PlayersHealth[playerNumber]);
	}
}
