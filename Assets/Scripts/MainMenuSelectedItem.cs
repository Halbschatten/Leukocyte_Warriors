using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenuSelectedItem : MonoBehaviour
{
    MainMenuScript mainMenuScript;
    List<string> selection = new List<string>();
    TMP_Dropdown tmpDropdown;

    public void SetSelection(List<string> input)
    {
        this.selection = input;
    }
    public List<string> GetSelection()
    {
        return selection;
    }

    public void UpdateListSelection(GameObject hat, GameObject accessory)
    {
        selection.Clear();
        if (hat == null && accessory == null)
        {
            selection.Add("None");
        }
        else
        {
            if (hat != null)
            {
                selection.Add(hat.name);
            }
            if (accessory != null)
            {
                selection.Add(accessory.name);
            }
        }
        tmpDropdown.ClearOptions();
        tmpDropdown.AddOptions(selection);
    }

    // Use this for initialization
    void Start ()
    {
        mainMenuScript = FindObjectOfType<MainMenuScript>();
        tmpDropdown = GetComponent<TMP_Dropdown>();
    }
	
	// Update is called once per frame
	void Update ()
    {
       
	}
}
