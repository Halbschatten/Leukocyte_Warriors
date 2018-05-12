using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour 
{
	private GameObject gameControllerGameObject; //Reference to the Game Controller GameObject;
	private string gameControllerTag = "GameController"; //Game Controller's tag;
	private GameObject[] players;
	private Vector3 cameraPosition;

	void Awake()
	{
		gameControllerGameObject = GameObject.FindGameObjectWithTag (gameControllerTag);
		players = gameControllerGameObject.GetComponent<GameControllerScript> ().Players;
	}
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		players = gameControllerGameObject.GetComponent<GameControllerScript> ().Players;
		int i;
		for (i = 0; i < players.Length; i++) 
		{
			cameraPosition += players [i].transform.position;
		}
		cameraPosition = new Vector3 (cameraPosition.x / players.Length, cameraPosition.y / players.Length, transform.position.z);
		transform.position = cameraPosition;
	}
}
