using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The abstract keyword enables you to create classes and class members that are incomplete and must be implemented in a derived class.
public abstract class MovingObject : MonoBehaviour {


    public float moveTime = 0.1f; //Time the object takes to move (seconds).
    public LayerMask blockingLayer; //The layer that deals with collisions.

    private BoxCollider2D boxCollider; //2D box collider component.
    private Rigidbody2D rb2D; //Rigidbody 2D component.
    private float inverseMoveTime; //Makes the calculations for the movement better.


    protected virtual void Start()//Protected, virtual functions can be overridden by inheriting classes.
    {
        boxCollider = GetComponent<BoxCollider2D>(); //Gets the attached box collider component.
        rb2D = GetComponent<Rigidbody2D>(); //Gets the attached Rigidbody component.
        inverseMoveTime = 1f / moveTime;//By storing the reciprocal of the move time we can use it by multiplying instead of dividing, this is more efficient.
    }


    protected bool Move (int xDir, int yDir, out RaycastHit2D hit)
    {
        Vector2 start = transform.position;
        Vector2 end = start + new Vector2(xDir, yDir); // Calculate end position based on the direction parameters passed in when calling Move.
        boxCollider.enabled = false; //Turns off box collider
        hit = Physics2D.Linecast(start, end, blockingLayer); //Checks for collision
        boxCollider.enabled = true; //Turns box collider back on
        if (hit.transform == null)
        {
            StartCoroutine(SmoothMovement(end)); //If nothing was hit, start SmoothMovement.
            return true; //Return true to say that Move was successful
        }
        return false;//If something was hit, return false, Move was unsuccesful.
    }


    protected IEnumerator SmoothMovement(Vector3 end) //Co-routine for moving units from one space to next, takes a parameter end to specify where to move to.
    {
        float sqrRemainingDistance = (transform.position - end).sqrMagnitude; //calculate the remaining distance to move based on the sqrMagnitude of the difference between the current position and our end parameter.

        while (sqrRemainingDistance > float.Epsilon)
        {
            Vector3 newPostion = Vector3.MoveTowards(rb2D.position, end, inverseMoveTime * Time.deltaTime);
            rb2D.MovePosition(newPostion);
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;

            yield return null;//Return and loop until sqrRemainingDistance is close enough to zero to end the function.
        }
    }


    //The virtual keyword means AttemptMove can be overridden by inheriting classes using the override keyword.
    protected virtual void AttemptMove<T>(int xDir, int yDir)
        where T : Component
    {
        RaycastHit2D hit; //Hit stores whatever the linecast hits when move is called.
        bool canMove = Move(xDir, yDir, out hit); //Set canMove to true if Move was successful, false if failed.

        if (hit.transform == null)
            return;//If nothing was hit, don't continue.

        T hitComponent = hit.transform.GetComponent<T>();//Get a component reference to the component of type T attached to the object that was hit

        if (!canMove && hitComponent != null)
            OnCantMove(hitComponent); //Call the OnCantMove function and pass it hitComponent as a parameter.
    }


    protected abstract void OnCantMove <T> (T component)
        where T : Component;
}
