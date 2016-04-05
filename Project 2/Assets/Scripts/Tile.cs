using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {
    /*
        public Point coords = Point.zero;
        private Renderer rend;
        public int moveCost;
        public bool passability = false;
        private Color preservative;
        public bool doNotHover;
        public Unit occupant = null;

        // Use this for initialization
        void Start () {
            rend = GetComponent<Renderer>();
            doNotHover = false;
        }

        void OnMouseUpAsButton()
        {
            doNotHover = true;
            rend.material.color = new Color(0f,0f,0f);
            World.Instance().moveCostText.text = "Currently Moving to this tile";
        }

        void OnMouseEnter()
        {
            if (!doNotHover)
            {
                preservative = rend.material.color;
                SetColor(new Color(5f, 0f, 0f));
                World.Instance().moveCostText.text = this.moveCost.ToString();
            }
        }

        void OnMouseExit()
        {
            if (!doNotHover)
            {
                ResetColor();
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

        public void SetColor(Color color)
        {
            rend.material.color = color;
        }

        public void ResetColor()
        {
            rend.material.color = preservative;
        }
    */
    public Unit occupant = null;
    public Point coords = Point.zero;
    public int moveCost = 1;
    public bool impassable = false;

    private Renderer rend;
    private Color originalColor;

    // Use this for initialization
    void Start()
    {
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetMaterial(Material mat)
    {
        if (rend == null)
            rend = GetComponent<Renderer>();
        rend.material = mat;
    }

    public void SetColor(Color color)
    {
        rend.material.color = color;
    }

    public void ResetColor()
    {
        rend.material.color = originalColor;
    }

    void OnMouseUpAsButton()
    {
        if (Input.GetMouseButtonUp(0))
        {
            World.Instance().Select(coords);
        }

        if (Input.GetMouseButtonUp(1))
        {
            World.Instance().MoveTo(coords);
        }
        
    }

    void OnMouseEnter()
    {
        if (!World.Instance().IsSelected(this))
            SetColor(Color.red);
    }

    void OnMouseExit()
    {
        if (!World.Instance().IsSelected(this))
            ResetColor();
    }
}
