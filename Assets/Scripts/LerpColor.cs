using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LerpColor : MonoBehaviour
{
    public Color colorA, colorB;
    public float time;
    public Color lerpedColor;
	// Update is called once per frame
	void Update () 
	{
        lerpedColor = Color.Lerp(colorA, colorB, Mathf.PingPong(Time.time, time));
        GetComponent<SpriteRenderer>().color = lerpedColor;
	}
}
