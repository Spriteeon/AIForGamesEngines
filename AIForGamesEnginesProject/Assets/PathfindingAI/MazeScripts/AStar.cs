using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class AStar : MonoBehaviour
{
    //start and end positions of the path
    public Transform enemy, mazeEnd;
    private Vector3 enemyStart;
    
    //smooth time, velocity and height diff to add  for enemy movement.
    public float enemySpeed;
    public Vector3 vel;
    public Vector3 diffY;

    public bool canRunAlgorithm;
    
    LevelGrid levelGrid;

    void Start()
    {
        levelGrid = GetComponent<LevelGrid> ();
        canRunAlgorithm = false;
    }

    void Update()
    {

        if (canRunAlgorithm)
        {
            CalculateOptimalPath(enemy.position, mazeEnd.position);
        }
    }
    void CalculateOptimalPath(Vector3 enemyPos, Vector3 mazeEndPos)
    {
        Node enemyStartNode = levelGrid.NodePosInWorld(enemyPos);
        Node mazeEndNode = levelGrid.NodePosInWorld(mazeEndPos);

        Optimisation<Node> openNodes = new Optimisation<Node>(levelGrid.GetMaxSize);
        List<Node> closedNodes = new List<Node>();

        openNodes.AddToHeap(enemyStartNode);

        while(openNodes.GetCurrentNumElements > 0) 
        {
            Node currentNode = openNodes.RemoveFromHeap();
            
            closedNodes.Add(currentNode);
            
            foreach(Node adjacentNode in levelGrid.GetAdjacentNodes(currentNode)) 
            {
                if (!adjacentNode.notObstructed || closedNodes.Contains(adjacentNode))
                {
                    continue;
                }

                int updatedMoveCost = currentNode.gCost + GetDistanceBetweenNodes(currentNode, adjacentNode);
               
                if (updatedMoveCost < adjacentNode.gCost || !openNodes.Contains(adjacentNode))
                {
                  adjacentNode.gCost = updatedMoveCost;
                  adjacentNode.hCost = GetDistanceBetweenNodes(adjacentNode, mazeEndNode);
                  adjacentNode.parentNode = currentNode;

                  if (!openNodes.Contains(adjacentNode))
                  {
                     openNodes.AddToHeap(adjacentNode);
                  } 
                 
                }
            }
            //if pathfinding is complete
            if(currentNode == mazeEndNode) 
            {
                RetracePath(enemyStartNode,mazeEndNode);
                 return;
            }
        }  
    }

    int GetDistanceBetweenNodes(Node A, Node B)
    {
        int distanceX = Mathf.Abs(A.nodePosOnGridX - B.nodePosOnGridX);
        int distanceY = Mathf.Abs(A.nodePosOnGridY - B.nodePosOnGridY);

        if(distanceX > distanceY)
        {
            return 14 * distanceY + 10 * (distanceX-distanceY);
        }
           
        return 14 * distanceX + 10 * (distanceY-distanceX);
        
    }

    void RetracePath(Node pathStart, Node pathEnd)
    {
        List<Node> finalPath = new List<Node>();
        Node currentNode = pathEnd;
        
        while(currentNode != pathStart)
        {
            finalPath.Add(currentNode);
            currentNode = currentNode.parentNode;
        }
        
        finalPath.Reverse();
        levelGrid.finalPath = finalPath;
        enemyStart = enemy.position;
        
        //Moves ai along the found path.
        foreach(Node fpNode in finalPath)
        {
               enemy.transform.position = Vector3.SmoothDamp(enemyStart, finalPath[0].nodeWorldPosition + diffY, ref vel, enemySpeed);
        }
    }   
}
