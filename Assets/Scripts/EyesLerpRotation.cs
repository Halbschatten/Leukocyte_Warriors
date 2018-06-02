using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyesLerpRotation : MonoBehaviour 
{
    float rotation;
    float time = 1.0f;
    public float minAngle = -10.0f;
    public float maxAngle = 90.0f;
    public bool flip = false;
    // Update is called once per frame
    void Update()
    {
        if (flip == true)
        {
            rotation = Mathf.LerpAngle(minAngle, maxAngle, Mathf.PingPong(Time.time, time));
        }
        else
        {
            rotation = -Mathf.LerpAngle(minAngle, maxAngle, Mathf.PingPong(Time.time, time));
        }
        this.transform.eulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, rotation + transform.parent.transform.eulerAngles.z); 
	}
}
