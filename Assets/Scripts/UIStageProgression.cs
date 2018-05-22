using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStageProgression : MonoBehaviour
{
    public GameObject boss;
    private Transform bossTrsfm;
    private Scrollbar scrllBar;
    public Slider bossHP;
    public float initialPosition;

    private void Awake()
    {
        scrllBar = GetComponent<Scrollbar>();
        bossTrsfm = boss.GetComponent<Transform>();
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
    }
}