using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuTutorialEnableBackground : MonoBehaviour 
{
    public GameObject[] objectsToEnable;
	// Use this for initialization
	void OnEnable () 
    {
        foreach (GameObject go in objectsToEnable)
        {
            go.gameObject.SetActive(true);
        }
	}
	void OnDisable()
	{
        foreach (GameObject go in objectsToEnable)
        {
            go.gameObject.SetActive(false);
        }
	}
}
