﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDebuffInvertedMovement : MonoBehaviour
{
    private GameObject gameControllerGameObject; //Reference to the Game Controller GameObject;
    private GameControllerScript gameControllerScript;
    private string gameControllerTag = "GameController"; //Game Controller's tag;
    private string playerTag = "Player";
    public float duration = 30.0f;

    void Awake()
    {
        gameControllerGameObject = GameObject.FindGameObjectWithTag(gameControllerTag);
        gameControllerScript = gameControllerGameObject.GetComponent<GameControllerScript>();
    }

    // Use this for initialization
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == playerTag)
        {
            for (int i = 0; i < gameControllerScript.Players.Length; i++)
            {
                gameControllerScript.Players[i].GetComponent<PlayerScript>().DebuffInvertedMovement(duration);
                //Debug.Log(string.Format("[Player {0}]: [{1}, {2:00}s] activated!", i + 1, "debuffInvertedMovement", duration));
            }
            GetComponent<CircleCollider2D>().enabled = false;
            StartCoroutine(WaitNSecondsAndDestroy(0.5f));
        }
    }

    IEnumerator WaitNSecondsAndDestroy(float n)
    {
        yield return new WaitForSeconds(n);
        Destroy(this.gameObject);
    }
}
