using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private bool gameOver;
    private bool reStart;

    void Start ()
        {
        StartCoroutine(SpawnWaves());

        score = 0; //Sets score to 0 at the beginning of the game.
        UpdateScore();

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
        if (reStart)
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
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards [Random.Range (0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            //Breaks the while loop to end the game.
            if (gameOver)
            {
                RestartText.text = "Press 'R' to Restart";
                reStart = true;
                break;
            }
        }
    }
    //Updates the player's score
    void UpdateScore ()
    {
        ScoreText.text ="Score: " + score;
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
        GameOverText.text = "Game Over";
        gameOver = true;
    }
}