using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public float bgScrollSpeed;
    public GameObject mainMenu, player1Menu, player2Menu, tutorialMenu, settingsMenu, confirmationMenu, aboutMenu;
    public GameObject logo;
    public GameObject debugUIFPS;
    public string[] activeItems = new string[2], activeHats = new string[2];
    public PlayerAccessories player1Accessories;
    public PlayerAccessories player2Accessories;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadDivide))
        {
            if (debugUIFPS.activeSelf == true)
            {
                debugUIFPS.SetActive(false);
                PlayerPrefs.SetInt("gameDebugUI_FPS", 0);
            }
            else
            {
                debugUIFPS.SetActive(true);
                PlayerPrefs.SetInt("gameDebugUI_FPS", 1);
            }
        }
       
        //If ESC key is pressed, quit application.
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
	void Awake()
	{
        if (PlayerPrefs.GetInt("gameDebugUI_FPS") == 0)
        {
            debugUIFPS.SetActive(false);
        }
        else
        {
            debugUIFPS.SetActive(true);
        }
    }

	public void MainMenuButtonPlay()
	{
        mainMenu.SetActive(false);
        logo.SetActive(false);
        player1Menu.SetActive(true);
	}
	public void MainMenuButtonTutorial()
	{
        mainMenu.SetActive(false);
        logo.SetActive(false);
        tutorialMenu.SetActive(true);
    }
	public void MainMenuButtonSettings()
	{
        mainMenu.SetActive(false);
        logo.SetActive(false);
        settingsMenu.SetActive(true);
    }
    public void MainMenuButtonAbout()
    {
        mainMenu.SetActive(false);
        logo.SetActive(false);
        aboutMenu.SetActive(true);
    }
    public void MainMenuButtonExit()
	{
		QuitGame();
	}
    public void Player1MenuBackButton()
    {
        player1Menu.SetActive(false);
        mainMenu.SetActive(true);
        logo.SetActive(true);
    }
    public void Player1MenuNextButton()
    {
        activeHats[0] = player1Accessories.GetSelectedHat();
        activeItems[0] = player1Accessories.GetSelectedAccessory();
        //print(activeHats[0]);
        //print(activeItems[0]);
        player1Menu.SetActive(false);
        player2Menu.SetActive(true);
    }
    public void Player2MenuBackButton()
    {
        player2Menu.SetActive(false);
        player1Menu.SetActive(true);
    }
    public void Player2MenuPlayButton()
    {
        activeHats[1] = player2Accessories.GetSelectedHat();
        activeItems[1] = player2Accessories.GetSelectedAccessory();
        //print(activeHats[1]);
        //print(activeItems[1]);
        SceneManager.LoadScene("DemoScene");
    }
    public void TutorialMenuBackButton()
    {
        tutorialMenu.SetActive(false);
        mainMenu.SetActive(true);
        logo.SetActive(true);
    }
    public void SettingsMenuBackButton()
    {
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
        logo.SetActive(true);
    }
    public void SettingsMenuEraseAllDataButton()
    {
        confirmationMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }
    public void ConfirmationMenuYesButton()
    {
        PlayerPrefs.DeleteAll();
        confirmationMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }
    public void ConfirmationMenuBackButton()
    {
        confirmationMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }
    public void AboutMenuBackButton()
    {
        aboutMenu.SetActive(false);
        mainMenu.SetActive(true);
        logo.SetActive(true);
    }
    public void QuitGame()
	{
     // save any game data here
     #if UNITY_EDITOR
         // Application.Quit() does not work in the editor so
         // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
         UnityEditor.EditorApplication.isPlaying = false;
     #else
         Application.Quit();
     #endif
	}
}
