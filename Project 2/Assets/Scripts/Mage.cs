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

    public override void Update()
    {
        base.Update();
        if (Input.GetKey(KeyCode.Space))
        {
            currentTarget = currentTile.occupant;
            Kill(currentTarget);
        }
    }
    public override void ActivateSpecial()
    {
        int randX = Random.Range(0, World.Instance().mapSize.x), randY = Random.Range(0, World.Instance().mapSize.y);
        bool blunk = false;
        
        while(blunk == false)
        {
            randX = Random.Range(0, World.Instance().mapSize.x);
            randY = Random.Range(0, World.Instance().mapSize.y);
            if (World.Instance().GetTileFromCoords(new Point(randX, randY)).occupant == null)
            {
                World.Instance().WarpUnit(base.currentTile.occupant, new Point(randX, randY)); // Flash away from enemies
                World.Instance().Select(new Point(randX, randY));
                print("Blink!");
                blunk = true;
            }
        }
    }
}
