using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuTutorialPowerupIcons : MonoBehaviour
{
    public Image[] statusEffect = new Image[7];

    public float seconds = 1.0f;

    // Update is called once per frame
    void Update()
    {
        if (seconds <= 0.0f)
        {
            seconds = 1.0f;
        }
        else
        {
            seconds = seconds - Time.deltaTime;
        }
        for (int i = 0; i < statusEffect.Length; i++)
        {
            statusEffect[i].fillAmount = seconds;
        }
    }
}
