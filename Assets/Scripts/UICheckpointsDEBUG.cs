using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UICheckpointsDEBUG : MonoBehaviour
{
    private GameControllerScript gameControllerScript;

    void Awake()
    { 
        gameControllerScript = FindObjectOfType<GameControllerScript>();
    }
    // Update is called once per frame
    void Update()
    {
        //print(gameControllerScript.GetCheckpoints());
        GetComponent<TMP_Text>().text = string.Format("--Checkpoints--\n{0}", gameControllerScript.GetCheckpoints());
        if (Input.GetKeyDown(KeyCode.KeypadPlus) || Input.GetKeyDown(KeyCode.F5) || Input.GetButton("MENU") && Input.GetButtonDown("BLUE0"))
        {
            gameControllerScript.AddNewCheckpoint();
        }
    }
}
