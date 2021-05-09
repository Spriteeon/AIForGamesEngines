using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Optimisation <Node> where Node : IHeapElement<Node>
{
    Node[] elements;
    int currentNumElements;

    public Optimisation(int heapSizeMax)
    {
        elements = new Node[heapSizeMax];
    }

    public void AddToHeap(Node element)
    {
        element.indexInHeap = currentNumElements;
        elements[currentNumElements] = element;
        SortUp(element);
        currentNumElements++;
    }

    public Node RemoveFromHeap()
    {
        Node firstElement = elements[0];
        currentNumElements--;
        elements[0] = elements[currentNumElements];
        elements[0].indexInHeap = 0;
        SortDown(elements[0]);
        return firstElement;
    }

    public bool Contains(Node element)
    {
        return Equals(elements[element.indexInHeap], element);
    }

    //As we only increase the priority of nodes, sort down does not need to be called here.
    public void UpdateElement(Node element)
    {
        SortUp(element);
    }
    public int Count
    {
        get { return currentNumElements; }
    }
    void SortUp(Node element)
    {
        int parentIndex = (element.indexInHeap - 1) / 2;

        while(true)
        {
            Node parentElement = elements[parentIndex];
            if(element.CompareTo(parentElement) > 0)
            {
                RearrangeElements(element, parentElement);
            }
            else
            {
                break;
            }
            parentIndex = (element.indexInHeap - 1) / 2;
        }
    }

    void SortDown(Node element)
    {
        while(true)
        {
            int childIndexLeft = element.indexInHeap * 2 + 1;
            int childIndexRight = element.indexInHeap * 2 + 2;
            int swapIndex = 0;

            if(childIndexLeft < currentNumElements)
            {
                swapIndex = childIndexLeft;

                if(childIndexRight < currentNumElements)
                {
                    if(elements[childIndexLeft].CompareTo(elements[childIndexRight]) < 0)
                    {
                        swapIndex = childIndexRight;
                    }
                }
                if(element.CompareTo(elements[swapIndex]) < 0)
                {
                    RearrangeElements(element, elements[swapIndex]);
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }
    }
    
    void RearrangeElements(Node elementA, Node elementB)
    {
        elements[elementA.indexInHeap] = elementB;
        elements[elementB.indexInHeap] = elementA;

        int elementAIndex = elementA.indexInHeap;
        
        elementA.indexInHeap = elementB.indexInHeap;
        elementB.indexInHeap = elementAIndex;
    }
}

public interface IHeapElement<T> : IComparable<T>
{
    int indexInHeap
    {
        get;
        set;
    }
}