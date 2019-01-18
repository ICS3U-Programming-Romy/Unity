using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{

    public Sprite dmgSprite;                    //Alternate sprite to display after Wall has been attacked by player.
    public int hp = 3;                          //hit points for the wall.


    private SpriteRenderer spriteRenderer; //Sprite Renderer compoenent


    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); //Gets the sprite renderer
    }


    public void DamageWall(int loss) //Called when the player attacks the wall.
    {
        spriteRenderer.sprite = dmgSprite; //The wall sprite is changed to a damaged one.
        hp -= loss; //Decreases the hit points of the wall

        if (hp <= 0)
            gameObject.SetActive(false);//Disable the gameObject.
    }
}