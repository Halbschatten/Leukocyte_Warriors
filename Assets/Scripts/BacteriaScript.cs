using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BacteriaScript : MonoBehaviour
{
    bool isQuitting;
    private GameControllerScript gameControllerScript; //Reference to the Game Controller GameObject;
    private static float defaultLife = 5.0f;
    private float life = defaultLife;
    public float Life
    {
        get
        {
            return life;
        }
        set
        {
            life = value;
        }
    }
    public float GetDefaultLife
    {
        get
        {
            return defaultLife;
        }
    }

    private int scoreOnDeath = 125;
    public string bulletTag = "Bullet";
    private PhysicsScript physs;
    public GameObject player;
    public bool followPlayer = true;
    private float time = 0.0f;
    public float inactiveTime = 0.01f;
    public float followSpeedMultiplier = 0.009f;
    public float followSpeed = 5.0f;

    void Awake()
    {
        gameControllerScript = GameObject.FindObjectOfType<GameControllerScript>();
        physs = new PhysicsScript();
        gameControllerScript.enemies.Add(this.gameObject);
        player = gameControllerScript.FindOneRandomPlayer();
    }

    void FixedUpdate()
    {
        time += Time.fixedDeltaTime;
        if (followPlayer == true)
        {
            if (player != null)
            {
                if (player.activeSelf == true)
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
                    player = gameControllerScript.FindOneRandomPlayer();
                }
            }
        }
    }

    private void Update()
    {
        if (this.life <= 0.0f)
        {
            gameControllerScript.Score += scoreOnDeath;
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == bulletTag)
        {
            this.life -= other.gameObject.GetComponent<BulletScript>().damage;
            Destroy(other.gameObject);
            if (this.life <= 0.0f)
            {
                gameControllerScript.Score += scoreOnDeath;
                Destroy(this.gameObject);
            }
        }
    }
    void OnApplicationQuit()
    {
        isQuitting = true;
    }
    private void OnDestroy()
    {
        if (!isQuitting && gameControllerScript != null)
        {
            if (Random.Range(0, 6) == 3)
            {

                bool isSpawnGoingToBeBuff;
                if (Random.Range(0, 2) == 0)
                {
                    isSpawnGoingToBeBuff = false;
                }
                else
                {
                    isSpawnGoingToBeBuff = true;
                }
                if (isSpawnGoingToBeBuff == true)
                {
                    int randomSpawn = Random.Range(0, gameControllerScript.buffPickups.Length);
                    Instantiate(gameControllerScript.buffPickups[randomSpawn], this.transform.position, Quaternion.identity);
                }
                else
                {
                    int randomSpawn = Random.Range(0, gameControllerScript.debuffPickups.Length);
                    Instantiate(gameControllerScript.debuffPickups[randomSpawn], this.transform.position, Quaternion.identity);
                }
            }
        }
    }
}
