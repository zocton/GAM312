using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {

	public Tile currentTile;

	// Use this for initialization
	void Start () {
		currentTile = World.Instance ().GetTileFromPosition (transform.position);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
