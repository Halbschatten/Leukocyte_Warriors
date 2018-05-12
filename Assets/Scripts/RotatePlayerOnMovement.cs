using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlayerOnMovement : MonoBehaviour
{
    private Transform trsfm;
    public string inputPlayerVertical = "VERTICAL0";
    public float rotationInEachUpdate = 0.01f;
    public float maxRotation = 0.2f;

    public void RotateOnMovement()
    {
        if (Input.GetAxis(inputPlayerVertical) > 0) // > 0 -> Up
        {
            RotateUp(-rotationInEachUpdate);
        }
        else if ((Input.GetAxis(inputPlayerVertical) < 0)) // < 0 -> Down
        {
            RotateDown(rotationInEachUpdate);
        }
        else // = 0 (-) -> Down | (+) -> Up
        {
            if (trsfm.rotation.z > 0) //(+)->Up
            {
                RotateDown(rotationInEachUpdate * 2);
                if (trsfm.rotation.z < 0)
                {
                    trsfm.rotation = new Quaternion(trsfm.rotation.x, trsfm.rotation.y, 0.0f, trsfm.rotation.w);
                }
            }
            else if (trsfm.rotation.z < 0) //(-) -> Down
            {
                RotateUp(-rotationInEachUpdate * 2);
                if (trsfm.rotation.z > 0)
                {
                    trsfm.rotation = new Quaternion(trsfm.rotation.x, trsfm.rotation.y, 0.0f, trsfm.rotation.w);
                }
            }
        }
    }
    void RotateUp(float rotation)
    {
        if (trsfm.rotation.z < maxRotation)
        {
            trsfm.rotation = new Quaternion(trsfm.rotation.x, trsfm.rotation.y, trsfm.rotation.z + rotationInEachUpdate, trsfm.rotation.w);
        }
    }
    void RotateDown(float rotation)
    {
        if (trsfm.rotation.z > -maxRotation)
        {
            trsfm.rotation = new Quaternion(trsfm.rotation.x, trsfm.rotation.y, trsfm.rotation.z - rotationInEachUpdate, trsfm.rotation.w);
        }
    }
    void Awake()
    {
        trsfm = GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RotateOnMovement();
    }
}
