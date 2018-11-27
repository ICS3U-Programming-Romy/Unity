using UnityEngine;
using System.Collections;

//Is used to make a list of floats. For organization. All floats for player boundaries in the game.
[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayersController : MonoBehaviour
{
    private Rigidbody rb; //Used to make life easier. Instead of writing rigidbody.whatever everytime. I only write rb.whatever.
    public float speed;
    public float tilt;
    public Boundary boundary; //Reference to the Boundary class (with the list of floats).

    //Stuff for the code to fire a "Blot" shot.
    public GameObject shot;
    public Transform shotSpawn;
    private float nextFire;
    public float fireRate;
    public GameObject shot2;
    private float nextFire2;
    public float fireRate2;

    //Creates a container for the AudioSource component
    private AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); //References the rigidbody component for "private Rigidbody rb;".
        audioSource = GetComponent<AudioSource>(); //References the player weapon laser sound effects.
    }

    void Update() //The code to fire a "Bolt" and "Bolt2" shot. With sound effects
    {//Bolt
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            audioSource.Play();
        }
        //Bolt2
        if (Input.GetKeyDown("space") && Time.time > nextFire2)
        {
            nextFire2 = Time.time + fireRate2;
            Instantiate(shot2, shotSpawn.position, shotSpawn.rotation);
            audioSource.Play();
        }
    }

    void FixedUpdate() //All for movement.
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
        rb.velocity = movement * speed;

        rb.position = new Vector3
        (
            Mathf.Clamp (rb.position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp (rb.position.z, boundary.zMin, boundary.zMax)
        );
        rb.rotation = Quaternion.Euler (0.0f, 0.0f, rb.velocity.x * -tilt);
    }

}