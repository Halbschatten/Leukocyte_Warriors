using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BacteriaScript : MonoBehaviour
{
	private GameObject gameControllerGameObject; //Reference to the Game Controller GameObject;
	private string gameControllerTag = "GameController"; //Game Controller's tag;
	private float life = 5.0f;
	private int scoreOnDeath = 125;
	public string bulletTag = "Bullet";
	private PhysicsScript physs;
	public string[] playerTags;
	public GameObject player;
	public bool followPlayer = true;
    private float time = 0.0f;
    public float inactiveTime = 0.01f;
	public float followSpeedMultiplier = 0.009f;
	public float followSpeed = 5.0f;
    
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
        gameControllerGameObject.GetComponent<GameControllerScript>().enemies.Add(this.gameObject);
        player = FindOneRandomPlayer ();
	}

	void FixedUpdate()
	{
        time += Time.fixedDeltaTime;
        if (followPlayer == true)
		{
			if (player != null) 
			{
                if (time >= inactiveTime)
                {
                    transform.rotation = physs.LookAt2D(this.gameObject, player);
                    time = 0.0f;
                }
                transform.Translate(Vector2.right * followSpeed * followSpeedMultiplier);
            }
			else 
			{
				player = FindOneRandomPlayer ();
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == bulletTag)
		{
            this.life -= other.gameObject.GetComponent<BulletScript>().damage;
            Destroy (other.gameObject);
			if (this.life <= 0.0f) 
			{
				Destroy (this.gameObject);
				gameControllerGameObject.GetComponent<GameControllerScript> ().Score += scoreOnDeath;
			}
		}
	}
}
