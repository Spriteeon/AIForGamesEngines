using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public bool notObstructed;
    public Vector3 nodeWorldPos;

    public int nodePosX;
    public int nodePosY;
    
    public int gCost;
    public int hCost;

    public Node parentNode;
    


    public Node(bool canBeWalkedOn, Vector3 nodePos, int nodeX, int nodeY)
    {
        notObstructed = canBeWalkedOn;
        nodeWorldPos = nodePos;
        nodePosX = nodeX;
        nodePosY = nodeY;
    }

    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
        
    }
}
