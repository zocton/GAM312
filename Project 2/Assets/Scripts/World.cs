using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public struct Point
{
    public int x, y;

    public static Point zero = new Point(0, 0);

    public Point(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public static Point operator +(Point a, Point b)
    {
        return new Point(a.x + b.x, a.y + b.y);
    }

    public static Point operator -(Point a, Point b)
    {
        return new Point(a.x - b.x, a.y - b.y);
    }

    public override string ToString()
    {
        return "(" + x.ToString() + ", " + y.ToString() + ")";
    }
}

public class World : MonoBehaviour {

    public Tile grassTile;
    public Tile sandTile;
    public Tile grassrockTile;
    public Tile mudrockTile;
    public Tile fossilTile;
    public Tile cliffrockTile;
    public Tile snowrockTile;
    public Tile selectedTile = null;

    public Point mapSize = new Point(50, 50);
    private Tile[,] map;
    public GameObject tile;
    public Text moveCostText;

    public UIController uic;

    private GameObject lastMouseHit = null;

    private static World instance = null;

    public static World Instance()
    {
        return instance;
    }

    private void GenerateMap()
    {
        map = new Tile[mapSize.x, mapSize.y];
        for(int x = 0; x <= mapSize.x-1; x++)
        {
            for (int y = 0; y <= mapSize.y - 1; y++)
            {
                Tile prefab;
                int temp = Random.Range(0, 7);
                switch (temp)
                {
                    case 0:
                        prefab = grassTile;
                        break;
                    case 1:
                        prefab = sandTile;
                        break;
                    case 2:
                        prefab = grassrockTile;
                        break;
                    case 3:
                        prefab = mudrockTile;
                        break;
                    case 4:
                        prefab = fossilTile;
                        break;
                    case 5:
                        prefab = cliffrockTile;
                        break;
                    case 6:
                        prefab = snowrockTile;
                        break;
                    default:
                        prefab = grassTile;
                        break;
                }
                GameObject g = (GameObject)Instantiate(prefab.gameObject, new Vector3(x, 0, y), Quaternion.identity);
                map[x, y] = g.GetComponent<Tile>();
                map[x, y].coords = new Point(x, y); 
            }
        }
    }

    public Point GetCoordsFromPosition(Vector3 position)
    {
        int x = Mathf.RoundToInt(position.x), y = Mathf.RoundToInt(position.z);
        return new Point(x, y);
    }

    public Vector3 GetPositionFromCoords(Point coords)
    {
        float x = coords.x;
        float z = coords.y;
        return new Vector3(x, 1.0f, z);  // Assuming 1.0f to be the proper height (it may not be!)
    }

    public Tile GetTileFromPosition(Vector3 position)
    {
        return GetTileFromCoords(GetCoordsFromPosition(position));
    }

    public Tile GetTileFromCoords(Point coords)
    {
        if (coords.x < 0 || coords.x >= mapSize.x || coords.y < 0 || coords.y >= mapSize.y)
        {
            return null;
        }
        return map[coords.x, coords.y];
    }
    
    public void Select(Point coords) {
		Tile lastSelection = selectedTile;

		// Reset old selected tile
		if (selectedTile != null) {
			selectedTile.ResetColor ();
			selectedTile = null;

            UpdateUI();
		}

		// Ensure we're within the bounds of the tile map
		if (coords.x < 0 || coords.x >= mapSize.x || coords.y < 0 || coords.y >= mapSize.y)
			return;
		
		// Cancel selection if it was already selected
		if (lastSelection == map [coords.x, coords.y])
			return;

		// Select the tile
		selectedTile = map[coords.x, coords.y];
		selectedTile.SetColor (new Color(0,50,50,1));

        UpdateUI();
	}

    public bool IsSelected(Tile tile)
    {
        return (tile == selectedTile);
    }

    public void UpdateUI()
    {
        if(selectedTile != null)
        {
            uic.SetUnitInfo(selectedTile.occupant);
        }
    }
    
    public void ActivateSpecial()
    {
        if (selectedTile == null || selectedTile.occupant == null)
        {
            return;
        }

        selectedTile.occupant.ActivateSpecial();
    }

    public void WarpUnit(Unit unit, Point coords)
    {
        Tile t = GetTileFromCoords(coords);
        if(t == null || t.occupant != null)
        {
            return;
        }

        // Remove from old tile
        Tile oldTile = unit.currentTile;
        if(oldTile != null)
        {
            oldTile.occupant = null;
        }

        // Add to the new tole
        unit.currentTile = t;
        t.occupant = unit;
        unit.transform.position = GetPositionFromCoords(coords);
    }

    public void MoveTo(Point coords)
    {
        if(selectedTile == null || selectedTile.occupant == null)
        {
            return;
        }

        WarpUnit(selectedTile.occupant, coords);
        Select(coords);
    }

    void Awake()
    {
        GenerateMap();
        instance = this;
    }
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                lastMouseHit = hit.transform.gameObject;

                if (lastMouseHit != null)
                {
                    lastMouseHit.SendMessage("OnMouseDown", null, SendMessageOptions.DontRequireReceiver);
                }
            }
        }

        if (Input.GetMouseButtonUp(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject previousHit = lastMouseHit;
                lastMouseHit = hit.transform.gameObject;

                if (lastMouseHit != null)
                {
                    if(lastMouseHit == previousHit)
                    {
                        lastMouseHit.SendMessage("OnMouseUpAsButton", null, SendMessageOptions.DontRequireReceiver);
                    }
                    else
                    {
                        lastMouseHit.SendMessage("OnMouseDown", null, SendMessageOptions.DontRequireReceiver);
                    }
                }
            }
        }
	}
}