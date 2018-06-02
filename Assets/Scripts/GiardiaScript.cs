using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiardiaScript : MonoBehaviour
{
    bool isQuitting;
    bool killedByPlayer;
    private GameControllerScript gameControllerScript; //Reference to the Game Controller GameObject;
    private static float defaultLife = 15.0f;
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
    private int scoreOnDeath = 1250;
    public string bulletTag = "Bullet";
    private float time;
    private float randomSpeed;

    void Awake()
    {
        gameControllerScript = GameObject.FindObjectOfType<GameControllerScript>();
        gameControllerScript.enemies.Add(this.gameObject);
        randomSpeed = Random.Range(12, 24);
    }

    private void FixedUpdate()
    {
        time += Time.fixedDeltaTime;
        float cosMovement = Mathf.Cos(2 * Mathf.PI * time / randomSpeed);
        transform.position = new Vector3((12.0f * cosMovement), transform.position.y, transform.position.z);
        if (cosMovement <= -0.98f)
        {
            transform.eulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, 270.0f);
            transform.position = new Vector3(transform.position.x, Random.Range(-gameControllerScript.ScreenBoundariesY, gameControllerScript.ScreenBoundariesY));
        }
        if (cosMovement >= 0.98f)
        {
            transform.eulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, 90.0f);
            transform.position = new Vector3(transform.position.x, Random.Range(-gameControllerScript.ScreenBoundariesY, gameControllerScript.ScreenBoundariesY));
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
                killedByPlayer = true;
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
            if (Random.Range(0, 2) == 1)
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
