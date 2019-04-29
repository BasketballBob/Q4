using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    //Reference Variables
    Transform trans;
    SpriteRenderer sr;

    //Particle Variables
    public float LifeAlarm = 1;
    public float vSpeed;
    public float hSpeed;
    public float Gravity;
    public float SpinSpeed;
    public float SpinRate;

    //Define Reference Variables
    private void OnEnable()
    {
        trans = GetComponent<Transform>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Gravity 
        vSpeed -= Gravity * Time.deltaTime;

        //Move Particle 
        trans.position += new Vector3(hSpeed, vSpeed) * Time.deltaTime;

        //Rotate Particle 
        SpinSpeed += SpinRate * Time.deltaTime;
        trans.eulerAngles += new Vector3(0, 0, SpinSpeed) * Time.deltaTime;

        //Deduct LifeAlarm
        if (LifeAlarm - Time.deltaTime > 0)
        {
            LifeAlarm -= Time.deltaTime;
        }
        else Destroy(gameObject);
    }
}
