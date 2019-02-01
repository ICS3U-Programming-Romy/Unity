using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawn : MonoBehaviour
{ // some variables for the coins
    public Transform[] coinSpawns; //an array for the possible spawn locations for the coin.
    public GameObject coin; // for the actual coin object

	void Start () //when the game starts it runs the Spawn() function.
    {
        Spawn();	
	}

    void Spawn() //randomly spawns coins on the platforms on the random spawn loaction.
    {
        for (int i = 0; i < coinSpawns.Length; i++)
        {
            int coinFlip = Random.Range(0, 2);
            if (coinFlip > 0)
            {
                Instantiate(coin, coinSpawns[i].position, Quaternion.identity);
            }
        }
    }
}
