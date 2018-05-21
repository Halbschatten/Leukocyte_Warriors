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
        GetComponent<TMP_Text>().text = string.Format("--Checkpoints--\n**Press KeypadPlus to Add New**\n{0} | score\n{1}", "No." ,gameControllerScript.GetCheckpoints());
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            gameControllerScript.AddNewCheckpoint();
        }
    }
}
