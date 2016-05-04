using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Unit : MonoBehaviour {

    public Tile currentTile;

    private Vector3 lastPosition;
    private float interpolationParam = 0f;

    // Unit info for fighting
    public int hp = 10, moveStat = 3, defense = 5, attackPower = 5, attackPowerSpecial = 0;
    public string unitName = "Unit";

    public Unit currentTarget;

    private List<Point> navPoints = new List<Point>();

    public bool isAttackable; // Still needs to be implemented properly

    public Animator animator;
    
	// Use this for initialization
	public virtual void Start ()
    {
        currentTile = World.Instance().GetTileFromPosition(transform.position);
        World.Instance().WarpUnit(this, GetCoords());
        lastPosition = transform.position;
        GetComponent<Animator>().Play("Idle");
	}

    public void Idle()
    {
        animator.SetBool("Idle", true);
    }

    public void StartWalking()
    {
        animator.SetBool("StartWalking", true);
    }

    public void StartAttacking()
    {
        animator.SetBool("Attack", true);
    }

    public void TakeDamage()
    {
        animator.SetBool("Damage", true);
    }
    /*
     * GetDistance() returns the distance (either manhattan or chessboard) to a target coordinate.
     *
     * CanAttack() returns whether or not the occupant of the target coordinates can be attacked by this Unit.
     *
     * Attack() performs the calculations to make a Unit attack another.
     *
     * Hit() performs the calculations for a Unit to take damage (and perhaps die).
     *
     */

    public void GetHit()
    {
        TakeDamage();
    }
    // Method for checking if a target can be attacked
    public bool CanAttack(Unit target)
    {
        currentTarget = target;
        if(target != null)
        {
            return (target.isAttackable);
        }
        else
        {
            return false;
        }
    }

    // Method for attacking other units
    public void Attack(Unit target)
    {
        //  If the target is attackable apply hit() function
        if (CanAttack(target))
        {
            Hit(target);
        }
        StartAttacking();
    }

    // Method for appying hit damage
    public void Hit(Unit target)
    {
        // Make sure there is a target
        if (target != null)
        {
            target.hp -= Mathf.Abs((target.defense - this.attackPower) + this.attackPowerSpecial);

            if (target.hp <= 0)
            {
                Kill(target);
            }
        }
    }

    // Method for killing a unit
    public void Kill(Unit target)
    {
        // Make sure there is a target
        if (target != null)
        {
            GameObject.DestroyObject(target.gameObject);
        }
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
        return (navPoints != null && navPoints.Count > 0);
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
    /*
    public void NavigateTo(Point coords)
    {
        navPoints = GetComponent<Navigator>().ComputePath(GetCoords(), coords);
    }
    */
    public bool NavigateTo(Point coords)
    {
        navPoints = GetComponent<Navigator>().ComputePath(GetCoords(), coords);
        //ResetNavIndicators();
        return (navPoints != null && navPoints.Count > 0);
    }
    // Update is called once per frame
    public virtual void Update ()
    {
        //Idle();
        if (interpolationParam <= 1.0f && navPoints != null && navPoints.Count > 0)
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

                if (navPoints.Count > 0) {
                    StartWalking();
                    
                    // Rotate your character approriately
                    Vector3 relativePos = World.Instance().GetPositionFromCoords(navPoints[0]) - transform.position;
                    transform.rotation = Quaternion.LookRotation(relativePos);
                }
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
