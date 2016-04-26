using UnityEngine;
using System.Collections;

public class Fighter : Unit {

	// Use this for initialization
	override public void Start () {
		base.Start ();

		unitName = "Fighter";
		print (unitName + " spawned at " + GetCoords());
	}
	
	override public void ActivateSpecial() {
		print ("Beefing up!");
	}

}
