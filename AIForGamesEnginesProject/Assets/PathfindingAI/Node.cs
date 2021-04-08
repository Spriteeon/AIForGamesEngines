using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public bool notObstructed;
    public Vector3 nodeWorldPos;

    public Node(bool canBeWalkedOn, Vector3 nodePos)
    {
        notObstructed = canBeWalkedOn;
        nodeWorldPos = nodePos;
    }
}
