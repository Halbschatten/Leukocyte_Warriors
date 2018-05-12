using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BacteriaScript : MonoBehaviour {

	private GameObject gameControllerGameObject; //Reference to the Game Controller GameObject;
	private string gameControllerTag = "GameController"; //Game Controller's tag;
	private float life = 5.0f;
	private int scoreOnDeath = 125;
	public string bulletTag = "Bullet";
	private PhysicsScript physs;
	public string[] playerTags;
	public GameObject player;
	public bool followPlayer = true;
	public float followSpeedMultiplier = 0.009f;
	public float followSpeed = 5.0f;
    private static float defaultDamage = 1.0f;
    public bool debuffTakeMoreDamage = true;
    public float debuffTakeMoreDamageMultiplier = 1.75f;

	GameObject FindOneRandomPlayer()
	{
		PlayerScript[] players = GameObject.FindObjectsOfType<PlayerScript> ();
		try
		{
			int pos = Random.Range (0, players.Length);
			return players[pos].gameObject;
		}
		catch 
		{
			this.enabled = false; 
			return null;
		}
	}

	void Awake()
	{	
		gameControllerGameObject = GameObject.FindGameObjectWithTag (gameControllerTag);
		physs = this.gameObject.GetComponent<PhysicsScript> ();	
		player = FindOneRandomPlayer ();
	}
	// Use this for initialization
	void Start ()
	{
		

	}
	void FixedUpdate()
	{
		if (followPlayer == true)
		{
			if (player != null) 
			{
				transform.rotation = physs.LookAt2D (this.gameObject, player);
				transform.Translate (Vector2.right * followSpeed * followSpeedMultiplier);
			}
			else 
			{
				player = FindOneRandomPlayer ();
			}
		}
	}
	// Update is called once per frame
	void Update () 
	{

	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == bulletTag)
		{
            if (debuffTakeMoreDamage == true)
            {
                this.life -= defaultDamage * debuffTakeMoreDamageMultiplier;
            }
            else
            {
                this.life -= defaultDamage;
            }
			Destroy (other.gameObject);
			if (this.life <= 0.0f) 
			{
				Destroy (this.gameObject);
				gameControllerGameObject.GetComponent<GameControllerScript> ().Score += scoreOnDeath;
			}
		}
	}
}
