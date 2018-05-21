using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBuffAndDebuffs : MonoBehaviour
{
    public PlayerScript playerScript;
    public Image[] statusEffect = new Image[7];

	// Update is called once per frame
	void Update ()
    {
        if (playerScript != null)
        {
            for (int i = 0; i < playerScript.statusEffect.Length; i++)
            {
                statusEffect[i].enabled = playerScript.statusEffect[i];
                statusEffect[i].fillAmount = playerScript.statusEffectTime[i] / playerScript.originalTime[i];
            }
        }
        else
        {
            for (int i = 0; i < statusEffect.Length; i++)
            {
                statusEffect[i].enabled = false;
            }
        }
	}
}
