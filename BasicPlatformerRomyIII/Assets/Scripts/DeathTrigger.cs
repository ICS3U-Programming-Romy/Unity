using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathTrigger : MonoBehaviour
{
    public Text GameOverText;
    public Text RestartText;
    private bool reStart;

    void Start()
    {
        reStart = false;
        GameOverText.text = "";
        RestartText.text = "";
    }

    void Update()
    {
        if (reStart) //Script that allows the player to restart.
        {
            GameOverText.text = "Game Over"; //Shows the player "GAME OVER"
            RestartText.text = "Press 'R' to Restart"; //Shows the restart text.
            if (Input.GetKeyDown(KeyCode.R))
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
    }

       void OnTriggerEnter2D (Collider2D other)
       {
            if (other.gameObject.CompareTag("Player"))
            {
            reStart = true;
            }

            if (other.gameObject.CompareTag("ground"))
            {
                Destroy(other.gameObject);
            }
        }
}

