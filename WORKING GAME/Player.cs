using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    public GameObject oCollision;
    public GameObject oInitialise;
    GameObject oParticle;
    public LayerMask gameplayLayer;
    //Variables
    double spd = 6;
    float spd1 = 6;
    double jumpCool = 15;
    double particleCoolMax = 6;
    double x;
    double y;

    double hsp;
    double vsp;
    double currentHeight;
    double jumpCoolCurrent;
    double facing;
    double particleCool;

    bool moving;

    float xSPD;

    Rigidbody2D rb;
    Animator anim;

    //State Machine
    private enum playerStates
    {
        MOVE,
        ATTACK,
        FAINT
    }
    playerStates state = playerStates.MOVE;
    //Initialise
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        hsp = 0;
        vsp = 0;
        currentHeight = 1;
        jumpCoolCurrent = 0;
        facing = 0;
        particleCool = 0;

        // playerStates state = playerStates.MOVE;
    }

    void playerCollision()
    {
        //Horizontal
        var cols = new ArrayList();
        var collide = false;
        cols.Contains((x + hsp, y, oCollision, cols, false));
        if (cols.Capacity > 0)
        {
            while (!collide)
            {
                for (var i = 0; i < cols.Capacity; i++)
                {
                    if (cols.Contains((x + Math.Sign(hsp), y, (cols, i))))
                    {
                        collide = true;
                        break;
                    }
                }
                if (!collide) x += Math.Sign(hsp);
            }
            hsp = 0;
        }
        else x += hsp;

        //Vertical
        var colsV = new ArrayList();
        var collideV = true;
        colsV.Contains((x, y + vsp, oCollision, colsV, false));
        if (colsV.Capacity > 0)
        {
            while (!collide)
            {
                for (var i = 0; i < colsV.Capacity; i++)
                {
                    if (cols.Contains((x, y + Math.Sign(vsp), (colsV, i))))
                    {
                        collideV = true;
                        break;
                    }
                }
                if (!collideV) y += Math.Sign(vsp);
            }
            vsp = 0;
        }
        else y += vsp;
    }


    // Update is called once per frame
    void Update()
    {
       
        //Set facing
        if (Input.GetKey("right")) facing = 0;
        else if (Input.GetKey("left")) facing = 2;
        else if (Input.GetKey("down")) facing = 3;
        else if (Input.GetKey("up")) facing = 1;

        moving = false;
        if (Input.GetKeyDown("right") || Input.GetKeyDown("left") || Input.GetKeyDown("down") || Input.GetKeyDown("up"))
        {
            Debug.Log("Moving");
            moving = true;
            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * spd1;
        }
        transform.localScale = new Vector3(1, 1, 1);
        UpdateAnimation();
    }

    //Set animation

    void UpdateAnimation()
    {
        if (moving)
        {
            if (particleCool > 0)
                particleCool--;
            else
            {
                //   CreateLayer(x + Random.Range(-2, 2), y + Random.Range(-2, 2), gameplayLayer, oParticle);
                particleCool = particleCoolMax;
            }
            switch (facing)
            {
                case 0:
                    {

                        //right
                        break;
                    }
                case 2:
                    {
                        //  sprite_index = sPlayerMoveS;
                        //  image_xscale = (facing * -1) + 1;
                        //move left
                        break;
                    }
                case 1:
                    {
                        //move up
                        /// sprite_index = sPlayerMoveB;
                        break;
                    }
                case 3:
                    {//move down
                        // sprite_index = sPlayerMoveF;
                        break;
                    }
            }
        }
        else
        {
            switch (facing)
            {
                case 0:
                case 2:
                    {
                        //   sprite_index = sPlayerIdleS;
                        //   image_xscale = (facing * -1) + 1;
                        break;
                    }
                case 1:
                    {
                        // sprite_index = sPlayerIdleB;
                        break;
                    }
                case 3:
                    {
                        // sprite_index = sPlayerIdleF;
                        break;
                    }
            }
        }
        //Player State
        switch (state)
        {
            case playerStates.MOVE:
                {
                    hsp = Input.GetAxisRaw("Horizontal") * spd;
                    vsp = Input.GetAxisRaw("Vertical") * spd;
                    //Keep diagonal hypotenuse
                    if ((hsp != 0) && (vsp != 0))
                    {
                        hsp /= Math.Sqrt(2);
                        vsp /= Math.Sqrt(2);
                    }


                    if (jumpCoolCurrent > 0) jumpCoolCurrent--;
                    else
                    {
                        if (Input.GetButtonDown("Jump"))
                        {
                            jumpCoolCurrent = jumpCool;
                            currentHeight++;
                        }
                    }
                    playerCollision();
                    break;
                }
        }

    }

}