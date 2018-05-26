using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStageProgression : MonoBehaviour
{
    private GameObject boss;
    private Transform bossTrsfm;
    private Scrollbar scrllBar;
    public Slider bossHP;
    private float initialPosition;

    private void Awake()
    {
        boss = GameObject.FindGameObjectWithTag("Boss");
        initialPosition = boss.GetComponent<InitialPosition>().initialPosition;
        bossTrsfm = boss.GetComponent<Transform>();
        scrllBar = GetComponent<Scrollbar>();
    }

    void Update()
    {
        if (boss)
        {
            if (boss.GetComponent<MoveToTheScene>().movementEnabled == true)
            {
                scrllBar.value = bossTrsfm.position.x / initialPosition;
                scrllBar.gameObject.SetActive(true);
                bossHP.gameObject.SetActive(false);
            }
            else
            {
                scrllBar.value = 0.0f;
                scrllBar.gameObject.SetActive(false);
                bossHP.gameObject.SetActive(true);
            }
        }
        else
        {
            if (boss = GameObject.FindGameObjectWithTag("Boss"))
            {
                initialPosition = boss.GetComponent<InitialPosition>().initialPosition;
                bossTrsfm = boss.GetComponent<Transform>();
            }
            scrllBar.value = 1.0f;
            scrllBar.gameObject.SetActive(true);
            bossHP.gameObject.SetActive(false);
        }
    }
}