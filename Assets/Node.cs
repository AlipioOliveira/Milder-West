﻿using UnityEngine;
using System.Collections;

public class Node
{

    public bool walkable;
    public Vector3 position;
    public int gridX;
    public int gridY;

    public int gCost;
    public int hCost;
    public Node parent;

    public Node(bool _walkable, Vector3 _position, int _gridX, int _gridY)
    {
        walkable = _walkable;
        position = _position;
        gridX = _gridX;
        gridY = _gridY;
    }

    public int getfCost()
    {
            return gCost + hCost;
    }
}

