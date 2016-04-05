using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {

    public Tile currentTile;

    public int hp = 10, moveStat = 3;
    public string unitName = "Unit";
    
	// Use this for initialization
	public virtual void Start ()
    {
        currentTile = World.Instance().GetTileFromPosition(transform.position);
        World.Instance().WarpUnit(this, GetCoords());
	}

    public Point GetCoords()
    {
        if(currentTile == null)
        {
            return new Point(-1, -1);
            throw new System.Exception("Invalid tile reference");
        }

        return currentTile.coords;
    }
    
	// Update is called once per frame
	void Update ()
    {
	
	}

    public virtual void ActivateSpecial()
    {

    }
}
