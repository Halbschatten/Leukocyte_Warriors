using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBossLife : MonoBehaviour
{
    private GameControllerScript gameControllerScript;
    private Slider hpBar;
    public Image fill, background;
    public Color fillColorHealthy, backgroundColorHealthy;
    public Color fillColorCaution, backgroundColorCaution;
    public Color fillColorDanger, backgroundColorDanger;
    public Color fillColorDead, backgroundColorDead;
    private float health;
    private float initialHealth;

    void Awake()
    {
        gameControllerScript = FindObjectOfType<GameControllerScript>();
        initialHealth = gameControllerScript.BossInitialHealth;
        hpBar = GetComponent<Slider>();

    }

    // Update is called once per frame
    void Update()
    {
        health = gameControllerScript.BossHealth;
        hpBar.value = health / (initialHealth / 10);
        if (health / initialHealth * 100 >= 75)
        {
            fill.color = fillColorHealthy;
            background.color = backgroundColorHealthy;
        }
        else
        {
            if (health / initialHealth * 100 >= 30 && health / initialHealth * 100 < 75)
            {
                fill.color = fillColorCaution;
                background.color = backgroundColorCaution;
            }
            else
            {
                if (health / initialHealth * 100 < 30 && health > 0.0f)
                {
                    fill.color = fillColorDanger;
                    background.color = backgroundColorDanger;
                }
                else
                {
                    fill.color = fillColorDead;
                    background.color = backgroundColorDead;
                }
            }
        }
    }
}
