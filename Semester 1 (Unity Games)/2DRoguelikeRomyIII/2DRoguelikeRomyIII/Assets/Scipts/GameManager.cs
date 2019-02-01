using UnityEngine;
using System.Collections;
using System.Collections.Generic;//For lists

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null; //Static instance of GameManager which allows it to be accessed by any other script.
    public BoardManager boardScript;   //Reference to the BoardManager, to set up the level.
    private int level = 1; //The Level Number
    public int playerFoodPoints = 100;
    [HideInInspector] public bool playersTurn = true;

    public float turnDelay = 0.1f;
    private List<Enemy> enemies;
    private bool enemiesMoving;


    void Awake() //Is called before Start function
    {
        //Basically prevents there from being more that 1 gamemanager
        if (instance == null)//Check if instance already exists
            instance = this;//if not, set instance to this

        else if (instance != this)//If instance already exists and it's not this
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);//Prevents this gameobject from being destroyed when reloading the scene
        enemies = new List<Enemy>();
        boardScript = GetComponent<BoardManager>();//Get the attached BoardManager script
        InitGame();
    }

    void InitGame()//Starts the game.
    {
        enemies.Clear(); //Clears enemy list
        boardScript.SetupScene(level); //Call the SetupScene function of the BoardManager script.
    }


    void Update()
    {
        if (playersTurn || enemiesMoving) //Check that playersTurn or enemiesMoving or doingSetup are not currently true
            return; //If any of these are true, return and do not start MoveEnemies.

        StartCoroutine(MoveEnemies());       //Start moving enemies.
    }


    public void AddEnemyToList(Enemy script)
    {
        enemies.Add(script); //Add Enemy to List enemies.
    }


    IEnumerator MoveEnemies()//Coroutine to move enemies in sequence.
    {
        enemiesMoving = true; //If the enemies are moving, the player can't.
        yield return new WaitForSeconds(turnDelay); //Wait for turnDelay seconds, defaults to .1 (100 ms).
        if (enemies.Count == 0)        //If there are no enemies spawned (IE in first level):
        {
            yield return new WaitForSeconds(turnDelay); //Wait for turnDelay seconds between moves, replaces delay caused by enemies moving when there are none.
        }

        for (int i = 0; i < enemies.Count; i++) //Loop through List of Enemy objects.
        {
            enemies[i].MoveEnemy(); //Call the MoveEnemy function of Enemy at index i in the enemies List.
            yield return new WaitForSeconds(enemies[i].moveTime); //Wait for Enemy's moveTime before moving next Enemy, 
        }

        playersTurn = true; //Once Enemies are done moving, set playersTurn to true so player can move.
        enemiesMoving = false;//Enemies are done moving, set enemiesMoving to false.
    }


    public void GameOver() //when function is run, disable this gameobject.
    {
        enabled = false;
    }
}