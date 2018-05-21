using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToTheScene : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public bool movementEnabled = true;
    public bool noStop = false;
    public float speed = 5.0f;
    public float offset = 2.5f;
    private Vector3 screenBoundary;
    private GameControllerScript gameControllerScript;

    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        gameControllerScript = FindObjectOfType<GameControllerScript>();
        screenBoundary = new Vector3(gameControllerScript.ScreenBoundariesX, gameControllerScript.ScreenBoundariesX, transform.position.z);
    }

     void FixedUpdate()
    {
        if (noStop == true)
        {
            rb2d.velocity = new Vector3(-speed, 0.0f, 0.0f);
        }
        else
        {
            if (transform.position.x < screenBoundary.x - offset && transform.position.x > -screenBoundary.x + offset)
            {
                movementEnabled = false;
                rb2d.velocity = Vector3.zero;
            }
            if (movementEnabled == true)
            {
                if (transform.position.x > gameControllerScript.ScreenBoundariesX - offset)
                {
                    rb2d.velocity = new Vector3(-speed, 0.0f, 0.0f);
                }
                else if (transform.position.x < gameControllerScript.ScreenBoundariesX + offset)
                {
                    rb2d.velocity = new Vector3(speed, 0.0f, 0.0f);
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.gameObject.tag == "Checkpoint" && collision.gameObject.tag == "Player")
        {
            gameControllerScript.AddNewCheckpoint();
            Destroy(this.gameObject);
        }
        if (this.gameObject.tag == "Bacteria" && collision.gameObject.tag == "Player")
        {
            
        }
    }
}
