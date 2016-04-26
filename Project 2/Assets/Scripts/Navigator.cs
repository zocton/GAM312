/*-----------PLAIN MOVEMENT-------------------
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class Navigator : MonoBehaviour {

    public static int Abs(int a) {
        return (a < 0 ? -a : a);
    }

    public static int Sign(int a) {
        return (a < 0 ? -1 : 1);
    }

	public List<Point> ComputePath(Point from, Point to) {
		List<Point> result = new List<Point>();
        // TODO: Basic pathing
        // Go one tile at a time (add the coords to the result list) to get to the destination.

        Point current = from;

        while(current.x != to.x || current.y != to.y)
        {
            Point seperation = to - current;
            if(Abs(seperation.x) > Abs(seperation.y))
            {
                current.x += Sign(seperation.x);
            }
            else
            {
                current.y += Sign(seperation.y);
            }
            result.Add(current);
        }
        
		return result;
	}
}
---------------------------------------------*/

/* ------------MY BROKENNESS------------------------
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


// You'll need a node class for storage of the pathfinding costs of each step.  Create one here.
// It needs coordinates, a reference to a parent node, and a cost value.  Also, put these functions in it:
class Node
{
    public int CurrentMoveCost { get; set; }
    public Point CurrentCoords { get; set; }
    public Node Parent { get; set; }

    public Node(int costValue, Point coords, Node parent)
    {
        CurrentMoveCost = costValue;
        CurrentCoords = coords;
        Parent = parent;
    }

    // Algorithm's f()
    private float CostFunction(Point from, Point to)
    {
        return MoveCost(from, to) + Heuristic(from, to);
    }

    // Algorithm's g()
    private float MoveCost(Point from, Point to)
    {
        
        return cost;
    }

    // Algorithm's h()
    private float Heuristic(Point from, Point to)
    {
        // One decent heuristic: How many steps does it take to get to the goal, ignoring terrain costs?
        return 0.0f;
    }

    public float CalculateCost(Point goal)
    {
        Tile tile = World.Instance().GetTileFromCoords(coords);

        return MoveCost(tile.coords, goal) + Heuristic(tile.coords, goal);
    }
}

public class Navigator : MonoBehaviour
{

    public static int Abs(int a)
    {
        return (a < 0 ? -a : a);
    }

    public static int Sign(int a)
    {
        return (a < 0 ? -1 : 1);
    }

    public List<Point> ComputePath(Point start, Point goal)
    {
        List<Point> result = new List<Point>();

        // TODO: Implement A* pathfinding
        // These comments describe the steps in coding this algorithm.
        // For a real challenge, delete all of my comments!


        // You'll need a list of open nodes, a list of closed nodes, and a current node.
        List<Node> openSet = new List<Node>();
        List<Node> closedSet = new List<Node>();

        Node currentNode = new Node(World.Instance().GetTileFromCoords(start).moveCost, start, null);
        Node cameFrom = currentNode.Parent;

        // Add the first node to the open list.
        openSet.Add(currentNode);

        // Loop over the open list until it is empty.
        while(openSet != null)
        {
            if(currentNode.CurrentCoords == goal)
            {
                return ReconstructedPath(cameFrom.CurrentCoords, goal); // maybe start not cameFrom
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            // List of neighbors
            List<Node> neighbors = new List<Node>();

            neighbors.Add(new Node(World.Instance().GetTileFromCoords(start + new Point(0, 1)).moveCost, start + new Point(0, 1), currentNode)); // above node
            neighbors.Add(new Node(World.Instance().GetTileFromCoords(start + new Point(1, 0)).moveCost, start + new Point(1, 0), currentNode)); // node to the right
            neighbors.Add(new Node(World.Instance().GetTileFromCoords(start + new Point(0, -1)).moveCost, start + new Point(0, -1), currentNode)); // node below
            neighbors.Add(new Node(World.Instance().GetTileFromCoords(start + new Point(-1, 0)).moveCost, start + new Point(-1, 0), currentNode)); // node to the left

            foreach(Node n in neighbors)
            {
                if (closedSet.Contains(n))
                {
                    continue;
                }
                float tempCost = g(currentNode) + DistanceBetween(currentNode, n);
                if (!openSet.Contains(n))
                {
                    openSet.Add(n);
                }
                else if(tempCost >= g(n))
                {
                    continue; // path cost is worse than a previously calculated cost
                }
                n.Parent = currentNode;
                g(n) = tempCost;
                f(n) = g(n) + h(n, goal);
            }
        }
        // If there are no nodes left in the open list, we can't find a valid path.  Return a null list.
        if(openSet.Count == 0)
        {
            return null;
        }

        // If we've reached the destination, we're done looping.

        // Otherwise, remove the current node from the open list and add it to the closed list.

        // For each neighbor of the current node, if it is undiscovered (not in the open list or the closed list), add it to the open list.
        // This is where you decide if you'll allow diagonal movement.

        // The open list should be sorted so that the lowest cost nodes are used first.
        // You can either use a sorted container that sorts automatically or use List.Sort().  You may need to refer to online documentation for this.

        // Fill the result List with the path coords.  You can follow the chain of parents of the current (last) node to reconstruct the path.


        return result;
    }

    public List<Point> ReconstructedPath(Point start, Point current)
    {
        return null;
    }
}
------------------------------------------*/
/*
class TileNode
{
    public Point coords;
    public TileNode parent;

    public float terrainCost;
    private float manhattanDistance;
    

    public TileNode(Point coords, TileNode parent, Point goal)
    {
        this.coords = coords;
        this.parent = parent;

        Tile tile = World.Instance().GetTileFromCoords(coords);
        terrainCost = tile.moveCost;

        manhattanDistance = Heuristic(coords, goal); // initialize manhattan distance
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
     *

    // Method for grabbing the distance
    public float GetDistance()
    {
        return manhattanDistance;
    }

    public float GrabCostFunction(Point from, Point to)
    {
        return CostFunction(from, to);
    }

    // Algorithm's f()
    private float CostFunction(Point from, Point to)
    {
        return MoveCost(from, to) + Heuristic(from, to);
    }

    // Algorithm's g()
    private float MoveCost(Point from, Point to)
    {
        return terrainCost;
    }

    // Algorithm's h()
    private float Heuristic(Point from, Point to)
    {
        // One decent heuristic: How many steps does it take to get to the goal, ignoring terrain costs?

        //ds2(A,B) = abs(B.x-A.x) + abs(B.y - A.y)

        return (Mathf.Abs(to.x - from.x) + Mathf.Abs(to.y - from.y));
    }

    public float CalculateCost(Point goal)
    {
        Tile tile = World.Instance().GetTileFromCoords(coords);

        return CostFunction(tile.coords, goal) + Heuristic(tile.coords, goal);
    }
}
*/
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class TileNode : IComparable<TileNode>
{
    public Point coords;
    public TileNode parent;

    public float tileCost = 0f;
    public float gCost = 0f;
    public float hCost = 0f;

    private float manhattanDistance;

    public TileNode(Point coords, TileNode parent, Point goal)
    {
        this.coords = coords;
        this.parent = parent;

        CalculateCost(goal);
    }

    public static int Abs(int a)
    {
        return (a < 0 ? -a : a);
    }

    public static int Min(int a, int b)
    {
        return (a < b ? a : b);
    }

    // Method for grabbing the distance
    public float GetDistance()
    {
        return manhattanDistance;
    }

    // Algorithm's g()
    private float TileCost(Point from, Point to)
    {
        Tile tile = World.Instance().GetTileFromCoords(from);
        return tile.moveCost;
    }

    // Algorithm's h()
    private float Heuristic(Point from, Point to)
    {
        // One decent heuristic: How many steps does it take to get to the goal, ignoring terrain costs?
        int dx = Abs(to.x - from.x);  // Steps horizontally
        int dy = Abs(to.y - from.y);  // Steps vertically
        return dx + dy - Min(dx, dy);  // Subtract the savings from moving diagonally
    }

    public void CalculateCost(Point goal)
    {
        tileCost = TileCost(coords, goal);
        gCost = tileCost;  // Will be updated later
        hCost = Heuristic(coords, goal);
    }

    public Int32 CompareTo(TileNode other)
    {
        float totalCost = gCost + hCost;
        float otherTotalCost = other.gCost + other.hCost;

        if (totalCost < otherTotalCost)
            return -1;
        if (totalCost > otherTotalCost)
            return 1;
        return 0;
    }
}

public class Navigator : MonoBehaviour
{

    public static int Abs(int a)
    {
        return (a < 0 ? -a : a);
    }

    public static int Sign(int a)
    {
        return (a < 0 ? -1 : 1);
    }

    public List<Point> ComputePath(Point from, Point to)
    {
        List<Point> result = new List<Point>();

        // A* pathfinding
        List<TileNode> open = new List<TileNode>();
        List<TileNode> closed = new List<TileNode>();

        TileNode currentNode = new TileNode(from, null, to);
        if (currentNode == null)
            return null;

        open.Add(currentNode);

        while (true)
        {
            // Did we run out of nodes to check?  Then no way to get there!
            if (open.Count == 0)
                return null;

            // Get the cheapest node (because the open set is sorted)
            currentNode = open[0];

            // Are at our goal?  Yay!
            if (currentNode.coords == to)
                break;

            // Move node from open set to closed set
            open.RemoveAt(0);
            closed.Add(currentNode);

            // Check neighboring nodes
            for (int x = -1; x <= 1; ++x)
            {
                for (int y = -1; y <= 1; ++y)
                {

                    // Don't consider the center node, that's the current one!
                    if (x == 0 && y == 0)
                        continue;

                    Point neighborCoords = currentNode.coords + new Point(x, y);

                    // See if this is a valid location
                    Tile tile = World.Instance().GetTileFromCoords(neighborCoords);
                    if (tile == null || tile.impassable || tile.occupant != null)
                        continue;

                    // If it's already in the closed set, skip it
                    if (closed.Find(obj => obj.coords == neighborCoords) != null)
                        continue;

                    // Create the node, which will set its basic costs
                    TileNode neighbor = new TileNode(neighborCoords, currentNode, to);

                    // Calculate the exact cost for this new node (the cost to get from the start to here).
                    neighbor.gCost = currentNode.gCost + neighbor.tileCost;

                    // Is this node already in the open list?
                    // If it is, then check to see if it's easier to get to from currentNode.
                    // Different paths which use the same node could have different costs.
                    TileNode openNode = open.Find(obj => obj.coords == neighborCoords);
                    if (openNode != null)
                    {
                        if (neighbor.gCost < openNode.gCost)
                        {
                            openNode.gCost = neighbor.gCost;
                            openNode.parent = currentNode;
                        }
                    }
                    else
                    {
                        // Not in the open set yet?  Then add it.
                        open.Add(neighbor);
                    }
                }
            }
            
            // Make sure the lowest cost nodes are checked first.
            open.Sort();
        }

        // We found the path, now retrace through the last node to construct the path coords.
        // currentNode is the goal node after all that work, so save the parent node coords all the way back to the start.
        while (currentNode != null && currentNode.coords != from)
        {
            result.Add(currentNode.coords);
            currentNode = currentNode.parent;
        }
        result.Reverse();

        return result;
    }
}

/*
public class Navigator : MonoBehaviour
{
    public static int Abs(int a)
    {
        return (a < 0 ? -a : a);
    }

    public static int Sign(int a)
    {
        return (a < 0 ? -1 : 1);
    }

    public List<Point> ComputePath(Point start, Point goal)
    {
        List<Point> result = new List<Point>();

        // TODO: Implement A* pathfinding
        // These comments describe the steps in coding this algorithm.
        // For a real challenge, delete all of my comments!


        // You'll need a list of open nodes, a list of closed nodes, and a current node.
        List<TileNode> openSet = new List<TileNode>();
        HashSet<TileNode> closedSet = new HashSet<TileNode>();

        TileNode currentNode = new TileNode(start, null, goal);
        TileNode cameFrom = currentNode.parent;

        // Add the first node to the open list.
        openSet.Add(currentNode);

        // Loop over the open list until it is empty.
        while (openSet != null)
        {
            if (currentNode.coords == goal)
            {
                return ReconstructedPath(cameFrom.coords, goal); // maybe start not cameFrom
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            // List of neighbors
            List<TileNode> neighbors = new List<TileNode>();

            neighbors.Add(new TileNode(start + new Point(0, 1), currentNode, goal)); // above node
            neighbors.Add(new TileNode(start + new Point(1, 0), currentNode, goal)); // node to the right
            neighbors.Add(new TileNode(start + new Point(0, -1), currentNode, goal)); // node below
            neighbors.Add(new TileNode(start + new Point(-1, 0), currentNode, goal)); // node to the left

            foreach (TileNode n in neighbors)
            {
                if (closedSet.Contains(n))
                {
                    continue; // If the tile has already been evalutated 
                }

                float tempCost = n.CalculateCost(goal);
                if (!openSet.Contains(n))
                {
                    openSet.Add(n);
                }
                else if (tempCost >= n.terrainCost)
                {
                    continue; // path cost is worse than a previously calculated cost
                }
                n.parent = currentNode;
                n.terrainCost = tempCost;
                n.GrabCostFunction(currentNode.coords, n.coords);
            }
        }
        // If there are no nodes left in the open list, we can't find a valid path.  Return a null list.
        if (openSet.Count == 0)
        {
            return null;
        }

        // If we've reached the destination, we're done looping.

        // Otherwise, remove the current node from the open list and add it to the closed list.

        // For each neighbor of the current node, if it is undiscovered (not in the open list or the closed list), add it to the open list.
        // This is where you decide if you'll allow diagonal movement.

        // The open list should be sorted so that the lowest cost nodes are used first.
        // You can either use a sorted container that sorts automatically or use List.Sort().  You may need to refer to online documentation for this.

        // Fill the result List with the path coords.  You can follow the chain of parents of the current (last) node to reconstruct the path.


        return result;
    }

    public List<Point> ReconstructedPath(Point start, Point current)
    {
        return null;
    }
}
*/
