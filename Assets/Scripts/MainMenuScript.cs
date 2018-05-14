using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public float bgScrollSpeed;
    public GameObject mainMenu, player1Menu, player2Menu, tutorialMenu, settingsMenu, aboutMenu;

    void Update()
    {
        //If ESC key is pressed, quit application.
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
	public void MainMenuButtonPlay()
	{
        mainMenu.SetActive(false);
        player1Menu.SetActive(true);
		//SceneManager.LoadScene("DemoScene");
	}
	public void MainMenuButtonTutorial()
	{
        mainMenu.SetActive(false);
        tutorialMenu.SetActive(true);
    }
	public void MainMenuButtonSettings()
	{
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }
    public void MainMenuButtonAbout()
    {
        mainMenu.SetActive(false);
        aboutMenu.SetActive(true);
    }
    public void MainMenuButtonExit()
	{
		QuitGame();
	}
    //Play Menu
    public void Player1MenuBackButton()
    {
        player1Menu.SetActive(false);
        mainMenu.SetActive(true);
    }
    public void Player1MenuNextButton()
    {
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
        //Do magic here ;)
    }
    //
    public void TutorialMenuBackButton()
    {
        tutorialMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
    public void SettingsMenuBackButton()
    {
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
    public void AboutMenuBackButton()
    {
        aboutMenu.SetActive(false);
        mainMenu.SetActive(true);
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
