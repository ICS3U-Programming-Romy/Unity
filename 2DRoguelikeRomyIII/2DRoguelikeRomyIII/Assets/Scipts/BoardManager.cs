using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{
    [Serializable]
    public class Count
    {
        public int minimum;
        public int maximum;

        public Count(int min, int max)
        {
            minimum = min;
            maximum = max;
        }
    }

    public int columns = 8;
    public int rows = 8;
    public Count wallCount = new Count(5,9);
    public Count foodCount = new Count(1, 5);
    public GameObject exit;
    public GameObject[] floorTiles;
    public GameObject[] wallTiles;
    public GameObject[] outterWallTiles;
    public GameObject[] foodTiles;
    public GameObject[] enemyTiles;

    private Transform boardHolder;
    private List<Vector3> gridPositions = new List<Vector3>();

    void InitialiseList ()
    {
        gridPositions.Clear();

        for (int x = 1; x < columns -1; x++)
        {
            for (int y = 1; x< rows -1; y++)
            {
                gridPositions.Add(new Vector3(x, y, 0f));
            }
        }
    }

    void BoardSetup ()
    {
        boardHolder = new GameObject("Board").transform;

        for (int x = 1; x < columns + 1; x++)
        {
            for (int y = 1; x < rows + 1; y++)
            {
                GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];
                if (x == -1 || x == columns || y == -1 || y == rows)
                {
                    toInstantiate = outterWallTiles[Random.Range(0, outterWallTiles.Length);] //Video 8:15
                }
            }
        }

    void Start () {
		
	}
	
	void Update () {
		
	}
}
