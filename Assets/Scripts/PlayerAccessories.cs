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
		PlayerPrefs.SetString (gameObject.name + "selectedAccessory", "");
		PlayerPrefs.SetInt (gameObject.name + "selectedAccessoryIndex", value);
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
			PlayerPrefs.SetString (gameObject.name + "selectedAccessory", selectedAccessory);
            selected[0] = playerAccessories[value - 1];
        }
    }
    public void PlayerHatsSetActiveByID(int value)
    {
		PlayerPrefs.SetString (gameObject.name + "selectedHat", "");
		PlayerPrefs.SetInt (gameObject.name + "selectedHatIndex", value);
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
			PlayerPrefs.SetString (gameObject.name + "selectedHat", selectedHat);
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
		foreach (GameObject go in playerHats)
		{
			go.SetActive(go.name == PlayerPrefs.GetString(gameObject.name + "selectedHat"));
			if (go.activeSelf) 
			{
				go.GetComponent<SpriteRenderer> ().color = new Color (PlayerPrefs.GetFloat (gameObject.name + go.gameObject.name + "_r_"), PlayerPrefs.GetFloat (gameObject.name + go.gameObject.name + "_g_"), PlayerPrefs.GetFloat (gameObject.name + go.gameObject.name + "_b_"));
			}
		}
		foreach (GameObject go in playerAccessories)
		{
			go.SetActive(go.name == PlayerPrefs.GetString(gameObject.name + "selectedAccessory"));
			if (go.activeSelf) 
			{
				go.GetComponent<SpriteRenderer> ().color = new Color (PlayerPrefs.GetFloat (gameObject.name + go.gameObject.name + "_r_"), PlayerPrefs.GetFloat (gameObject.name + go.gameObject.name + "_g_"), PlayerPrefs.GetFloat (gameObject.name + go.gameObject.name + "_b_"));
			}
		}
	}
}
