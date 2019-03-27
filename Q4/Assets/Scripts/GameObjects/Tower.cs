﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

    //Reference Variables
    Transform trans;
    Collider col;
    GameObject target;
    public GameObject projectile;

    //Tower Variables
    public Vector3 ShotOffset = new Vector3(0, 2.5f);
    public float Range = 15;
    public float AttackTime = 1f;
    float AttackAlarm;
    public float BulletSpeed = 20f;

    //Define Reference Variables
    private void OnEnable()
    {
        trans = GetComponent<Transform>();
        col = gameObject.AddComponent<Collider>();
        col.CollisionType = 3;
    }

    // Update is called once per frame
    void Update () {

        //Detect Closest Target
        target = col.NearestCollider(2);

        //Fire At Target 
        if (target != null)
        {

            if (Collider.TransDist(trans.position+ShotOffset, target.GetComponent<Transform>().position) <= Range)
            {

                //Deduct Attack Alarm
                if (AttackAlarm - Time.deltaTime > 0)
                {
                    AttackAlarm -= Time.deltaTime;
                }
                //Attack Target
                else
                {
                    //Fire Weapon
                    GameObject tvObj = Instantiate(projectile, trans.position + ShotOffset, Quaternion.identity);
                    tvObj.GetComponent<PhysicsObject>().Gravity = false;

                    //Calculate Trajectory
                    float xDist = target.GetComponent<Transform>().position.x - (trans.position.x + ShotOffset.x);
                    float yDist = target.GetComponent<Transform>().position.y - (trans.position.y + ShotOffset.y);
                    float initialMag = Mathf.Sqrt(Mathf.Pow(xDist, 2) + Mathf.Pow(yDist, 2));
                    float initalHSpeedRatio = xDist / (Mathf.Abs(xDist) + Mathf.Abs(yDist));
                    float initalVSpeedRatio = yDist / (Mathf.Abs(xDist) + Mathf.Abs(yDist));

                    tvObj.GetComponent<PhysicsObject>().hSpeed = BulletSpeed *
                    initalHSpeedRatio * (initialMag / Collider.TransDist(trans.position + ShotOffset, target.GetComponent<Transform>().position));
                    tvObj.GetComponent<PhysicsObject>().vSpeed = BulletSpeed *
                    initalVSpeedRatio * (initialMag / Collider.TransDist(trans.position + ShotOffset, target.GetComponent<Transform>().position));

                    //Reset Alarm
                    AttackAlarm = AttackTime;
                }
            }

        }
	}
}