using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public bool bcanBeWalkedOn;
    public Vector3 nodeWorldPos;

    public Node(bool canBeWalkedOn, Vector3 nodePos)
    {
        canBeWalkedOn = bcanBeWalkedOn;
        nodeWorldPos = nodePos;
    }
}
