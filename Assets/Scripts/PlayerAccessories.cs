using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAccessories : MonoBehaviour
{
    public GameObject player;
    public GameObject[] playerAccessories;
    public GameObject[] playerHats;
    public GameObject selectedObject;

    public void PlayerAccessoriesSetActiveByID(int value)
    {
        if (value == 0)
        {
            for (int i = 0; i < playerAccessories.Length; i++)
            {
                playerAccessories[i].SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i < playerAccessories.Length; i++)
            {
                playerAccessories[i].SetActive(false);
            }
            playerAccessories[value - 1].SetActive(true);
        }
    }
    public void PlayerHatsSetActiveByID(int value)
    {
        if (value == 0)
        {
            for (int i = 0; i < playerHats.Length; i++)
            {
                playerHats[i].SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i < playerHats.Length; i++)
            {
                playerHats[i].SetActive(false);
            }
            playerHats[value - 1].SetActive(true);
        }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
}
