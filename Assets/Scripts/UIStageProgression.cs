using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStageProgression : MonoBehaviour
{
    public GameObject boss;
    private Transform bossTrsfm;
    private Scrollbar scrllBar;
    public float initialPosition;

    private void Awake()
    {
        scrllBar = GetComponent<Scrollbar>();
        bossTrsfm = boss.GetComponent<Transform>();
    }

    void FixedUpdate ()
    {
        if (boss.GetComponent<MoveToTheScene>().movementEnabled == true)
        {
            scrllBar.value = bossTrsfm.position.x / initialPosition;
        }
        else
        {
            scrllBar.value = 0.0f;
        }
	}
}