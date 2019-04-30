using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRay : Tower
{

    //Reference Variables
    public Sprite HeadSprite;
    GameObject LaserHead;
    public Sprite LaserSprite;
    GameObject LaserBeam;
    float LaserBeamLength;

    //Tower Base Variables
    public Vector2 HeadPos;
    bool PrevTargetDetected = false;

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

        //Create Laser Head
        LaserHead = Instantiate(new GameObject());
        LaserHead.AddComponent<SpriteRenderer>();
        LaserHead.GetComponent<SpriteRenderer>().sprite = HeadSprite;
        LaserHead.transform.position = transform.position + new Vector3(HeadPos.x, HeadPos.y, 0);
        Children.Add(LaserHead);

        //Create Laser Beam
        LaserBeam = Instantiate(new GameObject());
        LaserBeam.AddComponent<SpriteRenderer>();
        LaserBeam.GetComponent<SpriteRenderer>().sprite = LaserSprite;
        LaserBeamLength = LaserBeam.GetComponent<SpriteRenderer>().bounds.size.x;
        LaserBeam.transform.position = transform.position + new Vector3(HeadPos.x, HeadPos.y, 0);
        Children.Add(LaserBeam);

        //Color Children
        SetColor(sr.color);
    }

    // Update is called once per frame
    public override void Update()
    {
        //Run Parent Update
        base.Update();


        //Control Turret Head Aiming
        if (target != null && Collider.TransDist(trans.position, target.GetComponent<Transform>().position) <= Range)
        {
            //Head 1
            LaserHead.transform.eulerAngles = new Vector3(0, 0, (Mathf.Atan2(target.transform.position.y - LaserHead.transform.position.y,
            target.transform.position.x - LaserHead.transform.position.x) / Mathf.PI) * 180);

            //Laser 1
            LaserBeam.transform.eulerAngles = LaserHead.transform.eulerAngles;

            //Set Laser Length
            LaserBeam.transform.localScale = new Vector3(Collider.TransDist(target.transform.position, LaserBeam.transform.position) / LaserBeamLength, 1, 1);
        }
        //Default Lasers 1
        else
        {
            //Default Laser Length
            LaserBeam.transform.localScale = new Vector3(0f, 0f, 0f);
        }

        //Fire At Target 
        if (target != null)
        {
            //Reset Attack Alarm
            if(!PrevTargetDetected)
            {
                AttackAlarm = AttackTime;
            }

            //Check If Target Is Within Range
            if (Collider.TransDist(trans.position, target.GetComponent<Transform>().position) <= Range)
            {
                //Attack Target
                if (AttackAlarm <= 0)
                {
                    target.GetComponent<Enemy>().health--;
                    AttackAlarm = AttackTime;
                }
            }
            //Default Lasers 2
            else
            {
                //Default Laser Length
                LaserBeam.transform.localScale = new Vector3(0f, 0f, 0f);
            }

        }

        //Set Previous Variables
        if (target != null)
        {
            PrevTargetDetected = Collider.TransDist(trans.position, target.GetComponent<Transform>().position) <= Range;
        }
        else PrevTargetDetected = false;
        
    }//Update 

    private void OnDestroy()
    {
        //Destroy Extra Tower Components
        foreach(GameObject element in Children)
        {
            Destroy(element);
        }
    }
}
