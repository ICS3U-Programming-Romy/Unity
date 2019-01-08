using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathTrigger : MonoBehaviour
{//some variables for the restart and game over text
    public Text GameOverText;
    public Text RestartText;
    private bool reStart;

    void Start() //at the start of the game it sets the bool reStart to false and sets both texts to null.
    {
        reStart = false;
        GameOverText.text = "";
        RestartText.text = "";
    }

    void Update()
    {//basically checks if the player CAN restart
        if (reStart) //Script that allows the player to restart.
        {
            GameOverText.text = "Game Over"; //Shows the player "GAME OVER"
            RestartText.text = "Press 'R' to Restart"; //Shows the restart text.
            if (Input.GetKeyDown(KeyCode.R))// if the 'R' key is pushed down, it reloads the game.
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
    }
 
       void OnTriggerEnter2D (Collider2D other)
       {//when the player touches the collider on the object that this script is attached to, it sets the bool reStart to true.
            if (other.gameObject.CompareTag("Player"))
            {
            reStart = true;
            }

            //if a platforms touches it, it destroys the platform.
            if (other.gameObject.CompareTag("ground"))
            {
                Destroy(other.gameObject);
            }
        }
}

