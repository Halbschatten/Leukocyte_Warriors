using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenuTutorialMenu : MonoBehaviour 
{
    public TMP_Text[] dialogMessages;
    private int messageNumberToDisplay = 0;
	// Use this for initialization

    void DisplayMessage(TMP_Text message)
    {
        for (int i = 0; i < dialogMessages.Length; i++)
        {
            if (dialogMessages[i].GetInstanceID() != message.GetInstanceID())
            {
                dialogMessages[i].gameObject.SetActive(false);
            }
            else
            {
                dialogMessages[i].gameObject.SetActive(true);
            }
        }
    }

    public void Next()
    {
        if (messageNumberToDisplay < dialogMessages.Length - 1)
        {
            messageNumberToDisplay++;
            //print(messageNumberToDisplay);
            DisplayMessage(dialogMessages[messageNumberToDisplay]);
        }
    }
    public void Back()
    {
        if (messageNumberToDisplay > 0)
        {
            messageNumberToDisplay--;
            //print(messageNumberToDisplay);
            DisplayMessage(dialogMessages[messageNumberToDisplay]);
        }
    }
}
