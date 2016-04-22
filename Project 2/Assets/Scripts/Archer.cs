using UnityEngine;
using System.Collections;

public class Archer : Unit {

    // Use this for initialization
    public override void Start()
    {
        base.Start();

        unitName = "Archer";
        print(unitName + " spawned here.");
    }
    

    public override void ActivateSpecial()
    {
        if(CanAttack(currentTarget))
        {
            TileNode distance = new TileNode(GetCoords(), null, currentTarget.GetCoords()); // Hold the distance between this unit and the target unit

            // Constraints for different range arrow volleys
            if(distance.GetDistance() >= 2f && distance.GetDistance() <= 5f)
            {
                attackPowerSpecial = 5; // Mid range
            }
            else if(distance.GetDistance() < 2f)
            {
                attackPowerSpecial = 3; // Close range
            }
            else if(distance.GetDistance() > 5f)
            {
                attackPowerSpecial = 7; // Longshot
            }
        }
        print("Volley!");
    }
}
