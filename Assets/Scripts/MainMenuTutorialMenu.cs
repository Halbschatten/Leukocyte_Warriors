using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuTutorialMenu : MonoBehaviour 
{
    public GameObject backButton, nextButton, finishButton, mainMenu, logo;
    public TMP_Text[] dialogMessages;
    private int messageNumberToDisplay = 0;
	// Use this for initialization

	private void Awake()
	{
        CheckButtons();
        SelectionCheck();
	}

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

    void CheckButtons()
    {
        if (messageNumberToDisplay == 0)
        {
            backButton.SetActive(false);
        }
        else
        {
            backButton.SetActive(true);
        }
        if (messageNumberToDisplay == dialogMessages.Length - 1)
        {
            nextButton.SetActive(false);
            finishButton.SetActive(true);
        }
        else
        {
            finishButton.SetActive(false);
            nextButton.SetActive(true);
        }
    }

    void SelectionCheck()
    {
        if (messageNumberToDisplay == 0)
        {
            if (nextButton.activeSelf)
            {
                nextButton.GetComponent<Button>().Select();
            }
        }
        else
        {
            if (messageNumberToDisplay == dialogMessages.Length - 1)
            {
                if (finishButton.activeSelf)
                {
                    finishButton.GetComponent<Button>().Select();
                }
                else
                {
                    if (nextButton.activeSelf)
                    {
                        nextButton.GetComponent<Button>().Select();
                    }
                }
            }
        }
    }

    public void ReturnToMainMenu()
    {
        messageNumberToDisplay = 0;
        DisplayMessage(dialogMessages[messageNumberToDisplay]);
        CheckButtons();
        SelectionCheck();
        transform.parent.gameObject.SetActive(false);
        logo.SetActive(true);
        mainMenu.SetActive(true);
    }

	public void Next()
    {
        if (messageNumberToDisplay < dialogMessages.Length - 1)
        {
            messageNumberToDisplay++;
            //print(messageNumberToDisplay);
            DisplayMessage(dialogMessages[messageNumberToDisplay]);
        }
        CheckButtons();
        SelectionCheck();
    }
    public void Back()
    {
        if (messageNumberToDisplay > 0)
        {
            messageNumberToDisplay--;
            //print(messageNumberToDisplay);
            DisplayMessage(dialogMessages[messageNumberToDisplay]);
        }
        CheckButtons();
        if (backButton.activeSelf)
        {
            if (messageNumberToDisplay >= 1)
            {
                backButton.GetComponent<Button>().Select();
            }
            else
            {
                nextButton.GetComponent<Button>().Select();
            }
        }
        else
        {
            if (nextButton.activeSelf)
            {
                nextButton.GetComponent<Button>().Select();
            }
        }
    }
}
