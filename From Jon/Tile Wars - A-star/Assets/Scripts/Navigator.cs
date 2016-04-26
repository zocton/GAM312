using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class TileNode : IComparable<TileNode> {
	public Point coords;
	public TileNode parent;

	public float tileCost = 0f;
	public float gCost = 0f;
	public float hCost = 0f;
	
	public TileNode(Point coords, TileNode parent, Point goal) {
		this.coords = coords;
		this.parent = parent;

		CalculateCost (goal);
	}
	
	public static int Abs(int a) {
		return (a < 0? -a : a);
	}

	public static int Min(int a, int b) {
		return (a < b? a : b);
	}

	
	// Algorithm's g()
	private float TileCost(Point from, Point to) {
		Tile tile = World.Instance ().GetTileFromCoords (from);
		return tile.moveCost;
	}
	
	// Algorithm's h()
	private float Heuristic(Point from, Point to) {
		// One decent heuristic: How many steps does it take to get to the goal, ignoring terrain costs?
		int dx = Abs (to.x - from.x);  // Steps horizontally
		int dy = Abs (to.y - from.y);  // Steps vertically
		return dx + dy - Min(dx, dy);  // Subtract the savings from moving diagonally
	}
	
	public void CalculateCost(Point goal) {
		tileCost = TileCost(coords, goal);
		gCost = tileCost;  // Will be updated later
		hCost = Heuristic(coords, goal);
	}
	
	public Int32 CompareTo(TileNode other) {
		float totalCost = gCost + hCost;
		float otherTotalCost = other.gCost + other.hCost;

		if (totalCost < otherTotalCost)
			return -1;
		if (totalCost > otherTotalCost)
			return 1;
		return 0;
	}
}

public class Navigator : MonoBehaviour {
	
	public static int Abs(int a) {
		return (a < 0? -a : a);
	}
	
	public static int Sign(int a) {
		return (a < 0 ? -1 : 1);
	}
	
	public List<Point> ComputePath(Point from, Point to) {
		List<Point> result = new List<Point>();
		
		// A* pathfinding
		List<TileNode> open = new List<TileNode>();
		List<TileNode> closed = new List<TileNode> ();
		
		TileNode currentNode = new TileNode (from, null, to);
		if (currentNode == null)
			return null;
		
		open.Add (currentNode);
		
		while (true) {
			// Did we run out of nodes to check?  Then no way to get there!
			if(open.Count == 0)
				return null;

			// Get the cheapest node (because the open set is sorted)
			currentNode = open[0];

			// Are at our goal?  Yay!
			if(currentNode.coords == to)
				break;

			// Move node from open set to closed set
			open.RemoveAt(0);
			closed.Add (currentNode);

			// Check neighboring nodes
			for(int x = -1; x <= 1; ++x) {
				for(int y = -1; y <= 1; ++y) {

					// Don't consider the center node, that's the current one!
					if(x == 0 && y == 0)
						continue;

					Point neighborCoords = currentNode.coords + new Point(x, y);
					
					// See if this is a valid location
					Tile tile = World.Instance ().GetTileFromCoords (neighborCoords);
					if(tile == null || tile.impassable || tile.occupant != null)
						continue;

					// If it's already in the closed set, skip it
					if(closed.Find (obj => obj.coords == neighborCoords) != null)
						continue;
					
					// Create the node, which will set its basic costs
					TileNode neighbor = new TileNode (neighborCoords, currentNode, to);

					// Calculate the exact cost for this new node (the cost to get from the start to here).
					neighbor.gCost = currentNode.gCost + neighbor.tileCost;

					// Is this node already in the open list?
					// If it is, then check to see if it's easier to get to from currentNode.
					// Different paths which use the same node could have different costs.
					TileNode openNode = open.Find(obj => obj.coords == neighborCoords);
					if(openNode != null) {
						if(neighbor.gCost < openNode.gCost) {
							openNode.gCost = neighbor.gCost;
							openNode.parent = currentNode;
						}
					}
					else {
						// Not in the open set yet?  Then add it.
						open.Add (neighbor);
					}
				}
			}

			// Make sure the lowest cost nodes are checked first.
			open.Sort();
		}
		
		// We found the path, now retrace through the last node to construct the path coords.
		// currentNode is the goal node after all that work, so save the parent node coords all the way back to the start.
		while (currentNode != null && currentNode.coords != from) {
			result.Add (currentNode.coords);
			currentNode = currentNode.parent;
		}
		result.Reverse ();
		
		return result;
	}
}
