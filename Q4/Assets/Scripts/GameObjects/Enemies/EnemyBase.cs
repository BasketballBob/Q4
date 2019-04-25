﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : Enemy
{

    //Reference Variables


    //Enemy Base Variables


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

    }

    // Update is called once per frame
    public override void Update()
    {
        //Run Parent Update
        base.Update();

    }//UPDATE

    private void OnDestroy()
    {
        //Destroy Extra Enemy Components

    }
}