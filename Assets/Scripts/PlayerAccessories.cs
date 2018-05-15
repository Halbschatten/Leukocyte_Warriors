using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAccessories : MonoBehaviour
{
    public int player;
    public GameObject[] playerAccessories;
    public GameObject[] playerHats;
    private string selectedHat;
    private string selectedAccessory;

    public string GetSelectedHat()
    {
        return selectedHat;
    }
    public string GetSelectedAccessory()
    {
        return selectedAccessory;
    }

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
            selectedAccessory = playerAccessories[value - 1].name;
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
            selectedHat = playerHats[value - 1].name;
        }
    }
    // Use this for initialization
    void Start () 
    {	
	}
}
