using UnityEngine;
using System.Collections;

public class Loader : MonoBehaviour
{
    public GameObject gameManager;

    void Awake()
    { //If there is no gameManager, then make one.
        if (GameManager.instance == null)
            Instantiate(gameManager); //Creates the gameManager
    }
}