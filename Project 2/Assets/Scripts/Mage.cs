using UnityEngine;
using System.Collections;

public class Mage : Unit {

    // Use this for initialization
    public override void Start()
    {
        base.Start();

        unitName = "Mage";
        print(unitName + " spawned here.");
    }

    public override void ActivateSpecial()
    {
        World.Instance().WarpUnit(this, GetCoords() + new Point(5, 5)); // Flash away from enemies
        print("Blink!");
    }
}
