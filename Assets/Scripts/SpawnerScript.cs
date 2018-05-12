using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
	private GameObject gameControllerGameObject; //Reference to the Game Controller GameObject;
	private string gameControllerTag = "GameController"; //Game Controller's tag;
	private float screenBoundariesX, screenBoundariesY; //Receives the GameController's boundaries on Awake().
	private bool gameOver;
    public bool isSpawnInfinite = false; //Ignore the GameObject instantiation limit.
    public bool useRandomPosition = true; //If true, the position to instantiate the GameObject will be random, respecting the screen bounds.
    public GameObject gameObjectToInstantiate; //GameObject to Instantiate.
    public Vector2 positionToSpawn; //Position to instantiate the GameObject.
    public Quaternion rotationToSpawn; //Rotation to instantiate the GameObject.
    public int amountToInstantiate; //The amount to instantiate.
    public float timeToInstantiate; //Time to instantiate each GameObject.
    private Vector2 randomPosition; //Holds a random position.
    private int amountInstantiated = 0; //Internal variable. Amount of instantiated GameObjects.
	private Renderer rend;

	void Awake()
	{
		gameControllerGameObject = GameObject.FindGameObjectWithTag (gameControllerTag);
		screenBoundariesX = gameControllerGameObject.GetComponent<GameControllerScript> ().ScreenBoundariesX; //Define local screen boundaries variables according to the GameController variables.
		screenBoundariesY = gameControllerGameObject.GetComponent<GameControllerScript> ().ScreenBoundariesY; //Define local screen boundaries variables according to the GameController variables.
		gameOver = gameControllerGameObject.GetComponent<GameControllerScript> ().GameOver;
	}
    void OnEnable()
    {
        Invoke("InstantiateGameObject", timeToInstantiate); //Repeatedly invoke InstantiateGameObject each timeToInstantiate seconds.
		rend = GetComponent<Renderer>();
    }
    void Update()
	{
		if (useRandomPosition == true)
		{
			randomPosition = new Vector2 (Random.Range (-screenBoundariesX, screenBoundariesX), Random.Range (-screenBoundariesY, screenBoundariesY));
		}
	}
    void InstantiateGameObject()
    {
		gameOver = gameControllerGameObject.GetComponent<GameControllerScript> ().GameOver;
		if ((amountInstantiated < amountToInstantiate || isSpawnInfinite == true) && gameOver == false) //Checks if the defined GameObjects instances limit has been reached or if isSpawnInfinite is enabled and if the game is not over.
        {
			if (useRandomPosition == true) 
			{
				positionToSpawn = randomPosition;
			}
			else
			{
				float yMinus = transform.position.y - rend.bounds.size.y / 2;
				float yPlus = transform.position.y + rend.bounds.size.y / 2;
				positionToSpawn = new Vector2 (transform.position.x, Random.Range (yMinus, yPlus));
			}
            Instantiate(gameObjectToInstantiate, positionToSpawn, rotationToSpawn); //Instantiate the GameObject.
            amountInstantiated++; //Adds 1 to the amount of instanciated GameObjects.
            Invoke("InstantiateGameObject", timeToInstantiate); //Invoke InstantiateGameObject once again.
        }
    }
}
