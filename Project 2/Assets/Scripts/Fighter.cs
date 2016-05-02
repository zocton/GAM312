using UnityEngine;
using System.Collections;

public class Fighter : Unit {

    private int buffTimer = 0;
	// Use this for initialization
	public override void Start () {
        base.Start();

        unitName = "Fighter";
        print(unitName + " spawned here.");
	}

    public override void Update()
    {
        base.Update();

        if (attackPowerSpecial == 0)
        {
            attackPowerSpecial += (int)Time.deltaTime;
        }
    }

    public override void ActivateSpecial()
    {
        if (CanAttack(currentTarget))
        {
            attackPowerSpecial = 0;
            while(buffTimer <= 5)
            {
                attackPowerSpecial = 7; // Have a beefeing up enrage timer for five seconds
            }
            attackPowerSpecial = 0;
        }
        print("Beefing up!");
    }
}
