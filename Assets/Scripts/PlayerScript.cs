using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private GameObject gameControllerGameObject; //Reference to the Game Controller GameObject;
    private string gameControllerTag = "GameController"; //Game Controller's tag;
    private float screenBoundariesX, screenBoundariesY; //Receives the GameController's boundaries on Awake().
    public string bacteriaTag = "Bacteria";
    public float bacteriaDamageOnTriggerEnter = 0.5f;
    public float bacteriaDamageOnTriggerStay = 0.01f;
    private Rigidbody2D rb2d; //Reference to the player's Rigidbody2D;
    private Transform trsfm; //Reference to the player's Transform;
    public int playerID = 0;
    private static float defaultLife = 10.0f; //Player Life;
    private float life = defaultLife;
    public float Life
    {
        get
        {
            return life;
        }
    }
    public GameObject projectile; //Reference to the projectile GameObject to be instantiated;
    public GameObject playerCannon; //Reference to the player's cannon. This is the position that the projectile will be instantiated;
    private Transform cannonTransform; //Reference to the player cannon's transform.
    private static float playerDefaultHorizontalSpeed = 200.0f; //Player horizontal movement speed.
    private static float playerDefaultVerticalSpeed = 200.0f; //Player vertical movement speed.
    public float playerHorizontalSpeed = playerDefaultHorizontalSpeed; //Player horizontal movement speed.
    public float playerVerticalSpeed = playerDefaultVerticalSpeed; //Player vertical movement speed.
    public string inputPlayerHorizontal = "HORIZONTAL0"; //Defaults to Player 1. Change in Unity's Inspector to the desired player controls.
    public string inputPlayerVertical = "VERTICAL0"; //Defaults to Player 1. Change in Unity's Inspector to the desired player controls.
    public string inputPlayerShoot = "GREEN0"; //Defaults to Player 1. Change in Unity's Inspector to the desired player controls.
    private float nextFire;
    public float fireRate = 0.15f;
    public bool limitPlayerToScreenBounds = true;
    private float resizeOnShoot = 0.75f;
    private PlayerResizeOnShoot playerResizeOnShoot;

    public bool buffAutoShoot = false;
    public bool buffFasterMovement = false;
    public float buffFasterMovementMultiplier = 1.5f;
    public bool buffIgnoreConstantDamage = false;


    Vector2 MovementVelocity(string inputH, string inputV, float inputPlayerSpeedH, float inputPlayerSpeedV)
    {
        Vector2 velocity = rb2d.velocity;
        velocity = new Vector2(Input.GetAxis(inputH) * inputPlayerSpeedH, Input.GetAxis(inputV) * inputPlayerSpeedV) * Time.fixedDeltaTime;
        //Debug.Log (string.Format("{0}: {1} | {2}: {3}", inputH, velocity.x, inputV, velocity.y));
        return velocity;
    }
    void Shoot(GameObject projectile)
    {
        playerResizeOnShoot.Shrink(resizeOnShoot);
        Instantiate(projectile, cannonTransform.position, cannonTransform.rotation);
    }

    void UpdateLifeInGameController()
    {
        gameControllerGameObject.GetComponent<GameControllerScript>().PlayersHealth[playerID] = life;
    }
    void Awake()
    {
        gameControllerGameObject = GameObject.FindGameObjectWithTag(gameControllerTag);
        screenBoundariesX = gameControllerGameObject.GetComponent<GameControllerScript>().ScreenBoundariesX; //Define local screen boundaries variables according to the GameController variables.
        screenBoundariesY = gameControllerGameObject.GetComponent<GameControllerScript>().ScreenBoundariesY; //Define local screen boundaries variables according to the GameController variables.
        cannonTransform = playerCannon.GetComponent<Transform>();
        rb2d = GetComponent<Rigidbody2D>();
        trsfm = GetComponent<Transform>();
        playerResizeOnShoot = GetComponent<PlayerResizeOnShoot>();
    }

    void FixedUpdate()
    {
        if (buffFasterMovement == true)
        {
            playerHorizontalSpeed = playerDefaultHorizontalSpeed * buffFasterMovementMultiplier;
            playerVerticalSpeed = playerDefaultVerticalSpeed * buffFasterMovementMultiplier;
        }
        else
        {
            playerHorizontalSpeed = playerDefaultHorizontalSpeed;
            playerVerticalSpeed = playerDefaultVerticalSpeed;
        }
        rb2d.velocity = MovementVelocity(inputPlayerHorizontal, inputPlayerVertical, playerHorizontalSpeed, playerVerticalSpeed);
        if (limitPlayerToScreenBounds == true)
        {
            trsfm.position = new Vector2(Mathf.Clamp(trsfm.position.x, -screenBoundariesX, screenBoundariesX), Mathf.Clamp(trsfm.position.y, -screenBoundariesY, screenBoundariesY));
        }
    }
    // Update is called once per frame
    void Update()
    {
        UpdateLifeInGameController();
        if (buffAutoShoot == true)
        {
            if (Input.GetButton(inputPlayerShoot) && Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                Shoot(projectile);
            }
        }
        else
        {
            if (Input.GetButtonDown(inputPlayerShoot))
            {
                Shoot(projectile);
            }
        }

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == bacteriaTag)
        {
            this.life -= bacteriaDamageOnTriggerEnter;
            if (this.life <= 0.0f)
            {
				life = 0.0f;
				UpdateLifeInGameController();
                Destroy(this.gameObject);
            }
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == bacteriaTag && buffIgnoreConstantDamage == false)
        {
            this.life -= bacteriaDamageOnTriggerStay;
            if (this.life <= 0.0f)
            {
				life = 0.0f;
				UpdateLifeInGameController();
                Destroy(this.gameObject);
            }
        }
    }
}
