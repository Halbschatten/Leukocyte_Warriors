﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BacteriaBossScript : MonoBehaviour
{
    bool isQuitting;
    private GameControllerScript gameControllerScript;
    public SpawnerScript spawnerScript;
    private static float defaultLife = 100.0f;
    private float life = defaultLife;
    private int scoreOnDeath = 50000;
    public string bulletTag = "Bullet";
    public Animator animator;
    public GameObject[] gameObjectsToEnableOnDestroy;

    private void Awake()
    {
        gameControllerScript = FindObjectOfType<GameControllerScript>();
        gameControllerScript.bossPosition = GetComponent<Transform>();
        gameControllerScript.BossInitialHealth = defaultLife;
    }

    // Update is called once per frame
    void Update ()
    {
        gameControllerScript.BossHealth = life;
        if (gameControllerScript.GameOver == true)
        {
            life = defaultLife;
        }
        if (GetComponent<MoveToTheScene>().movementEnabled == false)
        {
            spawnerScript.enabled = true;
        }
        else
        {
            spawnerScript.enabled = false;
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
            foreach (GameObject gameObject in gameObjectsToEnableOnDestroy)
            {
                gameObject.SetActive(true);
            }
            gameControllerScript.AddNewCheckpoint();
            gameControllerScript.uiStageProgression.SetActive(true);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == bulletTag)
        {
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == bulletTag && GetComponent<MoveToTheScene>().movementEnabled == false)
        {
            this.life -= other.gameObject.GetComponent<BulletScript>().damage * 0.75f;
            animator.Play("Boss_Face_Angry");
            if (this.life <= 0.0f)
            {
                gameControllerScript.BossHealth = life;
                gameControllerScript.Score += scoreOnDeath;
                Destroy(this.gameObject);
            }
        }
    }
}
