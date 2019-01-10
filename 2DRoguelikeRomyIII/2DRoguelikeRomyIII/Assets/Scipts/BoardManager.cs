using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{
    [Serializable] //More functionality in the inspector, basically for organization
    public class Count
    { 
        //Some variables 
        public int minimum;
        public int maximum;

        public Count(int min, int max)
        {
            minimum = min;
            maximum = max;
        }
    }
    //Defines the game's grid size
    public int columns = 8;
    public int rows = 8;
    //Limits for max and min number of walls and food
    public Count wallCount = new Count(5,9);
    public Count foodCount = new Count(1, 5);

    public GameObject exit; //for the exit sign

    //A bunch of arrays for the sprites
    public GameObject[] floorTiles;
    public GameObject[] wallTiles;
    public GameObject[] outterWallTiles;
    public GameObject[] foodTiles;
    public GameObject[] enemyTiles;

    private Transform boardHolder; //for the Board object
    private List<Vector3> gridPositions = new List<Vector3>(); //A list of possible locations to place tiles.

    //Clears our list gridPositions and prepares it to generate a new board.
    void InitialiseList ()
    {
        gridPositions.Clear(); //clears list

        //Loop through x axis (columns).
        for (int x = 1; x < columns -1; x++)
        {
            //Within each column, loop through y axis (rows).
            for (int y = 1; x< rows -1; y++)
            {
                //At each index add a new Vector3 to our list with the x and y coordinates of that position.
                gridPositions.Add(new Vector3(x, y, 0f));
            }
        }
    }
    
    //Self explanitory..... Sets up the game board, places the Outer wall and floor tiles
    void BoardSetup()
    {
        boardHolder = new GameObject("Board").transform;

        //Loop along x axis, starting from -1 (to fill corner) with floor or outerwall edge tiles.
        for (int x = 1; x < columns + 1; x++)
        {
            //Loop along y axis, starting from -1 to place floor or outerwall tiles.
            for (int y = 1; x < rows + 1; y++)
            {
                //Chooses a random tiles from the array
                GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];
                //Makes sure that only outter walls spawn at the edges
                if (x == -1 || x == columns || y == -1 || y == rows)
                    toInstantiate = outterWallTiles[Random.Range(0, outterWallTiles.Length)];

                //Places all of the objects
                GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0.0f), Quaternion.identity) as GameObject;

                //Parents all of the gameobjects to 'boardHolder', for organization.
                instance.transform.SetParent(boardHolder);
            }
        }
    }
    //Chooses a random position from gridPositions
    Vector3 RandomPosition()
    {
        int randomIndex = Random.Range(0, gridPositions.Count); //declares int, sets value to a random number in the amount of items in gridPositions
        Vector3 randomPosition = gridPositions[randomIndex]; //Declares Vector3. Sets it to the randomIndex above.
        gridPositions.RemoveAt(randomIndex); //removes the entry, so that it isn't reused
        return randomPosition; //Return the randomly selected Vector3 position.
    }

    //LayoutObjectAtRandom accepts an array of game objects to choose from along with a minimum and maximum range for the number of objects to create.
    void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum)
    {
        //Chooses a random number of objects to create.
        int objectCount = Random.Range(minimum, maximum +1);

        //Creates objects until that random number is reached
        for (int i = 0; i < objectCount; i++)
        {
            Vector3 randomPosition = RandomPosition(); //Chooses random position
            GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)]; //Chooses random tile
            Instantiate(tileChoice, randomPosition, Quaternion.identity); //creates object, Quaternion.identity means no change in rotation.
        }
    }

    public void SetupScene(int level)
    {

        BoardSetup(); //Calls BoardSetup to place the walls and floor tiles
        InitialiseList(); //Reset our list of gridpositions.

        //Instantiates a random number of wall and food tiles at random positions
        LayoutObjectAtRandom(wallTiles, wallCount.minimum, wallCount.maximum);        
        LayoutObjectAtRandom(foodTiles, foodCount.minimum, foodCount.maximum); 

        //Instantiates a random number of enemies at random positions
        int enemyCount = (int)Mathf.Log(level, 2f);
        LayoutObjectAtRandom(enemyTiles, enemyCount, enemyCount);

        //Places the exit sign at the top right corner of the game board.
        Instantiate(exit, new Vector3(columns - 1, rows - 1, 0f), Quaternion.identity);
    }
}