using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResizeOnShoot : MonoBehaviour
{
    private Transform trsfm;
    public float growthInEachUpdate = 0.0009f;
    private Vector3 originalScale;

    private void Grow()
    {
        if (trsfm.localScale != originalScale)
        {
            trsfm.localScale = new Vector3(trsfm.localScale.x + growthInEachUpdate, trsfm.localScale.y + growthInEachUpdate, trsfm.localScale.z);
            if (trsfm.localScale.x >= originalScale.x && trsfm.localScale.y >= originalScale.y)
            {
                trsfm.localScale = originalScale;
            }
        }
    }

    public void Shrink(float modifiedScale)
    {
        trsfm.localScale = new Vector3(originalScale.x * modifiedScale, originalScale.y * modifiedScale, originalScale.z);
    }

    void Awake()
    {
        trsfm = GetComponent<Transform>();
        originalScale = trsfm.localScale;
    }

	// Update is called once per frame
	void FixedUpdate ()
    {
        Grow();
	}
}
