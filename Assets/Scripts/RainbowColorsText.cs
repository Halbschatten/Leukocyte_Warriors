using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RainbowColorsText : MonoBehaviour {	
	public Vector4 randomColor;
	// Update is called once per frame
	void Update () 
	{
		randomColor = new Vector4 (Random.Range (0.1f, 1f), Random.Range (0.1f, 1f), Random.Range (0.1f, 1f), 1);
		ChangeColor();
	}
	void ChangeColor()
	{
		float r = 0.0f, g = 0.0f, b = 0.0f;
		for(r=0.0f; r <= 1.0f; r+=0.01f)
		{
		this.GetComponent<Text>().color = new Vector4(r, g, b, 1.0f);
		}
		r = 0.0f;
		for(g=0.0f; g <= 1.0f; g+=0.01f)
		{
		this.GetComponent<Text>().color = new Vector4(r, g, b, 1.0f);
		}
		g = 0.0f;
		for(b=0.0f; b <= 1.0f; b+=0.01f)
		{
		this.GetComponent<Text>().color = new Vector4(r, g, b, 1.0f);
		}
		b = 0.0f;
	}
}
