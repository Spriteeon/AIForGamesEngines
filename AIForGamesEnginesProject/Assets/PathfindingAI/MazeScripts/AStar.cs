using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class AStar : MonoBehaviour
{
    //start and end positions of the path
    public Transform enemy, goal;
    private Vector3 enemyStart;
    
    //smooth time, velocity and height diff to add  for enemy movement.
    public float enemySpeed;
    public Vector3 currentVelocity;
    public Vector3 diffY;

    public bool canRunAlgorithm;
    
    LevelGrid grid;

    void Start()
    {
        grid = GetComponent<LevelGrid> ();
        canRunAlgorithm = false;
    }

    void Update()
    {

        if (canRunAlgorithm)
        {
            CalculateOptimalPath(enemy.position, goal.position);
        }
    }


    void CalculateOptimalPath(Vector3 enemyPos, Vector3 goalPos)
    {
        //Stopwatch for analysing algorithm efficiency.
        Stopwatch sw = new Stopwatch();
        sw.Start();
        
        //The start and end of the path. Used to determin the path that needs calculating. 
        Node enemyStartNode = grid.NodePosInWorld(enemyPos);
        Node goalNode = grid.NodePosInWorld(goalPos);

        //Lists of open and closed nodes used in pathfinding calculations. 
        //Open = is yet to be checked but is in range. Closed = has been checked and processed. 
        List<Node> openNodesList = new List<Node>();
        List<Node> closedNodesList = new List<Node>();

        openNodesList.Add(enemyStartNode);

        //While there are still open nodes to explore:
        while(openNodesList.Count > 0) 
        {
            Node currentNode = openNodesList[0];
            for(int i = 1; i < openNodesList.Count; i++) 
            {
                
                if(openNodesList[i].fCost < currentNode.fCost || openNodesList[i].fCost == currentNode.fCost) 
                {
                    /*if multiple adjacent nodes in the open list have the same f cost, 
                    look to the h cost to decide which is the optimum node.*/
                    if (openNodesList[i].hCost < currentNode.hCost)
                    {
                        currentNode = openNodesList[i];
                    }
                        
                    
                }
            }
               
            openNodesList.Remove(currentNode);
            closedNodesList.Add(currentNode);
            
            //if pathfinding is complete
            if(currentNode == goalNode) 
            {
                sw.Stop();
                
                RetracePath(enemyStartNode,goalNode);
                print("Path calculated in: " + sw.ElapsedMilliseconds + "ms");
                 return;
            }

            foreach(Node adjacentNode in grid.GetAdjacentNodes(currentNode)) 
            {
                if (!adjacentNode.notObstructed || closedNodesList.Contains(adjacentNode))
                {
                    continue;
                   
                }

               int newMovementCost = currentNode.gCost + GetDistanceBetweenNodes(currentNode, adjacentNode);
               if (newMovementCost < adjacentNode.gCost || !openNodesList.Contains(adjacentNode))
               {
                  adjacentNode.gCost = newMovementCost;
                  adjacentNode.hCost = GetDistanceBetweenNodes(adjacentNode, goalNode);
                  adjacentNode.parentNode = currentNode;

                 if (!openNodesList.Contains(adjacentNode))
                 {
                        openNodesList.Add(adjacentNode);
                 }    
               }
            }
        }  
    }

    int GetDistanceBetweenNodes(Node A, Node B)
    {
        int distanceX = Mathf.Abs(A.nodePosOnGridX - B.nodePosOnGridX);
        int distanceY = Mathf.Abs(A.nodePosOnGridY - B.nodePosOnGridY);

        if(distanceX > distanceY)
        {
            return 14*distanceY + 10* (distanceX-distanceY);
        }
           
        return 14*distanceX + 10 * (distanceY-distanceX);
        
    }

    void RetracePath(Node startNode, Node endNode)
    {
        List<Node> finalPath = new List<Node>();
        Node currentNode = endNode;
        
        while(currentNode != startNode)
        {
            finalPath.Add(currentNode);
            currentNode = currentNode.parentNode;
        }
        
        finalPath.Reverse();
        grid.finalPath = finalPath;
        enemyStart = enemy.position;
        
        //Moves ai along the found path.
        foreach(Node n in finalPath)
        {
               enemy.transform.position = Vector3.SmoothDamp(enemyStart, finalPath[0].nodeWorldPosition + diffY, ref currentVelocity, enemySpeed);
        }
    }   
}
