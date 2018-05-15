using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAccessories : MonoBehaviour
{
    public int player;
    public GameObject[] playerAccessories;
    public GameObject[] playerHats;
    private GameObject[] selected = new GameObject[2];
    private string selectedHat;
    private string selectedAccessory;
    private MainMenuSelectedItem mainMenuSelectedItem;

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
            selected[0] = null;
        }
        else
        {
            for (int i = 0; i < playerAccessories.Length; i++)
            {
                playerAccessories[i].SetActive(false);
            }
            playerAccessories[value - 1].SetActive(true);
            selectedAccessory = playerAccessories[value - 1].name;
            selected[0] = playerAccessories[value - 1];
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
            selected[1] = null;
        }
        else
        {
            for (int i = 0; i < playerHats.Length; i++)
            {
                playerHats[i].SetActive(false);
            }
            playerHats[value - 1].SetActive(true);
            selectedHat = playerHats[value - 1].name;
            selected[1] = playerHats[value - 1];
        }
    }
    private void Update()
    {
        mainMenuSelectedItem.UpdateListSelection(selected[0], selected[1]);
    }
    // Use this for initialization
    void Start () 
    {
        mainMenuSelectedItem = FindObjectOfType<MainMenuSelectedItem>();
	}
}
