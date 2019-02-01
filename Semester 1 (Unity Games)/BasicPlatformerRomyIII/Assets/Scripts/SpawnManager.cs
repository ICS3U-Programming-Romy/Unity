using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

       //A bunch of stuff for platforms
    public int maxPlatforms = 20; //max amount of platforms that can spawn at once
    public GameObject[] platforms; //reference for the platform gameObject

    //how far they can spawn away from eachother
    public float horizontalMin = 7.5f;
    public float horizontalMax = 14f;
    public float verticalMin = -6f;
    public float verticalMax = 6f; 

    private Vector2 originPosition;

    void Start()
    {
        originPosition = transform.position; //sets the originPosition as the positon of the gameObject that this is attached to.
        Spawn(); //Runs the spawn function
    }

    void Spawn()
    {
        //Script that allows platforms to spawn.
        for (int i = 0; i < maxPlatforms; i++) //keeps spawning platforms until it reaches maxPlatforms
        {
            //Randomly spawns platforms
            Vector2 randomPosition = originPosition + new Vector2(Random.Range(horizontalMin, horizontalMax), Random.Range(verticalMin, verticalMax));
            //Instantiate(platform, randomPosition, Quaternion.identity);
            Instantiate(platforms[UnityEngine.Random.Range(0, 3)], randomPosition, Quaternion.identity);
            originPosition = randomPosition;
        }
    }
}
