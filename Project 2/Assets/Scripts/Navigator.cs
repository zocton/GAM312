﻿/*
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

*/

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