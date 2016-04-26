using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Unit : MonoBehaviour {

	public Tile currentTile;

	public string unitName = "Unit";
	public int hp = 10;
	public int moveStat = 3;

	private Vector3 lastPosition;
	private float interpolationParam = 0.0f;

	private List<Point> navPoints = new List<Point>();
	public GameObject navPointIndicatorPrefab;
	private List<GameObject> navPointIndicators = new List<GameObject>();

	// Use this for initialization
	virtual public void Start () {
		currentTile = World.Instance ().GetTileFromPosition (transform.position);
		World.Instance ().WarpUnit (this, GetCoords ());
		lastPosition = transform.position;
	}

	public void DestroyNavIndicators() {
		foreach(GameObject obj in navPointIndicators) {
			Destroy (obj);
		}
		navPointIndicators.Clear ();
	}

	public void DestroyNextNavIndicator() {
		if (navPointIndicators.Count > 0) {
			Destroy(navPointIndicators[0]);
			navPointIndicators.RemoveAt (0);
		}
	}

	public void ResetNavIndicators() {
		DestroyNavIndicators ();

		if (navPoints == null)
			return;

		foreach (Point p in navPoints) {
			Vector3 pos = World.Instance ().GetPositionFromCoords(p);
			navPointIndicators.Add ((GameObject) Instantiate(navPointIndicatorPrefab, pos, navPointIndicatorPrefab.transform.rotation));
		}
	}

	public bool NavigateTo(Point coords) {
		navPoints = GetComponent<Navigator> ().ComputePath (GetCoords (), coords);
		ResetNavIndicators ();
		return (navPoints != null && navPoints.Count > 0);
	}

	public void BeginInterpolatedMove (Point coords) {

		lastPosition = transform.position;
		interpolationParam = 0.0f;
	}

	public bool IsMoving() {
		return (navPoints != null && navPoints.Count > 0);
	}

	// Update is called once per frame
	void Update () {
		if (interpolationParam <= 1.0f && navPoints != null && navPoints.Count > 0) {
			// Get the first element of navPoints
			//  Use that element as destination
			Vector3 destination = World.Instance ().GetPositionFromCoords (navPoints[0]);
			transform.position = Vector3.Lerp (lastPosition, destination, interpolationParam);

			float interpolationSpeed = 3.0f;
			interpolationParam += interpolationSpeed*Time.deltaTime;

			if(interpolationParam >= 1.0f) {
				lastPosition = World.Instance ().GetPositionFromCoords (navPoints[0]);
				navPoints.RemoveAt (0);
				DestroyNextNavIndicator();
				interpolationParam = 0.0f;
				// If interpolationParam is >= 1.0f, it's time to go to the next navPoint element
			}
		}
	}

	public Point GetCoords() {
		if (currentTile == null)
			return new Point (-1, -1);

		return currentTile.coords;
	}
	
	public void OnMouseUpAsButton() {
		if (Input.GetMouseButtonUp (0))
			World.Instance ().Select (GetCoords ());
		if (Input.GetMouseButtonUp (1))
			World.Instance ().MoveTo (GetCoords ());
	}
	
	virtual public void ActivateSpecial() {

	}
}
