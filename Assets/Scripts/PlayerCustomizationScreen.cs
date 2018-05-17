using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCustomizationScreen : MonoBehaviour
{
    //Play Menu
    public Image[] colorSliderBackgrounds = new Image[3];
    public Color color;
    public Image colorDisplay;
    public TMPro.TMP_Dropdown tMP_Dropdown;
    private GameObject selection;
	public TMPro.TMP_Dropdown hatDrop, accessoryDrop;

    public void OnRedSliderChange(float value)
    {
        selection = GameObject.Find(tMP_Dropdown.captionText.text);
        if (selection != null) {
			PlayerPrefs.SetFloat (gameObject.name + selection.gameObject.name + "_r_", 0.0f);
			color = new Color (value, color.g, color.b);
			colorSliderBackgrounds [0].color = new Vector4 (value, 0.0f, 0.0f, 1.0f);
			if (tMP_Dropdown.captionText.text != "None") {
				selection = GameObject.Find (tMP_Dropdown.captionText.text);
				selection.GetComponent<SpriteRenderer> ().color = color;
				PlayerPrefs.SetFloat (gameObject.name + selection.gameObject.name + "_r_", value);
			}
		}
    }
    public void OnGreenSliderChange(float value)
    {
		selection = GameObject.Find(tMP_Dropdown.captionText.text);
		if (selection != null) {
			PlayerPrefs.SetFloat (gameObject.name + selection.gameObject.name + "_g_", 0.0f);
			color = new Color (color.r, value, color.b);
			colorSliderBackgrounds [1].color = new Vector4 (0.0f, value, 0.0f, 1.0f);
			if (tMP_Dropdown.captionText.text != "None") {
				selection = GameObject.Find (tMP_Dropdown.captionText.text);
				selection.GetComponent<SpriteRenderer> ().color = color;
				PlayerPrefs.SetFloat (gameObject.name + selection.gameObject.name + "_g_", value);
			}
		}
    }
    public void OnBlueSliderChange(float value)
    {
		selection = GameObject.Find(tMP_Dropdown.captionText.text);
		if (selection != null) {
			PlayerPrefs.SetFloat (gameObject.name + selection.gameObject.name + "_b_", 0.0f);
			color = new Color (color.r, color.g, value);
			colorSliderBackgrounds [2].color = new Vector4 (0.0f, 0.0f, value, 1.0f);
			if (tMP_Dropdown.captionText.text != "None") {
				selection.GetComponent<SpriteRenderer> ().color = color;
				PlayerPrefs.SetFloat (gameObject.name + selection.gameObject.name + "_b_", value);
			}
		}
    }

    // Use this for initialization
    void Start ()
	{
		hatDrop.value = PlayerPrefs.GetInt (gameObject.name + "selectedHatIndex");
		accessoryDrop.value = PlayerPrefs.GetInt (gameObject.name + "selectedAccessoryIndex");
        color = new Color(PlayerPrefs.GetFloat(gameObject.name + PlayerPrefs.GetString(gameObject.name + "selectedAccessory") + "_r_"), PlayerPrefs.GetFloat(gameObject.name + PlayerPrefs.GetString(gameObject.name + "selectedAccessory") + "_g_"), PlayerPrefs.GetFloat(gameObject.name + PlayerPrefs.GetString(gameObject.name + "selectedAccessory") + "_b_"));
		colorSliderBackgrounds [0].transform.parent.parent.GetComponent<Slider> ().value = PlayerPrefs.GetFloat (gameObject.name + PlayerPrefs.GetString(gameObject.name + "selectedAccessory") + "_r_");
		colorSliderBackgrounds [1].transform.parent.parent.GetComponent<Slider> ().value = PlayerPrefs.GetFloat (gameObject.name + PlayerPrefs.GetString(gameObject.name + "selectedAccessory") + "_g_");
		colorSliderBackgrounds [2].transform.parent.parent.GetComponent<Slider> ().value = PlayerPrefs.GetFloat (gameObject.name + PlayerPrefs.GetString(gameObject.name + "selectedAccessory") + "_b_");
	}
	
	// Update is called once per frame
	void Update () {
        colorDisplay.color = color;
    }
}
