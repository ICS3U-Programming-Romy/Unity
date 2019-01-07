using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollect : MonoBehaviour
{
    //stuff for the scoring mechanic
    public int scoreValue;
    private GameController gameController;

    void Start()
    {
        //Finds the GameController Object
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) //when the "Player" touches the coin, it destroys the coin.
        {
            gameController.addScore(scoreValue); //added for increasing score.
            gameController.soundYeet(); //used to play sound after the coin is collected
            Kill();
        }
    }

    void Kill()
    {
        Destroy(gameObject);
    }
}
