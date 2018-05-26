using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerForegroundCollisionTrigger : MonoBehaviour
{
    private GameControllerScript gameControllerScript;
    private string playerTag = "Player";

    void Awake ()
    {
        gameControllerScript = FindObjectOfType<GameControllerScript>();
	}
    
   void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == playerTag)
        {
            collision.GetComponent<PlayerScript>().DebuffSlowerMovement(30.0f, 0.5f);
            gameControllerScript.fgScrollSpeed = gameControllerScript.defaultFGScrollSpeed / 2;
            gameControllerScript.bgScrollSpeed = gameControllerScript.defaultBGScrollSpeed / 2;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == playerTag)
        {
            gameControllerScript.fgScrollSpeed = gameControllerScript.defaultFGScrollSpeed;
            gameControllerScript.bgScrollSpeed = gameControllerScript.defaultBGScrollSpeed;
        }
    }
}
