using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

    public Point coords = Point.zero;
    private Renderer rend;
    public int moveCost;
    public bool passability;
    private Color preservative;
    public bool doNotHover;

	// Use this for initialization
	void Start () {
        rend = GetComponent<Renderer>();
        doNotHover = false;
	}

    void OnMouseUpAsButton()
    {
        doNotHover = true;
        rend.material.color = new Color(0f,0f,0f);
    }

    void OnMouseEnter()
    {
        if (!doNotHover)
        {
            preservative = rend.material.color;
            rend.material.color = new Color(255f, 0f, 150f);
        }
    }

    void OnMouseExit()
    {
        if (!doNotHover)
        {
            rend.material.color = preservative;
        }
    }
    // Update is called once per frame
    void Update () {
	
	}

    public void SetMaterial(Material mat)
    {
        if (rend == null)
        {
            rend = GetComponent<Renderer>();
        }
        rend.material = mat;
    }
}
