using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformFall : MonoBehaviour {

    public float fallDelay = 1f; //variable to change how long a platform will stay up
    private Rigidbody2D rb2d; //Reference for the rigidbody component

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>(); //gets the rigidbody component
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        //When the gameObject that this is attached to touches another gameObject tagged 'Player' it will 'Fall'
        if (other.gameObject.CompareTag("Player"))
            Invoke ("Fall", fallDelay);

    }
    //sets isKinematic to false, to allow the platform to fall.
    void Fall()
    {
        rb2d.isKinematic = false;
    }
}
