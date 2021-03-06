﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    //Reference Variables
    Transform trans;
    Collider co;
    PhysicsObject po;

    //Projectile Variable
    public bool ProjectileTilt = false;
    public bool Invulnerable = false;
    int damage = 1;
    float lifeAlarm = 3f;
    bool DestroySelf = false;

    //Define Reference Variables
    public virtual void OnEnable()
    {
        trans = GetComponent<Transform>();
        co = GetComponent<Collider>();
        po = GetComponent<PhysicsObject>();
    }

    // Update is called once per frame
    public virtual void Update () {

        //Destroy Self One Fram Later
        if (DestroySelf) Destroy(gameObject);

        //Set Collider If Missing
        if(co == null) co = GetComponent<Collider>();

        //Set Direction Based On Trajectory 
        if (ProjectileTilt)
        {
            trans.eulerAngles = new Vector3(0, 0, (Mathf.Atan2(po.vSpeed, po.hSpeed) / Mathf.PI) * 180);
        }

        //Destroy On Enemy Contact 
        if(co.PlaceMeeting(trans.position.x, trans.position.y, 2) && !Invulnerable)
        {
            //Deduct Enemy Health
            GameObject tvInst = co.InstanceMeeting(trans.position.x, trans.position.y, 2);
            tvInst.GetComponent<Enemy>().health -= damage;

            //Destroy Self
            DestroySelf = true;
        }

        //Countdown Life Alarm
        if (!Invulnerable)
        {
            if (lifeAlarm - Time.deltaTime > 0)
            {
                lifeAlarm -= Time.deltaTime;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
	}
}
