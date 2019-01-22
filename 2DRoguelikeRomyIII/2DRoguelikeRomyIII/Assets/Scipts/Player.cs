using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MovingObject
{

    public float restartLevelDelay = 3f;        //Delay time in seconds to restart level.
    public int wallDamage = 1;  //The damage that the player does to the wall.
    public int pointsPerFood = 10; //Increases the food points when food is picked up.
    public int pointsPerSoda = 20; //Increases the food points when soda is picked up.

    private Animator animator;
    private int food;  //Stores the player's score



    protected override void Start()
    {
        animator = GetComponent<Animator>();
        food = GameManager.instance.playerFoodPoints;

        base.Start();
    }



    private void OnDisable()//This function is called when the behaviour becomes disabled or inactive.
    {
        GameManager.instance.playerFoodPoints = food;//When Player object is disabled, store the food points
    }



    private void Update()
    {
        if (!GameManager.instance.playersTurn)
        {
            return; //return if it's not the player's turn
        }

        int horizontal = 0;     //Used to store the horizontal move direction.
        int vertical = 0;       //Used to store the vertical move direction.

        horizontal = (int) (Input.GetAxisRaw("Horizontal")); //Get input from the input manager and store in horizontal to set x axis move direction
        vertical = (int) (Input.GetAxisRaw("Vertical"));//Get input from the input manager and store in vertical to set y axis move direction

        if (horizontal != 0) //prevents the player from moving diagonaly. 
            vertical = 0;//if player is moving horizontally, i.e. horizontal is not 0, then the player can't/isn't allowed to move vertically


        if (horizontal != 0 || vertical != 0) //Check if we have a non-zero value for horizontal or vertical
            AttemptMove<Wall> (horizontal, vertical);//when the player tries to move, it calls the AttemptMove function.
    }



    protected override void AttemptMove <T> (int xDir, int yDir)
    {
        food--;//removes food when player moves.
        base.AttemptMove <T> (xDir, yDir);
        RaycastHit2D hit;//Reference the line cast done in move.

        CheckIfGameOver();
        GameManager.instance.playersTurn = false;
    }



    protected override void OnCantMove <T> (T component)
    {
        Wall hitWall = component as Wall;//Set hitWall to equal the component passed in as a parameter.
        hitWall.DamageWall(wallDamage);//Call the DamageWall function of the Wall we are hitting.
        animator.SetTrigger("playerChop");//Set the attack trigger of the player's animation controller in order to play the player's attack animation.
    }



    private void OnTriggerEnter2D(Collider2D other)
    {//All these check what object the player is touching. Either the exit, food, or soda.

        if (other.tag == "Exit")//If the player touches the exit, it restarts the level.
        {
            Invoke("Restart", restartLevelDelay);
            enabled = false;
        }

        else if (other.tag == "Food")//if the player touches food, increase the food total and disable the gameobject.
        {
            food += pointsPerFood;
            other.gameObject.SetActive(false);
        }

        else if (other.tag == "Soda")//if the player touches soda, increase the food total and disable the gameobject.
        { }
        food += pointsPerSoda;
        other.gameObject.SetActive(false);
    }



    private void Restart()
    {
        SceneManager.LoadScene (0);
  
  }



    public void LoseFood(int loss) //Runs when the player is hit by an enemy and specifies how much food is lost
    {
        animator.SetTrigger("playerHit"); //Set the trigger for the player animator to transition to the playerHit animation.
        food -= loss; //Depletes the player's food score
        CheckIfGameOver(); //Check to see if game has ended.
    }



    private void CheckIfGameOver()
    {
        if (food <= 0) //when food is less than or equal to 0, GameOver
        {
            GameManager.instance.GameOver();
        }
    }
}