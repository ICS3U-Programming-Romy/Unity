using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null; //Static instance of GameManager which allows it to be accessed by any other script.
    private BoardManager boardScript;   //Reference to the BoardManager, to set up the level.
    private int level = 3; //The Level Number

    void Awake()//Is called before Start function
    {
        boardScript = GetComponent<BoardManager>();//Get the attached BoardManager script
        InitGame();

        //Basically prevents there from being more that 1 gamemanager
        if (instance == null)//Check if instance already exists
            instance = this;//if not, set instance to this
        else if (instance != this) //If instance already exists and it's not this
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);//Prevents this gameobject from being destroyed when reloading the scene
    }

    void InitGame()//Starts the game.
    {
        //Call the SetupScene function of the BoardManager script.
        boardScript.SetupScene(level);

    }
}