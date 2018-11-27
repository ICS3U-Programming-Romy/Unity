using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  //This is required to change the text in a canvas/text box.

public class GameController : MonoBehaviour
{
    //Stuff for the hazard spawning
    public GameObject[] hazards; //Makes reference to the asteroid
    public Vector3 spawnValues;
    public int hazardCount; //Max hazards in 1 wave.
    public float startWait; 
    public float spawnWait;
    public float waveWait;

    //Stuff for all the text in the Canvas
    public Text ScoreText; //references the GUIText component
    public int score; //Player's score
    public Text GameOverText;
    public Text RestartText;

    //Tells the game if the player lost (gameover) and when they can restart.
    private bool gameOver;
    private bool reStart;

    void Start ()
        {
        StartCoroutine(SpawnWaves()); //Starts the wave spawning.

        score = 0; //Sets score to 0 at the beginning of the game.
        UpdateScore();
        
        //Sets the gameover and restart (Allowing for restart) functions to false. So the player doesn't get a gameover when they start the game.
        gameOver = false;
        reStart = false;
        GameOverText.text = "";
        RestartText.text = "";

        //Used to get the ScoreText in the canvas.
        GameObject scoreTextObject = GameObject.FindWithTag("ScoreText"); //Finds a gameObject with the ScoreText tag.
        if (scoreTextObject != null)//If it does find an object with the tag, it gets the Text component
        {
            ScoreText = scoreTextObject.GetComponent<Text>();
        }
        if (ScoreText == null)//Used for debugging purposes. Just incase something goes wrong and it cannot find the tag, it will tell me.
        {
            Debug.Log("Cannot Find 'scoreText' Script");
        }
    }

    void Update()
    {
        if (reStart) //Script that allows the player to restart.
        {
            if (Input.GetKeyDown (KeyCode.R))
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
    }


    //Code for a wave spawning for hazards loop, endless spawning of hazards (In waves). 
    IEnumerator SpawnWaves ()
    {
        yield return new WaitForSeconds(startWait); //Waits at the beginning before spawning waves. 
        while (true) //While 'true' = 'true' it will run the code indefinatley.
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards [Random.Range (0, hazards.Length)]; //Picks random obstacle from the array to spawn.
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z); //Chooses a random spawn location for obstacles, within the boundaries.
                Quaternion spawnRotation = Quaternion.identity; //Adds random rotation the the obstacles.
                Instantiate(hazard, spawnPosition, spawnRotation); //Spawns the obstacles at the given location
                yield return new WaitForSeconds(spawnWait); //Waits before spawning another obstacle, so they don't collide with eachother.
            }
            yield return new WaitForSeconds(waveWait); //Waits before spawning another wave of obstacles.

            //Breaks the while loop to end the game.
            if (gameOver) //If the game ends, code is executed
            {
                RestartText.text = "Press 'R' to Restart"; //Shows the restart text.
                reStart = true;
                break; //Breaks the loop.
            }
        }
    }
    //Updates the player's score
    void UpdateScore ()
    {
        ScoreText.text ="Score: " + score; //Changes the score integer to a string.
    }
    //Function that increases player's score, from the ScoreValue of the asteroids.
    public void AddScore (int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }
    //Function that allows the game to end when the player is destroyed.
    public void GameOver ()
    {
        GameOverText.text = "Game Over"; //Shows the player "GAME OVER"
        gameOver = true;
    }
}