using UnityEngine;
using System.Collections;

public class Mage : Unit {

	// Use this for initialization
	override public void Start () {
		base.Start ();
		
		unitName = "Mage";
		print (unitName + " spawned at " + GetCoords());
	}
	
	override public void ActivateSpecial() {
		print ("Random teleport!");
	}
}
