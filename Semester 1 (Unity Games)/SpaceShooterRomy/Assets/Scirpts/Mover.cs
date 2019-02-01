using System.Collections;
using UnityEngine;


//All this script is, is to allow bolts and asteroids to move at a constant speed. That's All.
public class Mover : MonoBehaviour
{
    private Rigidbody rb; //Makes an "rb" container contaning the rigidbody component.
    public float speed; //Float number that can be changed to change the objects speed

    void Start ()
    {
        rb = GetComponent<Rigidbody>(); //Gets the rigidbody component
        rb.velocity = transform.forward * speed; //Makes the objects move.
    }
}
