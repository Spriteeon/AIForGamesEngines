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
            CalculateOptimumPath(enemy.position, mazeEnd.position);
        }
    }
    void CalculateOptimumPath(Vector3 enemyPos, Vector3 mazeEndPos)
    {
        //Stopwatch for analysing algorithm efficiency.
        Stopwatch sw = new Stopwatch();
        sw.Start();
        
        Node enemyStartNode = levelGrid.NodePosInWorld(enemyPos);
        Node mazeEndNode = levelGrid.NodePosInWorld(mazeEndPos);

        List<Node> openNodes = new List<Node>();
        List<Node> closedNodes = new List<Node>();

        openNodes.Add(enemyStartNode);

        while(openNodes.Count > 0) 
        {
            Node currentNode = openNodes[0];
            for(int i = 1; i < openNodes.Count; i++) 
            {
                
                if(openNodes[i].fCost < currentNode.fCost || openNodes[i].fCost == currentNode.fCost) 
                {
                    if (openNodes[i].hCost < currentNode.hCost)
                    {
                        currentNode = openNodes[i];
                    }  
                }
            }
            openNodes.Remove(currentNode);
            closedNodes.Add(currentNode);
            
            foreach(Node adjacentNode in levelGrid.GetAdjacentNodes(currentNode)) 
            {
                if (!adjacentNode.notObstructed || closedNodes.Contains(adjacentNode))
                {
                    continue;
                }

                int newMovementCost = currentNode.gCost + GetDistanceBetweenNodes(currentNode, adjacentNode);
               
                if (newMovementCost < adjacentNode.gCost || !openNodes.Contains(adjacentNode))
                {
                  adjacentNode.gCost = newMovementCost;
                  adjacentNode.hCost = GetDistanceBetweenNodes(adjacentNode, mazeEndNode);
                  adjacentNode.parentNode = currentNode;

                  if (!openNodes.Contains(adjacentNode))
                  {
                     openNodes.Add(adjacentNode);
                  } 
                 
                }
            }
            //if pathfinding is complete
            if(currentNode == mazeEndNode) 
            {
                sw.Stop();
                
                RetracePath(enemyStartNode,mazeEndNode);
                print("Path calculated in: " + sw.ElapsedMilliseconds + "ms");
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
