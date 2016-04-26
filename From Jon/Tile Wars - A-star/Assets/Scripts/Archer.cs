using UnityEngine;
using System.Collections;

public class Archer : Unit {

	// Use this for initialization
	override public void Start () {
		base.Start ();
		
		unitName = "Archer";
		print (unitName + " spawned at " + GetCoords());
	}
	
	override public void ActivateSpecial() {
		print ("Desperation barrage!");
	}
}
