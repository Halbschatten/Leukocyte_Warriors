using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public float bgScrollSpeed;
    public GameObject mainMenu, playMenu, tutorialMenu, settingsMenu, aboutMenu;

    //Play Menu
    public Image[] colorSliderBackgrounds = new Image[3];
    public Color color = Color.black;
    public Image colorDisplay;

    void Update()
    {
        colorDisplay.color = color;
        //If ESC key is pressed, quit application.
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
	public void MainMenuButtonPlay()
	{
        mainMenu.SetActive(false);
        playMenu.SetActive(true);
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
    public void PlayMenuBackButton()
    {
        playMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
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
