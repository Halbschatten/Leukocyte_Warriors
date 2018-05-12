using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControllerScript : MonoBehaviour 
{
	private float screenBoundariesX = 8.5f, screenBoundariesY = 4.5f; //Defines the Boundary to be used on all scripts.
    public float ScreenBoundariesX //Makes the values accesible to other scripts.
    {
        get
        {
            return screenBoundariesX;
        }
        set
        {
            //screenBoundariesX = value; //Disabled because we need to protect the screen boundaries, making it read-only.
        }
    }
    public float ScreenBoundariesY //Makes the values accesible to other scripts.
    {
        get
        {
            return screenBoundariesY;
        }
        set
        {
            //screenBoundariesY = value; //Disabled because we need to protect the screen boundaries, making it read-only.
        }
    }
	private int[] playersScores;
	public int[] PlayersScores
	{
		get
		{
			return playersScores;
		}
		set
		{
			playersScores = value;
		}
	}
	private int score;
	public int Score
	{
		get
		{
			return score;
		}
		set
		{
			score = value;
		}
	}
	private bool gameOver = false;
	public bool GameOver
	{
		get
		{
			return gameOver;
		}
	}
	private GameObject[] players;
	public GameObject[] Players
	{
		get
		{
			return players;
		}
		set
		{
			players = value;
		}
	}
	private float[] playersHealth;
	public float[] PlayersHealth
	{
		get
		{
			return playersHealth;
		}
		set
		{
			playersHealth = value;
		}
	}
	public float bgScrollSpeed;
	public GameObject[] FindAllPlayers()
	{
		PlayerScript[] playerScripts = GameObject.FindObjectsOfType<PlayerScript> ();
		if (playerScripts.Length == 0)
		{
			return null;
		}
		GameObject[] players = new GameObject[playerScripts.Length];
		int i;
		for (i = 0; i < playerScripts.Length; i++) 
		{
			players [i] = playerScripts [i].gameObject;
		}
		return players;
	}
	public GameObject FindOneRandomPlayer()
	{
		PlayerScript[] players = GameObject.FindObjectsOfType<PlayerScript> ();
		try
		{
			int pos = Random.Range (0, players.Length);
			return players[pos].gameObject;
		}
		catch 
		{
			return null;
		}
	}
	public GameObject uiGameOverGameObject;
	public GameObject uiPlayer1HPGameObject;
	public GameObject uiPlayer2HPGameObject;
	public GameObject uiScoreGameObject;
	public GameObject pSystem;
	void Awake()
	{
		players = FindAllPlayers ();
        if (FindAllPlayers() != null)
        {
            playersHealth = new float[FindAllPlayers().Length];
        }
	}
	// Use this for initialization
	void Start () 
	{

	}
	void GameOverMethod()
	{
		if (gameOver == false) 
		{
			gameOver = true;
			uiPlayer1HPGameObject.gameObject.SetActive(false);
			uiPlayer2HPGameObject.gameObject.SetActive(false);
			uiScoreGameObject.gameObject.SetActive(false);
			uiGameOverGameObject.gameObject.SetActive(true);
			pSystem.GetComponent<ParticleSystem>().Stop();
			//Implement Game Over Code Here;
			Debug.Log ("Game Over! Returning to Main Menu in 5 seconds!");
			StartCoroutine(WaitForNSecondsAndReturnToMainMenu(5));
		}
	}
	IEnumerator WaitForNSecondsAndReturnToMainMenu(int n)
	{
		 yield return new WaitForSeconds(n);
		 SceneManager.LoadScene("MainMenu");
	}
	// Update is called once per frame
	void Update () 
	{
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
        players = FindAllPlayers ();
		if (players == null) 
		{
			GameOverMethod ();
		}
//		foreach (float hp in playersHealth) 
//		{
//			print (hp);
//		}
	}
}
