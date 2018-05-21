using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIFPSCounter : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        GetComponent<TMP_Text>().text = string.Format("FPS: {0:00.00}\nFrametime: {1:00.00} ms\nPhysics Time: {2:00.00} ms\nExecution Time: {3:00.00} s", 1.0f / Time.deltaTime, Time.deltaTime * 1000f, Time.fixedDeltaTime * 1000f, Time.time);
    }
}
