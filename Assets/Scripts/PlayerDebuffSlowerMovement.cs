using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDebuffSlowerMovement : MonoBehaviour
{
    private GameObject gameControllerGameObject; //Reference to the Game Controller GameObject;
    private GameControllerScript gameControllerScript;
    private string gameControllerTag = "GameController"; //Game Controller's tag;
    private string playerTag = "Player";
    private Rigidbody2D rb2d;
    public float initialVelocityX = -1.5f;
    public float multiplier = 0.5f;
    public float duration = 30.0f;

    void Awake()
    {
        gameControllerGameObject = GameObject.FindGameObjectWithTag(gameControllerTag);
        gameControllerScript = gameControllerGameObject.GetComponent<GameControllerScript>();
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector2(initialVelocityX, rb2d.velocity.y);
    }

    // Use this for initialization
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == playerTag)
        {
            for (int i = 0; i < gameControllerScript.Players.Length; i++)
            {
                gameControllerScript.Players[i].GetComponent<PlayerScript>().DebuffSlowerMovement(duration, multiplier);
                //Debug.Log(string.Format("[Player {0}]: [{1}, {2:00}s, {3:#.##}x] activated!", i + 1, "debuffSlowerMovement", duration, multiplier));
            }
            GetComponent<CircleCollider2D>().enabled = false;
            StartCoroutine(WaitNSecondsAndDestroy(0.5f));
            rb2d.velocity = Vector2.zero;
        }
    }

    IEnumerator WaitNSecondsAndDestroy(float n)
    {
        yield return new WaitForSeconds(n);
        Destroy(this.gameObject);
    }
    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
