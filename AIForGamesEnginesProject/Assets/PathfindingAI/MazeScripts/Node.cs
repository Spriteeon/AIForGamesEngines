using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : IHeapElement<Node>
{
    //G and H cost of each node used to calculate the f cost.
    public int gCost;
    public int hCost;


    public int nodePosOnGridX;
    public int nodePosOnGridY;
    
    public bool notObstructed;
    public Vector3 nodeWorldPosition;
   // public Vector3 nodeRelativePos;

    public Node parentNode;
    int index;


    public Node(bool _notObstructed, Vector3 _nodeWorldPos/*_nodeRelativePos*/, int _posOnGridX, int _posOnGridY)
    {
        notObstructed = _notObstructed;
        nodeWorldPosition = _nodeWorldPos;
        //nodeRelativePos = _nodeRelativePos;
        nodePosOnGridX = _posOnGridX;
        nodePosOnGridY = _posOnGridY;
    }

    //obtaining the value of the f cost.
    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
        
    }

    public int indexInHeap
    {
        get
        {
            return index;
        }
        set
        {
            index = value;
        }
    }

    public int CompareTo(Node comparingNode)
    {
        int compare = fCost.CompareTo(comparingNode.fCost);
        if(compare == 0)
        {
            compare = hCost.CompareTo(comparingNode.hCost);
        }
        return -compare;
    }
}
