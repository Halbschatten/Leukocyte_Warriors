using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BacteriaBossScript : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private GameControllerScript gameControllerScript;
    private float life = 100.0f;
    private int scoreOnDeath = 50000;
    public string bulletTag = "Bullet";
    public Animator animator;

    private void Awake()
    {
        gameControllerScript = FindObjectOfType<GameControllerScript>();
        rb2d = GetComponent<Rigidbody2D>();
    }
    // Use this for initialization
    void Start ()
    {

	}

    void FixedUpdate()
    {

    }

    // Update is called once per frame
    void Update ()
    {
        gameControllerScript.BossHealth = life;
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == bulletTag)
        {
            this.life -= other.gameObject.GetComponent<BulletScript>().damage;
            Destroy(other.gameObject);
            animator.Play("Boss_Face_Angry");
            if (this.life <= 0.0f)
            {
                gameControllerScript.BossHealth = life;
                gameControllerScript.Score += scoreOnDeath;
                Destroy(this.gameObject);
            }
        }
    }
}
