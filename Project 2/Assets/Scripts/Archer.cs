﻿using UnityEngine;
using System.Collections;

public class Archer : Unit {

    // Use this for initialization
    public override void Start()
    {
        base.Start();

        unitName = "Archer";
        print(unitName + " spawned here.");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void ActivateSpecial()
    {
        print("Volley!");
    }
}
