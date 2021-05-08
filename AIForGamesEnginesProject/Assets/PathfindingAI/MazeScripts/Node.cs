using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : IHeapElement<Node>
{
    //G and H cost of each node used to calculate the f cost.
    public int g;
    public int h;


    public int nodePosOnGridX;
    public int nodePosOnGridY;
    
    public bool notObstructed;
    public Vector3 nodeWorldPosition;
   // public Vector3 nodeRelativePos;

    public Node parentNode;
    int heapIndex;


    public Node(bool _notObstructed, Vector3 _nodeWorldPos/*_nodeRelativePos*/, int _posOnGridX, int _posOnGridY)
    {
        notObstructed = _notObstructed;
        nodeWorldPosition = _nodeWorldPos;
        //nodeRelativePos = _nodeRelativePos;
        nodePosOnGridX = _posOnGridX;
        nodePosOnGridY = _posOnGridY;
    }

    //obtaining the value of the f cost.
    public int fcost
    {
        get
        {
            return g + h;
        }
        
    }

    public int HeapIndex
    {
        get
        {
            return heapIndex;
        }
        set
        {
            heapIndex = value;
        }
    }

    public int CompareTo(Node nodeToCompare)
    {
        int compare = fcost.CompareTo(nodeToCompare.fcost);
        if(compare == 0)
        {
            compare = h.CompareTo(nodeToCompare.h);
        }
        return -compare;
    }
}
