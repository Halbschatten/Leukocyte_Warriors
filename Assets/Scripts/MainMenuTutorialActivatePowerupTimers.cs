using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuTutorialActivatePowerupTimers : MonoBehaviour
{

    public MainMenuTutorialPowerupIcons[] mainMenuTutorialPowerupIcons;

    private void OnEnable()
    {
        foreach (MainMenuTutorialPowerupIcons icon in mainMenuTutorialPowerupIcons)
        {
            icon.enabled = true;
        }
    }
    private void OnDisable()
    {

        foreach (MainMenuTutorialPowerupIcons icon in mainMenuTutorialPowerupIcons)
        {
            icon.enabled = false;
        }
    }
}
