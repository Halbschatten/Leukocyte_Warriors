using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainMenuSelectedItem : MonoBehaviour
{
    MainMenuScript mainMenuScript;
    List<string> selection = new List<string>();
    TMP_Dropdown tmpDropdown;
    public Slider r, g, b;
    public Image displayColor, rFill, gFill, bFill;

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
            if (GameObject.Find(tmpDropdown.captionText.text))
            {
                if (GameObject.Find(tmpDropdown.captionText.text).GetComponent<SpriteRenderer>())
                {
                    Color color = GameObject.Find(tmpDropdown.captionText.text).GetComponent<SpriteRenderer>().color;
                    r.value = color.r;
                    g.value = color.g;
                    b.value = color.b;
                    rFill.color = new Color(color.r, 0.0f, 0.0f);
                    gFill.color = new Color(0.0f, color.g, 0.0f);
                    bFill.color = new Color(0.0f, 0.0f, color.b);
                    displayColor.color = color;
                }
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
