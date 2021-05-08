using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Optimization <T> where T : IHeapElement<T>
{
    T[] elements;
    int currentNumElements;

    public Optimization(int heapSizeMax)
    {
        elements = new T[heapSizeMax];
    }

    public void AddToHeap(T element)
    {
        element.HeapIndex = currentNumElements;
        elements[currentNumElements] = element;
        SortUp(element);
        currentNumElements++;
    }

    public T RemoveFromHeap()
    {
        T firstElement = elements[0];
        currentNumElements--;
        elements[0] = elements[currentNumElements];
        elements[0].HeapIndex = 0;
        SortDown(elements[0]);
        return firstElement;
    }

    public bool Contains(T element)
    {
        return Equals(elements[element.HeapIndex], element);
    }

    //As we only increase the priority of nodes, sort down does not need to be called here.
    public void UpdateElement(T element)
    {
        SortUp(element);
    }
    public int Count
    {
        get { return currentNumElements; }
    }
    void SortUp(T element)
    {
        int parentIndex = (element.HeapIndex - 1) / 2;

        while(true)
        {
            T parentElement = elements[parentIndex];
            if(element.CompareTo(parentElement) > 0)
            {
                RearrangeElements(element, parentElement);
            }
            else
            {
                break;
            }
            parentIndex = (element.HeapIndex - 1) / 2;
        }
    }

    void SortDown(T element)
    {
        while(true)
        {
            int childIndexLeft = element.HeapIndex * 2 + 1;
            int childIndexRight = element.HeapIndex * 2 + 2;
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
    
    void RearrangeElements(T elementA, T elementB)
    {
        elements[elementA.HeapIndex] = elementB;
        elements[elementB.HeapIndex] = elementA;

        int elementAIndex = elementA.HeapIndex;
        
        elementA.HeapIndex = elementB.HeapIndex;
        elementB.HeapIndex = elementAIndex;
    }
}

public interface IHeapElement<T> : IComparable<T>
{
    int HeapIndex
    {
        get;
        set;
    }
}