using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiardiaScript : MonoBehaviour
{
    bool isQuitting;
    private GameControllerScript gameControllerScript; //Reference to the Game Controller GameObject;
    public SpriteRenderer[] sprComponents;
    private static float defaultLife = 10.0f;
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
    private Vector3 velocity = new Vector3(-4.0f, 0.0f, 0.0f);
    private int scoreOnDeath = 1250;
    public string bulletTag = "Bullet";


    void Awake()
    {
        gameControllerScript = GameObject.FindObjectOfType<GameControllerScript>();
        gameControllerScript.enemies.Add(this.gameObject);
    }

    private void FixedUpdate()
    {

        float cosMovement = Mathf.Cos(2 * Mathf.PI * Time.time / 12);
        transform.position = new Vector3((12.0f * cosMovement), transform.position.y, transform.position.z);
        if (cosMovement <= -0.95)
        {
            transform.eulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, 270.0f);
            transform.position = new Vector3(transform.position.x, Random.Range(-gameControllerScript.ScreenBoundariesY, gameControllerScript.ScreenBoundariesY));
        }
        if (cosMovement >= 0.95)
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
