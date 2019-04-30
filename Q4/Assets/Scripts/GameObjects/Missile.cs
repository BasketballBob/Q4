using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Projectile
{
    //Missile References
    public Transform Target;
    Vector3 TargetPos;

    //Missile Variables
    int VDirection = 1;
    float vSpeed = 0f;
    float flyRate = 10f;
    float flyCap = 20f;
    float TurnHeight = 20f;
    float TargetXOffSet = 5f;
    bool FollowTarget = false;

    public override void OnEnable()
    {
        //Run Parent Event
        base.OnEnable();
        
    }

    // Start is called before the first frame update
    public void Start()
    {        
        //Make Missile Invulnerable
        GetComponent<Projectile>().Invulnerable = true;
    }

    // Update is called once per frame
    public override void Update()
    {
        //Run Parent Event
        base.Update();

        //Increment Speed
        vSpeed += flyRate*(float)VDirection * Time.deltaTime;

        //Set Physics Object Speed
        GetComponent<PhysicsObject>().vSpeed = vSpeed;

        //Turn Around Once High Enough
        if(VDirection == 1 && transform.position.y >= TurnHeight)
        {
            //Begin Heading Downwards
            VDirection = -1;
            vSpeed *= -1;

            //Set Target 
            Target = GetComponent<Collider>().NearestCollider(transform.position.x, transform.position.y, 2).transform;

            //Destroy Self If No Target
            if (Target == null)
            {
                Destroy(gameObject);
            }
            //Set Target pos
            else TargetPos = Target.transform.position + new Vector3(Random.Range(-TargetXOffSet, TargetXOffSet), 0);

            //Begin Following Target
            FollowTarget = true;

            //Make Missile Vulnerable
            GetComponent<Projectile>().Invulnerable = false;
        }

        //Follow Target
        if(FollowTarget && Target != null)
        {
            //Follow Target Horizontally
            transform.position = new Vector3(TargetPos.x, transform.position.y, transform.position.z);
        }
    }
}
