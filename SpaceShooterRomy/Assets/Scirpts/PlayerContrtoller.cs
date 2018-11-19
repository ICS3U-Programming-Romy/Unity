using System.Collections;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}
 
public class PlayerContrtoller : MonoBehaviour {

    public Rigidbody rb;
    public float speed;
    public Boundary boundary;

    void FixedUpdate()
    {
        float moveH = Input.GetAxis("Horizontal");
        float moveV = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3 (moveH, 0.0f, moveV);
        rb.velocity = movement * speed;
        rb.position = new Vector3(Mathf.Clamp(rb.position.x, xMin, xMax), 0.0f, Mathf.Clamp(rb.position.z, zMin, zMax));
    }
}
