using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : Tower
{

    //Reference Variables
    Collider Collider2;
    Animator anim;

    //Tower Base Variables
    bool ColliderAdded = false;
    [SerializeField] int HealthCount = 3;
    [SerializeField] float Health;
    float HealthAmount = 4f; //Time in seconds of destruction
    float HealthRiseRate = .5f; //Multiplier
    float ShakeAlarm;
    float ShakeTime = .2f;
    float ShakeAngle = 1;
    int ShakeSide = 1;

    //Particle Variables
    public Sprite TopParticle;
    public Sprite MidParticle;
    public Sprite BottomParticle;
    int ParticleCount = 3;
    

    //Define Reference Variables
    public override void OnEnable()
    {
        //Run Parent Event
        base.OnEnable();

        anim = GetComponent<Animator>();
    }

    //Add Additional Components
    public override void Start()
    {
        //Run Parent Start
        base.Start();

        //Set Initial Health
        Health = HealthAmount * (float)HealthCount;

        //Color Children
        SetColor(sr.color);
    }

    // Update is called once per frame
    public override void Update()
    {
        //Run Parent Update
        base.Update();

        //Add Wall Collider Component (if activated)
        if(Activated && !ColliderAdded)
        {
            Collider2 = gameObject.AddComponent<Collider>();
            Collider2.CollisionType = 4;
            Collider2.width = 1;
            Collider2.height = 3;
            ColliderAdded = true;

            //Kill All Enemies Inside
            int KillCap = 0; //Ensures Loop Isn't Infinite
            while(Collider2.PlaceMeeting(trans.position.x, trans.position.y, 2) && KillCap < 20)
            {
                Destroy(col.NearestCollider(trans.position.x, trans.position.y, 2));
                KillCap++;
            }
        }

        //Fire At Target 
        if (target != null)
        {

            //Check If Target Is Within Range
            /*if (Collider.TransDist(trans.position, target.GetComponent<Transform>().position) <= Range)
            {
                //Attack Target
                if (AttackAlarm <= 0)
                {

                }
            }*/
        }

        //Detect If Enemy Is Colliding With Wall
        if (Collider2 != null)
        {
            if (Collider2.PlaceMeeting(trans.position.x - PhysicsObject.minMove, trans.position.y, 2))
            {
                //Shake Tower 
                if (ShakeAlarm - Time.deltaTime > 0)
                {
                    ShakeAlarm -= Time.deltaTime;
                }
                else
                {
                    ShakeSide *= -1;
                    ShakeAlarm = ShakeTime;
                }

                //Set Shake Angle
                trans.eulerAngles = new Vector3(0, 0, ShakeAngle * ShakeSide);

                //Deduct From Health 
                if (Health - Time.deltaTime > 0)
                {
                    Health -= Time.deltaTime;
                }
                else Health = 0;

                //Deduct From Health Count
                if ((HealthCount - 1) * HealthAmount >= Health)
                {
                    HealthCount -= 1;
                }
            }
        }
        else
        {
            //Reset Angle
            trans.eulerAngles = new Vector3(0, 0, 0);

            //Rise Health
            if (Health + HealthRiseRate * Time.deltaTime < HealthCount * (float)HealthAmount)
            {
                Health += HealthRiseRate * Time.deltaTime;
            }
            else Health = HealthCount * (float)HealthAmount;
        }

        //Destroy Tower If Broken
        if(HealthCount <= 0)
        {
            Destroy(gameObject);
        }

        //Spawn Block Particles
        if(HealthCount < ParticleCount)
        {
            //Create Empty GameObject
            GameObject tvParticle = Instantiate(new GameObject());
            tvParticle.AddComponent<SpriteRenderer>();
            tvParticle.AddComponent<Particle>();

            //Set Particle Variables
            tvParticle.GetComponent<Particle>().vSpeed = 4;
            tvParticle.GetComponent<Particle>().hSpeed = .5f;
            tvParticle.GetComponent<Particle>().Gravity = 9;
            tvParticle.GetComponent<Particle>().LifeAlarm = 5;
            tvParticle.GetComponent<Particle>().SpinRate = -20;

            //Spawn Particle
            if (ParticleCount == 1)
            {
                tvParticle.GetComponent<SpriteRenderer>().sprite = BottomParticle;
                tvParticle.transform.position = transform.position + new Vector3(-.08f, -1.29f, 0);
            }
            else if(ParticleCount == 2)
            {
                tvParticle.GetComponent<SpriteRenderer>().sprite = MidParticle;
                tvParticle.transform.position = transform.position + new Vector3(0.15f, -0.21f, 0);
            }
            else if(ParticleCount == 3)
            {
                tvParticle.GetComponent<SpriteRenderer>().sprite = TopParticle;
                tvParticle.transform.position = transform.position + new Vector3(-0.16f, .87f, 0);
            }

            //Deduct Particle Count
            ParticleCount--;

        }

        //Set Wall Sprites
        if (HealthCount == 3) anim.Play("WallPhase3");
        else if (HealthCount == 2) anim.Play("WallPhase2");
        else if (HealthCount == 1) anim.Play("WallPhase1");

      
    }//Update 

    private void OnDestroy()
    {
        //Destroy Extra Tower Components

    }
}
