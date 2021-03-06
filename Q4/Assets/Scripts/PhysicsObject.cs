﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour {

    //Reference Variables
    public const float minMove = .001f;
    Transform trans;
    public Collider col;

    //Modifiable Variables
    public int CollisionType = 0;
    public bool Gravity = false;
    public float GravRate = 20f;
    public bool DestroyOnContact = false;
    public bool CeaseHSpeedOnGround = false;

    //Collider Variables
    public float ColliderWidth;
    public float ColliderHeight;

    //Physics Object Variables   
    public float hSpeed;
    public float vSpeed;

    //Housekeeping Variables
    bool SetToDestroy = false;

    //Define Reference Variables
    void OnEnable()
    {
        //Define Reference Variables
        trans = GetComponent<Transform>();
        col = gameObject.AddComponent<Collider>();
        col.CollisionType = CollisionType;
       
    }

    //Initialize Variables
    private void Start()
    {
        //Set Collider Size (Optional Overriding)
        if (ColliderWidth != 0) col.width = ColliderWidth;
        if (ColliderHeight != 0) col.height = ColliderHeight;
    }

    // Update is called once per frame
    void Update () {

        //Set To Destroy
        //(used to destroy after collision)
        if(SetToDestroy)
        {
            Destroy(gameObject);
        }

        //Cease HSpeed On Ground (Specifically to prevent Granny from sliding)
        if (CeaseHSpeedOnGround && col.PlaceMeeting(trans.position.x, trans.position.y - minMove, 0) && vSpeed <= 0)
        {
            hSpeed = 0;
        }

        //Horizontal Collision
        if (hSpeed != 0)
        {
            if(!col.PlaceMeeting(trans.position.x+hSpeed*Time.deltaTime, trans.position.y, 0))
            {
                trans.position += new Vector3(hSpeed * Time.deltaTime, 0);
            }
            else if(!col.PlaceMeeting(trans.position.x+minMove*Sign(hSpeed),trans.position.y,0))
            {
                do
                {
                    trans.position += new Vector3(minMove * Sign(hSpeed), 0);
                }
                while (!col.PlaceMeeting(trans.position.x + minMove * Sign(hSpeed), trans.position.y, 0));
            }

            //Cease HSpeed
            if(col.PlaceMeeting(trans.position.x + minMove * Sign(hSpeed), trans.position.y, 0))
            {
                hSpeed = 0;

                //Play Collision Actions
                OnCollisionActions();
            }
        }


        //Gravity
        if(Gravity && !col.PlaceMeeting(trans.position.x, trans.position.y-minMove, 0))
        {
            vSpeed -= GravRate * Time.deltaTime;
        }

        //Vertical Collision Management
        if(vSpeed != 0)
        {
            if(!col.PlaceMeeting(trans.position.x, trans.position.y+vSpeed*Time.deltaTime, 0))
            {
                trans.position += new Vector3(0, vSpeed) * Time.deltaTime;
            }
            else if(!col.PlaceMeeting(trans.position.x, trans.position.y+minMove*Sign(vSpeed),0))
            {
                do
                {
                    trans.position += new Vector3(0, minMove * Sign(vSpeed));
                }
                while (!col.PlaceMeeting(trans.position.x, trans.position.y + minMove * Sign(vSpeed), 0));
            }

            //Cease VSpeed
            if (col.PlaceMeeting(trans.position.x, trans.position.y + minMove * Sign(vSpeed), 0))
            {
                vSpeed = 0;

                //Play Collision Actions
                OnCollisionActions();
            }
        }
	}

    public float Sign(float input)
    {
        if (input > 0) return 1;
        else if (input < 0) return -1;
        else return 0;
    }

    private void OnCollisionActions()
    {
       //Self Destruction
       if(DestroyOnContact)
       {
           SetToDestroy = true;
       }
    }

    public bool PlaceMeeting(float xPos, float yPos, int collisionType)
    {
        //Call PlaceMeeting From Child Collider
        //(Used to make reference to collider unnecessary)
        return col.PlaceMeeting(xPos, yPos, collisionType);
    }
}
