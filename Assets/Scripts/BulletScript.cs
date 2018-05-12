using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour 
{
	private Rigidbody2D rb2d;
	public float bulletVelocity = 10.0f;

	void Awake()
	{
		rb2d = GetComponent<Rigidbody2D> ();
	}

	// Use this for initialization
	void Start () 
	{
		rb2d.velocity = new Vector2(bulletVelocity, 0.0f);
	}
	void OnBecameInvisible()
	{
		Destroy (this.gameObject);
	}
}
