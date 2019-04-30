using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artillery : Tower
{

    //Reference Variables


    //Artillery Variables
    int VolleyPos = 0;
    public int VolleyCount = 4;
    public float VolleyRate = .25f;
    float VolleyAlarm;
    float ShotXOffSet = 1f;
    float ShotY = 0f;

    //Define Reference Variables
    public override void OnEnable()
    {
        //Run Parent Event
        base.OnEnable();
    }

    //Add Additional Components
    public override void Start()
    {
        //Run Parent Start
        base.Start();


        //Color Children
        SetColor(sr.color);
    }

    // Update is called once per frame
    public override void Update()
    {
        //Run Parent Update
        base.Update();

        //Specialized Code (Different for each turret)


        //Fire At Target 
        if (target != null)
        {

            //Check If Target Is Within Range
            if (Collider.TransDist(trans.position, target.GetComponent<Transform>().position) <= Range)
            {
                //Attack Target With Volley
                if (AttackAlarm <= 0)
                {
                    //Fire Volley
                    if(VolleyPos < VolleyCount)
                    {
                        //Deduct Volley Alarm
                        if(VolleyAlarm-Time.deltaTime > 0)
                        {
                            VolleyAlarm -= Time.deltaTime;
                        }
                        //Shoot Missile
                        else
                        {
                            //Instantiate Missile
                            GameObject tvObj = Instantiate(projectile);

                            //Set Missile Position
                            int ShotPos = VolleyPos % 3;
                            if (ShotPos == 0) tvObj.transform.position = new Vector3(trans.position.x-ShotXOffSet, trans.position.y+ShotY, tvObj.transform.position.z);
                            else if (ShotPos == 1) tvObj.transform.position = new Vector3(trans.position.x, trans.position.y+ShotY, tvObj.transform.position.z);
                            else if (ShotPos == 2) tvObj.transform.position = new Vector3(trans.position.x+ShotXOffSet, trans.position.y+ShotY, tvObj.transform.position.z);

                            //Manage Alarm
                            VolleyAlarm = VolleyRate;
                            VolleyPos++;
                        }
                    }

                    //Finish Volley
                    if(VolleyPos == VolleyCount)
                    {
                        VolleyPos = 0;
                        AttackAlarm = AttackTime;
                    }
                }
            }

        }
    }//Update 

    private void OnDestroy()
    {
        //Destroy Extra Tower Components

    }
}
