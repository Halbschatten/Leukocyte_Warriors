﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObjectForeground : MonoBehaviour
{
    private GameObject gameControllerGameObject; //Reference to the Game Controller GameObject;
    private string gameControllerTag = "GameController"; //Game Controller's tag;
    private float fgScrollSpeed;
    private Rigidbody2D rb2d;

    void Awake()
    {
        gameControllerGameObject = GameObject.FindGameObjectWithTag(gameControllerTag);
        rb2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        //// If the game is over, stop scrolling.
        //if (gameControllerGameObject.GetComponent<GameControllerScript>().GameOver == true)
        //{
        //    rb2d.velocity = Vector2.zero;
        //}
        //else
        //{
        //    rb2d.velocity = new Vector2(gameControllerGameObject.GetComponent<GameControllerScript>().fgScrollSpeed, 0);
        //}
        rb2d.velocity = new Vector2(gameControllerGameObject.GetComponent<GameControllerScript>().fgScrollSpeed, 0);
    }
}