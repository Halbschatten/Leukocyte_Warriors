using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILife : MonoBehaviour 
{
	private GameObject gameControllerGameObject; //Reference to the Game Controller GameObject;
	private string gameControllerTag = "GameController"; //Game Controller's tag;
    private Slider hpBar;
    public Image fill, background;
    public Color fillColorHealthy, backgroundColorHealthy;
    public Color fillColorCaution, backgroundColorCaution;
    public Color fillColorDanger, backgroundColorDanger;
    public Color fillColorDead, backgroundColorDead;
    public int playerNumber;
    private float health;

	void Awake()
	{
		gameControllerGameObject = GameObject.FindGameObjectWithTag (gameControllerTag);
        hpBar = GetComponent<Slider>();
	}

	// Update is called once per frame
	void Update () 
	{
        health = gameControllerGameObject.GetComponent<GameControllerScript>().PlayersHealth[playerNumber];
        hpBar.value = health;
        if (health / 10 * 100 >= 75)
        {
            fill.color = fillColorHealthy;
            background.color = backgroundColorHealthy;
        }
        else
        {
            if (health / 10 * 100 >= 30 && health / 10 * 100 < 75)
            {
                fill.color = fillColorCaution;
                background.color = backgroundColorCaution;
            }
            else
            {
                if (health / 10 * 100 < 30 && health != 0.0f)
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
