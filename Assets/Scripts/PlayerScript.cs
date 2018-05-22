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
    private Vector3 originalPosition;
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
    private static float defaultBulletDamage = 1.0f;
    private float bulletDamage = defaultBulletDamage;
    public bool limitPlayerToScreenBounds = true;
    private float resizeOnShoot = 0.75f;
    private PlayerResizeOnShoot playerResizeOnShoot;

    private bool buffAutoShoot = false;
    private bool buffFasterMovement = false;
    private float buffFasterMovementMultiplier;
    private bool buffIgnoreConstantDamage = false;
    private bool buffShield = false;
    private float buffShieldMultiplier;
    private bool buffStrongerAttacks = false;
    private float buffStrongerAttacksMultiplier;
    private float buffAutoShootDuration = 0.0f;
    private float buffFasterMovementDuration = 0.0f;
    private float buffIgnoreConstantDamageDuration = 0.0f;
    private float buffShieldDuration = 0.0f;
    private float buffStrongerAttacksDuration = 0.0f;
    private bool buffEnemyLoseHPOverTime = false;
    public bool GetBuffEnemyLoseHPOverTime
    {
        get
        {
            return buffEnemyLoseHPOverTime;
        }
    }
    private float buffEnemyLoseHPOverTimeDuration;
    private float buffEnemyLoseHPOverTimeMultiplier;

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

    public bool[] statusEffect = new bool[7];
    public float[] statusEffectTime = new float[7];
    public float[] originalTime = new float[7];

    private List<GameObject> enemies = new List<GameObject>();

    public GameObject[] hats;
	public GameObject[] accessories;

    public void Respawn()
    {
        trsfm.position = originalPosition;
        life = defaultLife;
        GetOutfitFromPlayerPrefs();
        buffAutoShoot = false;
        buffFasterMovement = false;
        buffIgnoreConstantDamage = false;
        buffShield = false;
        buffStrongerAttacks = false;
        debuffInvertedMovement = false;
        debuffSlowerMovement = false;
        this.gameObject.SetActive(true);
    }

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
            //Debug.Log(string.Format("[Player {0}]: [{1}] activated, healing {2} points of health!", playerID + 1, "buffRestoreHealth", amountHealed));
            return amountHealed;
        }
        else
        {
            if (life + amount > defaultLife)
            {
                amountHealed = defaultLife - life;
                life = defaultLife;
                //Debug.Log(string.Format("[Player {0}]: [{1}] activated, healing {2} points of health!", playerID + 1, "buffRestoreHealth", amountHealed));
                return amountHealed;
            }
            else
            {
                life = life + amount;
                amountHealed = amount;
                //Debug.Log(string.Format("[Player {0}]: [{1}] activated, healing {2} points of health!", playerID + 1, "buffRestoreHealth", amountHealed));
                return amountHealed;
            }
        }
       
    }
    //Buffs
    public void BuffAutoShoot(float duration)
    {
        this.buffAutoShootDuration = duration;
        this.originalTime[0] = duration;
        buffAutoShoot = true;
        //Debug.Log(string.Format("[Player {0}]: [{1}, {2:00}s] activated!", playerID + 1, "buffAutoShoot", buffAutoShootDuration));
    }
    public void BuffFasterMovement(float duration, float multiplier)
    {
        this.buffFasterMovementDuration = duration;
        this.buffFasterMovementMultiplier = multiplier;
        this.originalTime[2] = duration;
        buffFasterMovement = true;
        //Debug.Log(string.Format("[Player {0}]: [{1}, {2:00}s, {3:#.##}x] activated!", playerID + 1, "buffFasterMovement", buffFasterMovementDuration, buffFasterMovementMultiplier));
    }
    public void BuffIgnoreConstantDamage(float duration)
    {
        this.buffIgnoreConstantDamageDuration = duration;
        //this.originalTime[1] = duration; [?]
        buffIgnoreConstantDamage = true;
        //Debug.Log(string.Format("[Player {0}]: [{1}, {2:00}s] activated!", playerID + 1, "buffIgnoreConstantDamage", buffIgnoreConstantDamageDuration));
    }
    public void BuffShield(float duration, float multiplier)
    {
        this.buffShieldDuration = duration;
        this.buffShieldMultiplier = multiplier;
        this.originalTime[3] = duration;
        buffShield = true;
        //Debug.Log(string.Format("[Player {0}]: [{1}, {2:00}s, {3:#.##}x] activated!", playerID + 1, "buffShield", buffShieldDuration, buffShieldMultiplier));
    }
    public void BuffStrongerAttacks(float duration, float multiplier)
    {
        this.buffStrongerAttacksDuration = duration;
        this.buffStrongerAttacksMultiplier = multiplier;
        this.originalTime[4] = duration;
        this.buffStrongerAttacks = true;
        //Debug.Log(string.Format("[Player {0}]: [{1}, {2:00}s, {3:#.##}x] activated!", playerID + 1, "buffStrongerAttacks", buffStrongerAttacksDuration, buffStrongerAttacksMultiplier));
    }

    public void BuffEnemyLoseHPOverTime(float duration, float multiplier)
    {
        this.buffEnemyLoseHPOverTimeDuration = duration;
        this.buffEnemyLoseHPOverTimeMultiplier = multiplier;
        this.originalTime[1] = duration;
        this.buffEnemyLoseHPOverTime = true;
        //Debug.Log(string.Format("[Player {0}]: [{1}, {2:00}s, {3:#.##}x] activated!", playerID + 1, "buffEnemyLoseHPOverTime", buffEnemyLoseHPOverTimeDuration, buffEnemyLoseHPOverTimeMultiplier));
    }

    //Debuffs
    public void DebuffInvertedMovement(float duration)
    {
        this.debuffInvertedMovementDuration = duration;
        this.originalTime[5] = duration;
        debuffInvertedMovement = true;
    }
    public void DebuffSlowerMovement(float duration, float multiplier)
    {
        this.debuffSlowerMovementDuration = duration;
        this.debuffSlowerMovementMultiplier = multiplier;
        this.originalTime[6] = duration;
        debuffSlowerMovement = true;
        //Debug.Log(string.Format("[Player {0}]: [{1}, {2:00}s, {3:#.##}x] activated!", playerID + 1, "debuffSlowerMovement", debuffSlowerMovementDuration, debuffSlowerMovementMultiplier));
    }

    void Shoot(GameObject projectile)
    {
        playerResizeOnShoot.Shrink(resizeOnShoot);
        Instantiate(projectile, cannonTransform.position, cannonTransform.rotation).gameObject.GetComponent<BulletScript>().damage = bulletDamage;
    }

    void UpdateLifeInGameController()
    {
        gameControllerGameObject.GetComponent<GameControllerScript>().PlayersHealth[playerID] = life;
    }

    void GetOutfitFromPlayerPrefs()
    {
        foreach (GameObject go in hats)
        {
            go.SetActive(go.name == PlayerPrefs.GetString(gameObject.name + "selectedHat"));
            if (go.activeSelf)
            {
                go.GetComponent<SpriteRenderer>().color = new Color(PlayerPrefs.GetFloat(gameObject.name + go.gameObject.name + "_r_"), PlayerPrefs.GetFloat(gameObject.name + go.gameObject.name + "_g_"), PlayerPrefs.GetFloat(gameObject.name + go.gameObject.name + "_b_"));
            }
        }
        foreach (GameObject go in accessories)
        {
            go.SetActive(go.name == PlayerPrefs.GetString(gameObject.name + "selectedAccessory"));
            if (go.activeSelf)
            {
                go.GetComponent<SpriteRenderer>().color = new Color(PlayerPrefs.GetFloat(gameObject.name + go.gameObject.name + "_r_"), PlayerPrefs.GetFloat(gameObject.name + go.gameObject.name + "_g_"), PlayerPrefs.GetFloat(gameObject.name + go.gameObject.name + "_b_"));
            }
        }
    }

    void Awake()
    {
        gameControllerGameObject = GameObject.FindGameObjectWithTag(gameControllerTag);
        screenBoundariesX = gameControllerGameObject.GetComponent<GameControllerScript>().ScreenBoundariesX; //Define local screen boundaries variables according to the GameController variables.
        screenBoundariesY = gameControllerGameObject.GetComponent<GameControllerScript>().ScreenBoundariesY; //Define local screen boundaries variables according to the GameController variables.
        cannonTransform = playerCannon.GetComponent<Transform>();
        rb2d = GetComponent<Rigidbody2D>();
        trsfm = GetComponent<Transform>();
        originalPosition = trsfm.position;
        playerResizeOnShoot = GetComponent<PlayerResizeOnShoot>();
        GetOutfitFromPlayerPrefs();
    }

    void FixedUpdate()
    {
        if (buffFasterMovement == true)
        {
            if (debuffSlowerMovement == true)
            {
                debuffSlowerMovementDuration = 0.0f;
            }
            else
            {
                playerHorizontalSpeed = playerDefaultHorizontalSpeed * buffFasterMovementMultiplier;
                playerVerticalSpeed = playerDefaultVerticalSpeed * buffFasterMovementMultiplier;
            }
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
            if (buffFasterMovement == true)
            {
                buffFasterMovementDuration = 0.0f;
            }
            else
            {
                playerHorizontalSpeed = playerDefaultHorizontalSpeed * debuffSlowerMovementMultiplier;
                playerVerticalSpeed = playerDefaultVerticalSpeed * debuffSlowerMovementMultiplier;
            }
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

        statusEffect[0] = buffAutoShoot;
        statusEffectTime[0] = buffAutoShootDuration;

        statusEffect[1] = buffEnemyLoseHPOverTime; //In UI this spot is Enemy Takes Damage over Time. This is just for testing.
        statusEffectTime[1] = buffEnemyLoseHPOverTimeDuration;

        statusEffect[2] = buffFasterMovement;
        statusEffectTime[2] = buffFasterMovementDuration;

        statusEffect[3] = buffShield;
        statusEffectTime[3] = buffShieldDuration;

        statusEffect[4] = buffStrongerAttacks;
        statusEffectTime[4] = buffStrongerAttacksDuration;

        statusEffect[5] = debuffInvertedMovement;
        statusEffectTime[5] = debuffInvertedMovementDuration;

        statusEffect[6] = debuffSlowerMovement;
        statusEffectTime[6] = debuffSlowerMovementDuration;

        if (buffEnemyLoseHPOverTime == true)
        {
            enemies = gameControllerGameObject.GetComponent<GameControllerScript>().enemies;
            foreach (GameObject enemy in enemies)
            {
                float enemyLife, enemyDefaultLife;
                enemyLife = enemy.GetComponent<BacteriaScript>().Life;
                enemyDefaultLife = enemy.GetComponent<BacteriaScript>().GetDefaultLife;
                enemy.GetComponent<BacteriaScript>().Life = enemyLife - (enemyDefaultLife * buffEnemyLoseHPOverTimeMultiplier);
                //print(enemy + " lost " + (enemyDefaultLife * buffEnemyLoseHPOverTimeMultiplier));
            }
        }

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
        if (buffStrongerAttacks == true)
        {
            bulletDamage = defaultBulletDamage * buffStrongerAttacksMultiplier;
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
                //print(string.Format("[Player {0}]: [{1}] deactivated!", playerID + 1, "buffAutoShoot"));
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
                //print(string.Format("[Player {0}]: [{1}] deactivated!", playerID + 1, "buffFasterMovement"));
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
                //print(string.Format("[Player {0}]: [{1}] deactivated!", playerID + 1, "buffIgnoreConstantDamage"));
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
                //print(string.Format("[Player {0}]: [{1}] deactivated!", playerID + 1, "buffShield"));
            }
            else
            {
                buffShieldDuration = buffShieldDuration - Time.deltaTime;
            }
        }

        //buffStrongerAttacks Timer
        if (buffStrongerAttacks == true)
        {
            if (buffStrongerAttacksDuration < 0.0f)
            {
                buffStrongerAttacksDuration = 0.0f;
                buffStrongerAttacks = false;
                //print(string.Format("[Player {0}]: [{1}] deactivated!", playerID + 1, "buffStrongerAttacks"));
            }
            else
            {
                buffStrongerAttacksDuration = buffStrongerAttacksDuration - Time.deltaTime;
            }
        }

        //buffStrongerAttacks Timer
        if (buffEnemyLoseHPOverTime == true)
        {
            if (buffEnemyLoseHPOverTimeDuration < 0.0f)
            {
                buffEnemyLoseHPOverTimeDuration = 0.0f;
                buffEnemyLoseHPOverTime = false;
                //print(string.Format("[Player {0}]: [{1}] deactivated!", playerID + 1, "buffEnemyLoseHPOverTime"));
            }
            else
            {
                buffEnemyLoseHPOverTimeDuration = buffEnemyLoseHPOverTimeDuration - Time.deltaTime;
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
                //print(string.Format("[Player {0}]: [{1}] deactivated!", playerID + 1, "debuffInvertedMovement"));
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
                //print(string.Format("[Player {0}]: [{1}] deactivated!", playerID + 1, "debuffSlowerMovement"));
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
                this.gameObject.SetActive(false);
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
                this.gameObject.SetActive(false);
            }
        }
    }
}
