using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class World : MonoBehaviour {

    public Tile grassTile;
    public Tile sandTile;
    public Tile grassrockTile;
    public Tile mudrockTile;
    public Tile fossilTile;
    public Tile cliffrockTile;
    public Tile snowrockTile;

    public Point mapSize = new Point(50, 50);
    private Tile[,] map;
    public GameObject tile;
    public Text moveCostText;

    private static World instance = null;

    public static World Instance()
    {
        return instance;
    }

    public Point GetCoordsFromPosition(Vector3 position) {
        int x = Mathf.RoundToInt(position.x), y = Mathf.RoundToInt(position.z);
        return new Point(x, y);
    }
    public Tile GetTileFromPosition(Vector3 position)
    {
        Point coords = GetCoordsFromPosition(position);

        if(coords.x < 0 || coords.x >= mapSize.x || coords.y < 0 || coords.y >= mapSize.y)
        {
            return null;
        }
        return map[coords.x, coords.y];
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
	
	}
}

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