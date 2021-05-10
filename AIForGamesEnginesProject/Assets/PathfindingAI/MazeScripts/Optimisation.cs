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
        UpSort(element);
        currentNumElements++;
    }

    public Node RemoveFromHeap()
    {
        Node firstElement = elements[0];
        currentNumElements--;
        elements[0] = elements[currentNumElements];
        elements[0].indexInHeap = 0;
        DownSort(elements[0]);
        return firstElement;
    }

    public bool Contains(Node element)
    {
        return Equals(elements[element.indexInHeap], element);
    }

    //As we only increase the priority of nodes, sort down does not need to be called here.
    public void UpdatePriority(Node element)
    {
        UpSort(element);
    }
    public int GetCurrentNumElements
    {
        get { return currentNumElements; }
    }
    void UpSort(Node element)
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

    void DownSort(Node element)
    {
        while(true)
        {
            int leftChildIndex = element.indexInHeap * 2 + 1;
            int rightChildIndex = element.indexInHeap * 2 + 2;
            int indexToSwap = 0;

            if(leftChildIndex < currentNumElements)
            {
                indexToSwap = leftChildIndex;

                if(rightChildIndex < currentNumElements)
                {
                    if(elements[leftChildIndex].CompareTo(elements[rightChildIndex]) < 0)
                    {
                        indexToSwap = rightChildIndex;
                    }
                }
                if(element.CompareTo(elements[indexToSwap]) < 0)
                {
                    RearrangeElements(element, elements[indexToSwap]);
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
    
    void RearrangeElements(Node A, Node B)
    {
        elements[A.indexInHeap] = B;
        elements[B.indexInHeap] = A;

        int tempAindex = A.indexInHeap;
        
        A.indexInHeap = B.indexInHeap;
        B.indexInHeap = tempAindex;
    }
}

public interface IHeapElement<Node> : IComparable<Node>
{
    int indexInHeap
    {
        get;
        set;
    }
}