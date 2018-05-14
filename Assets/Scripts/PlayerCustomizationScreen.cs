using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCustomizationScreen : MonoBehaviour
{
    //Play Menu
    public Image[] colorSliderBackgrounds = new Image[3];
    public Color color = Color.black;
    public Image colorDisplay;

    public void OnRedSliderChange(float value)
    {
        color = new Color(value, color.g, color.b);
        colorSliderBackgrounds[0].color = new Vector4(value, 0.0f, 0.0f, 1.0f);
    }
    public void OnGreenSliderChange(float value)
    {
        color = new Color(color.r, value, color.b);
        colorSliderBackgrounds[1].color = new Vector4(0.0f, value, 0.0f, 1.0f);
    }
    public void OnBlueSliderChange(float value)
    {
        color = new Color(color.r, color.g, value);
        colorSliderBackgrounds[2].color = new Vector4(0.0f, 0.0f, value, 1.0f);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        colorDisplay.color = color;
    }
}
