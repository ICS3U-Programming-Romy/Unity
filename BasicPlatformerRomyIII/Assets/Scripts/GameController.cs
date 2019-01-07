using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //for canvas text

[RequireComponent(typeof(AudioSource))]
public class GameController : MonoBehaviour {
    //all of this is added tto increase score

    //Stuff for all the text in the Canvas
    public Text ScoreText; //references the GUIText component
    public int score; //Player's score
    public GameObject SoundSource;
    public AudioSource audioData;

    void Start()
    {
        audioData = GetComponent<AudioSource>();
        score = 0; //Sets score to 0 at the beginning of the game.
        UpdateScore();
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
    //increasing / displaying score
    public void addScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }
    public void UpdateScore()
    {
        ScoreText.text = "Score: " + score;
    }

    public void soundYeet()
    {
        audioData.Play(0);
    }
}
