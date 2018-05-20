using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private GameObject gameControllerGameObject; //Reference to the Game Controller GameObject;
    private string gameControllerTag = "GameController"; //Game Controller's tag;
    private float screenBoundariesX, screenBoundariesY; //Receives the GameController's boundaries on Awake().
    public string bacteriaTag = "Bacteria";
    private float bacteriaDamageOnTriggerEnter = 0.5f;
    private float bacteriaDamageOnTriggerStay = 0.01f;
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
    private float playerHorizontalSpeed = playerDefaultHorizontalSpeed; //Player horizontal movement speed.
    private float playerVerticalSpeed = playerDefaultVerticalSpeed; //Player vertical movement speed.
    public string inputPlayerHorizontal = "HORIZONTAL0"; //Defaults to Player 1. Change in Unity's Inspector to the desired player controls.
    public string inputPlayerVertical = "VERTICAL0"; //Defaults to Player 1. Change in Unity's Inspector to the desired player controls.
    public string inputPlayerShoot = "GREEN0"; //Defaults to Player 1. Change in Unity's Inspector to the desired player controls.
    private float nextFire;
    private float fireRate = 0.15f;
    public bool limitPlayerToScreenBounds = true;
    private float resizeOnShoot = 0.75f;
    private PlayerResizeOnShoot playerResizeOnShoot;

    private bool buffAutoShoot = false;
    private bool buffFasterMovement = false;
    private float buffFasterMovementMultiplier;
    private bool buffIgnoreConstantDamage = false;
    private bool buffShield = false;
    private float buffShieldMultiplier;
    private float buffAutoShootDuration = 0.0f;
    private float buffFasterMovementDuration = 0.0f;
    private float buffIgnoreConstantDamageDuration = 0.0f;
    private float buffShieldDuration = 0.0f;

    private bool debuffInvertedMovement = false;
    public bool GetDebuffInvertedMovement
    {
        get
        {
            return debuffInvertedMovement;
        }
    }
    private float debuffInvertedMovementDuration = 0.0f;
    private bool debuffSlowerMovement = false;
    private float debuffSlowerMovementDuration = 0.0f;
    private float debuffSlowerMovementMultiplier;

    public GameObject[] hats;
	public GameObject[] accessories;

    Vector2 MovementVelocity(string inputH, string inputV, float inputPlayerSpeedH, float inputPlayerSpeedV)
    {
        Vector2 velocity = rb2d.velocity;
        velocity = new Vector2(Input.GetAxis(inputH) * inputPlayerSpeedH, Input.GetAxis(inputV) * inputPlayerSpeedV) * Time.fixedDeltaTime;
        //Debug.Log (string.Format("{0}: {1} | {2}: {3}", inputH, velocity.x, inputV, velocity.y));
        return velocity;
    }
    public float BuffHealHP(float amount)
    {
        float amountHealed = 0.0f;
        if (life == defaultLife)
        {
            Debug.Log(string.Format("[Player {0}]: [{1}] activated, healing {2} points of health!", playerID + 1, "buffRestoreHealth", amountHealed));
            return amountHealed;
        }
        else
        {
            if (life + amount > defaultLife)
            {
                amountHealed = defaultLife - life;
                life = defaultLife;
                Debug.Log(string.Format("[Player {0}]: [{1}] activated, healing {2} points of health!", playerID + 1, "buffRestoreHealth", amountHealed));
                return amountHealed;
            }
            else
            {
                life = life + amount;
                amountHealed = amount;
                Debug.Log(string.Format("[Player {0}]: [{1}] activated, healing {2} points of health!", playerID + 1, "buffRestoreHealth", amountHealed));
                return amountHealed;
            }
        }
       
    }
    //Buffs
    public void BuffAutoShoot(float duration)
    {
        this.buffAutoShootDuration = duration;
        buffAutoShoot = true;
        Debug.Log(string.Format("[Player {0}]: [{1}, {2:00}s] activated!", playerID + 1, "buffAutoShoot", buffAutoShootDuration));
    }
    public void BuffFasterMovement(float duration, float multiplier)
    {
        this.buffFasterMovementDuration = duration;
        this.buffFasterMovementMultiplier = multiplier;
        buffFasterMovement = true;
        Debug.Log(string.Format("[Player {0}]: [{1}, {2:00}s, {3:#.##}x] activated!", playerID + 1, "buffFasterMovement", buffFasterMovementDuration, buffFasterMovementMultiplier));
    }
    public void BuffIgnoreConstantDamage(float duration)
    {
        this.buffIgnoreConstantDamageDuration = duration;
        buffIgnoreConstantDamage = true;
        Debug.Log(string.Format("[Player {0}]: [{1}, {2:00}s] activated!", playerID + 1, "buffIgnoreConstantDamage", buffIgnoreConstantDamageDuration));
    }
    public void BuffShield(float duration, float multiplier)
    {
        this.buffShieldDuration = duration;
        this.buffShieldMultiplier = multiplier;
        buffShield = true;
        Debug.Log(string.Format("[Player {0}]: [{1}, {2:00}s, {3:#.##}x] activated!", playerID + 1, "buffShield", buffShieldDuration, buffShieldMultiplier));
    }

    //Debuffs
    public void DebuffInvertedMovement(float duration)
    {
        this.debuffInvertedMovementDuration = duration;
        debuffInvertedMovement = true;
    }
    public void DebuffSlowerMovement(float duration, float multiplier)
    {
        this.debuffSlowerMovementDuration = duration;
        this.debuffSlowerMovementMultiplier = multiplier;
        debuffSlowerMovement = true;
        Debug.Log(string.Format("[Player {0}]: [{1}, {2:00}s, {3:#.##}x] activated!", playerID + 1, "debuffSlowerMovement", debuffSlowerMovementDuration, debuffSlowerMovementMultiplier));
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
		foreach (GameObject go in hats)
		{
			go.SetActive(go.name == PlayerPrefs.GetString(gameObject.name + "selectedHat"));
			if (go.activeSelf) 
			{
				go.GetComponent<SpriteRenderer> ().color = new Color (PlayerPrefs.GetFloat (gameObject.name + go.gameObject.name + "_r_"), PlayerPrefs.GetFloat (gameObject.name + go.gameObject.name + "_g_"), PlayerPrefs.GetFloat (gameObject.name + go.gameObject.name + "_b_"));
			}
		}
		foreach (GameObject go in accessories)
		{
			go.SetActive(go.name == PlayerPrefs.GetString(gameObject.name + "selectedAccessory"));
			if (go.activeSelf) 
			{
				go.GetComponent<SpriteRenderer> ().color = new Color (PlayerPrefs.GetFloat (gameObject.name + go.gameObject.name + "_r_"), PlayerPrefs.GetFloat (gameObject.name + go.gameObject.name + "_g_"), PlayerPrefs.GetFloat (gameObject.name + go.gameObject.name + "_b_"));
			}
		}
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
            if (debuffSlowerMovement == false)
            {
                playerHorizontalSpeed = playerDefaultHorizontalSpeed;
                playerVerticalSpeed = playerDefaultVerticalSpeed;
            }
        }
        if (debuffInvertedMovement == true)
        {
            rb2d.velocity = MovementVelocity(inputPlayerHorizontal, inputPlayerVertical, -playerHorizontalSpeed, -playerVerticalSpeed);
        }
        else
        {
            rb2d.velocity = MovementVelocity(inputPlayerHorizontal, inputPlayerVertical, playerHorizontalSpeed, playerVerticalSpeed);
        }
        if (debuffSlowerMovement == true)
        {
            playerHorizontalSpeed = playerDefaultHorizontalSpeed * debuffSlowerMovementMultiplier;
            playerVerticalSpeed = playerDefaultVerticalSpeed * debuffSlowerMovementMultiplier;
        }
        else
        {
            if (buffFasterMovement == false)
            {
                playerHorizontalSpeed = playerDefaultHorizontalSpeed;
                playerVerticalSpeed = playerDefaultVerticalSpeed;
            }
        }
        if (limitPlayerToScreenBounds == true)
        {
            trsfm.position = new Vector2(Mathf.Clamp(trsfm.position.x, -screenBoundariesX, screenBoundariesX), Mathf.Clamp(trsfm.position.y, -screenBoundariesY, screenBoundariesY));
        }
    }
    // Update is called once per frame
    void Update()
    {
        UpdateLifeInGameController();
        BuffDebuffTimers();
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

    void BuffDebuffTimers()
    {
        //Buffs

        //buffAutoShoot Timer
        if (buffAutoShoot == true)
        {
            if (buffAutoShootDuration < 0.0f)
            { 
                buffAutoShootDuration = 0.0f;
                buffAutoShoot = false;
                print(string.Format("[Player {0}]: [{1}] deactivated!", playerID + 1, "buffAutoShoot"));
            }
            else
            {
                buffAutoShootDuration = buffAutoShootDuration - Time.deltaTime;
            }
        }

        //buffFasterMovement Timer
        if (buffFasterMovement == true)
        {
            if (buffFasterMovementDuration < 0.0f)
            {
                buffFasterMovementDuration = 0.0f;
                buffFasterMovement = false;
                print(string.Format("[Player {0}]: [{1}] deactivated!", playerID + 1, "buffFasterMovement"));
            }
            else
            {
                buffFasterMovementDuration = buffFasterMovementDuration - Time.deltaTime;
            }
        }

        //buffIgnoreConstantDamage Timer
        if (buffIgnoreConstantDamage == true)
        {
            if (buffIgnoreConstantDamageDuration < 0.0f)
            {
                buffIgnoreConstantDamageDuration = 0.0f;
                buffIgnoreConstantDamage = false;
                print(string.Format("[Player {0}]: [{1}] deactivated!", playerID + 1, "buffIgnoreConstantDamage"));
            }
            else
            {
                buffIgnoreConstantDamageDuration = buffIgnoreConstantDamageDuration - Time.deltaTime;
            }
        }

        //buffShield Timer
        if (buffShield == true)
        {
            if (buffShieldDuration < 0.0f)
            {
                buffShieldDuration = 0.0f;
                buffShield = false;
                print(string.Format("[Player {0}]: [{1}] deactivated!", playerID + 1, "buffShield"));
            }
            else
            {
                buffShieldDuration = buffShieldDuration - Time.deltaTime;
            }
        }

        //Debuffs

        //debuffInvertedMovement Timer
        if (debuffInvertedMovement == true)
        {
            if (debuffInvertedMovementDuration < 0.0f)
            {
                debuffInvertedMovementDuration = 0.0f;
                debuffInvertedMovement = false;
                print(string.Format("[Player {0}]: [{1}] deactivated!", playerID + 1, "debuffInvertedMovement"));
            }
            else
            {
                debuffInvertedMovementDuration = debuffInvertedMovementDuration - Time.deltaTime;
            }
        }

        //debuffSlowerMovement Timer
        if (debuffSlowerMovement == true)
        {
            if (debuffSlowerMovementDuration < 0.0f)
            {
                debuffSlowerMovementDuration = 0.0f;
                debuffSlowerMovement = false;
                print(string.Format("[Player {0}]: [{1}] deactivated!", playerID + 1, "debuffSlowerMovement"));
            }
            else
            {
                debuffSlowerMovementDuration = debuffSlowerMovementDuration - Time.deltaTime;
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == bacteriaTag)
        {
            if (buffShield == true)
            {
                this.life -= bacteriaDamageOnTriggerEnter * buffShieldMultiplier;
            }
            else
            {
                this.life -= bacteriaDamageOnTriggerEnter;
            }
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
            if (buffShield == true)
            {
                this.life -= bacteriaDamageOnTriggerStay * buffShieldMultiplier;
            }
            else
            {
                this.life -= bacteriaDamageOnTriggerStay;
            }
            if (this.life <= 0.0f)
            {
				life = 0.0f;
				UpdateLifeInGameController();
                Destroy(this.gameObject);
            }
        }
    }
}
