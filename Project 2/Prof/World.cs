using UnityEngine;
using System.Collections;

[System.Serializable]
public struct Point {
	public int x;
	public int y;

	public static Point zero = new Point (0, 0);

	public Point(int x, int y) {
		this.x = x;
		this.y = y;
	}

	public static Point operator +(Point A, Point B) {
		return new Point (A.x + B.x, A.y + B.y);
	}
	
	public static Point operator -(Point A, Point B) {
		return new Point (A.x - B.x, A.y - B.y);
	}

	public override string ToString ()
	{
		return "(" + x.ToString () + ", " + y.ToString () + ")";
	}
}

public class World : MonoBehaviour {

	public Point mapSize = new Point(50,50);

	private Tile[,] map;

	public GameObject[] tiles;

	public Tile selectedTile = null;



	private static World instance = null;
	
	public static World Instance() {
		return instance;
	}

	// Use this for initialization
	void Awake () {
		instance = this;
		GenerateMap ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void GenerateMap() {
		map = new Tile[mapSize.x,mapSize.y];
		for(int x = 0; x<= mapSize.x-1; x++) {

			for(int y = 0; y<= mapSize.y-1; y++) {
				GameObject g = (GameObject)Instantiate(tiles[Random.Range(0,tiles.Length)], new Vector3(x,0,y),Quaternion.identity);
				Tile t = g.GetComponent<Tile>();
				t.coords = new Point(x, y);
				map[x,y] = t;
			}
		}
	}
	
	public Point GetCoordsFromPosition(Vector3 position) {
		int x = Mathf.RoundToInt (position.x);
		int y = Mathf.RoundToInt (position.z);
		return new Point(x, y);
	}
	
	public Vector3 GetPositionFromCoords(Point coords) {
		float x = coords.x;
		float z = coords.y;
		return new Vector3(x, 1.0f, z);  // Assuming 1.0f to be the proper height (it may not be!)
	}

	public Tile GetTileFromPosition(Vector3 position) {
		Point coords = GetCoordsFromPosition (position);
		if (coords.x < 0 || coords.x >= mapSize.x || coords.y < 0 || coords.y >= mapSize.y)
			return null;

		return map[coords.x, coords.y];
	}

	public void Select(Point coords) {
		Tile lastSelection = selectedTile;

		// Reset old selected tile
		if (selectedTile != null) {
			selectedTile.ResetColor ();
			selectedTile = null;
		}

		// Ensure we're within the bounds of the tile map
		if (coords.x < 0 || coords.x >= mapSize.x || coords.y < 0 || coords.y >= mapSize.y)
			return;
		
		// Cancel selection if it was already selected
		if (lastSelection == map [coords.x, coords.y])
			return;

		// Select the tile
		selectedTile = map[coords.x, coords.y];
		selectedTile.SetColor (Color.yellow);
	}

	public bool IsSelected(Tile tile) {
		return (tile == selectedTile);
	}
}
