using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MovingObject
{
    public int playerDamage; //The damage that the enemy can do (Subtracts food points).
    private Animator animator;
    private Transform target; //Store transform of 'targe'.
    private bool skipMove; //Can skip turn? true or false?


    protected override void Start()
    {
        GameManager.instance.AddEnemyToList(this);
        animator = GetComponent<Animator>(); //Gets the animator component.
        target = GameObject.FindGameObjectWithTag ("Player").transform; //Finds the player and sets it as target.
        base.Start();        //Call the start function of our base class MovingObject.


    }


    //Override the AttemptMove function of MovingObject to include functionality needed for Enemy to skip turns.
    protected override void AttemptMove <T> (int xDir, int yDir)
    {
        if (skipMove)//Checks if skip move is true, if so, set to false, then return.
        {
            skipMove = false;
            return;
        }

        base.AttemptMove <T> (xDir, yDir); //Calls AttemptMove Function from base class moving object.
        skipMove = true;
    }


    //MoveEnemy is called by the GameManger each turn to tell each Enemy to try to move towards the player.
    public void MoveEnemy()
    {//Allows the choosing of direction, up down left right.
        int xDir = 0;
        int yDir = 0;

        if (Mathf.Abs (target.position.x - transform.position.x) < float.Epsilon)    //If the difference in positions is approximately zero (Epsilon) do the following:
            yDir = target.position.y > transform.position.y ? 1 : -1;  //is target y position greater than my y position? yes=move up, no=move down.

        else      //If the difference in positions is not approximately zero (Epsilon) do the following:
            xDir = target.position.x > transform.position.x ? 1 : -1; //is target x position greater than my x position? yes=move right, no=move left.

        AttemptMove <Player> (xDir, yDir); //Call AttemptMove
    }


    protected override void OnCantMove <T> (T component)
    {
        Player hitPlayer = component as Player;      //Declare hitPlayer and set it to equal the encountered component.
        hitPlayer.LoseFood(playerDamage);        //Call the LoseFood function of hitPlayer passing it playerDamage, the amount of foodpoints to be subtracted.
        animator.SetTrigger("enemyAttack");        //Set the attack trigger of animator to trigger Enemy attack animation.
    }
}