﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//In the comments, "It" refers to the object that this script is attached to.

public class DestroyByContact : MonoBehaviour
{
    //References to explosion effects.
    public GameObject explosion;
    public GameObject playerExplosion;
    //Stuff to increase score
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

    //Creates explosion effect when the asteroid hits another collider
    void OnTriggerEnter(Collider other)
        {

        if (other.CompareTag("Asteroid") || other.CompareTag("Asteroid"))
        {
            return;
        }

        //Prevents the Boundary from activating the rest of the code. Will ignore the Boundary's collider
        if (other.CompareTag ("Boundary") || other.CompareTag("Enemy"))
            {
              return;
            }
        //Creates an explosion when it hits another collider
        if (explosion !=null)
        {
            Instantiate(explosion, transform.position, transform.rotation);
        }
        //Creates a player explosion when it hits the player
        if (other.tag == "Player")
            {
                Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
                gameController.GameOver();
            }
        //Destroys the 2 gameObjects that collided
        Destroy(other.gameObject);
        Destroy(gameObject);
        //References the GameController so it could increase the score when an asteroid is hit.
        gameController.AddScore(scoreValue);
        }
}
