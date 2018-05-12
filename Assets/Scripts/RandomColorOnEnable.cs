using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColorOnEnable : MonoBehaviour
{
    public Color randomColor;
    private SpriteRenderer spriteRenderer;

	private void Awake()
	{
        spriteRenderer = GetComponent<SpriteRenderer>();
	}
    private void OnEnable()
    {
        randomColor = new Vector4(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 1.0f);
        spriteRenderer.color = randomColor;
    }
}
