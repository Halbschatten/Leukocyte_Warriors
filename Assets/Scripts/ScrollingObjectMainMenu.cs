using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObjectMainMenu : MonoBehaviour
{
    private GameObject mainCameraGameObject; //Reference to the Game Controller GameObject;
    private string mainCameraTag = "MainCamera"; //Game Controller's tag;
    private float bgScrollSpeed;
    private Rigidbody2D rb2d;

    void Awake()
    {
        mainCameraGameObject = GameObject.FindGameObjectWithTag(mainCameraTag);
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb2d.velocity = new Vector2(mainCameraGameObject.GetComponent<MainMenuScript>().bgScrollSpeed, 0);
    }
}