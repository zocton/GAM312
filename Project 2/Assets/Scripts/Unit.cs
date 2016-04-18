﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Unit : MonoBehaviour {

    public Tile currentTile;

    private Vector3 lastPosition;
    private float interpolationParam = 0f;

    public int hp = 10, moveStat = 3;
    public string unitName = "Unit";

    private List<Point> navPoints = new List<Point>();
    
	// Use this for initialization
	public virtual void Start ()
    {
        currentTile = World.Instance().GetTileFromPosition(transform.position);
        World.Instance().WarpUnit(this, GetCoords());
        lastPosition = transform.position;
	}

    public Point GetCoords()
    {
        if(currentTile == null)
        {
            throw new System.Exception("Invalid tile reference");
        }

        return currentTile.coords;
    }
    
    public void OnMouseUpAsButton()
    {
        if (Input.GetMouseButtonUp(0))
        {
            World.Instance().Select(GetCoords());
        }

        if (Input.GetMouseButtonUp(1))
        {
            World.Instance().MoveTo(GetCoords());
        }
    }

    public bool IsMoving()
    {
        return (navPoints.Count > 0);
    }

    public float NLerp(float from, float to, float t)
    {
        //t = 4 * Mathf.Pow(t - 0.5f, 3f) + 0.5f;
        t = 0.36f * Mathf.Atan(10f * (t - 0.5f)) + 0.5f;
        return from + (to - from) * t;
        //return from * (1.0f - t) + to * t;
    }

    public Vector3 VectorInterpolate(Vector3 from, Vector3 to, float t)
    {
        return new Vector3(NLerp(from.x, to.x, t), NLerp(from.y, to.y, t), NLerp(from.z, to.z, t));
    }

    public void BeginInterpolatedMove(Point coords)
    {
        lastPosition = transform.position;
        interpolationParam = 0.0f;
    }

    public void NavigateTo(Point coords)
    {
        navPoints = GetComponent<Navigator>().ComputePath(GetCoords(), coords);
    }
	// Update is called once per frame
	void Update ()
    {
        if(interpolationParam <= 1.0f && navPoints.Count > 0)
        {
            // Get the first emelent of navPoints
            Point nextPoint = navPoints[0];

            // Use that first element as destination
            Vector3 destination = World.Instance().GetPositionFromCoords(nextPoint);
            
            transform.position = VectorInterpolate(lastPosition, destination, interpolationParam);
            interpolationParam += Time.deltaTime;

            // If interpolationParam is >= 1f, it's time to go to the next navPoint element
            if(interpolationParam >= 1.0f)
            {
                lastPosition = World.Instance().GetPositionFromCoords(nextPoint);
                navPoints.RemoveAt(0);
                interpolationParam = 0.0f;
            }
        }
        
	}
/*
    void GrabPath()
    {
        Navigator.ComputePath(GetCoords(), World.Instance().get)
    }
*/
    public virtual void ActivateSpecial()
    {

    }
}
