using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class AStar : MonoBehaviour
{
    public Transform enemy, goal;
    public float enemySpeed;
    private Vector3 enemyStart;
    
    private float fraction;
    public Vector3 currentVelocity;
    public Vector3 positionAddition;

    public bool canRunAlgorithm;
    
    LevelGrid grid;

    //public List<Node> finalPath;
     

    void Start()
    {
        grid = GetComponent<LevelGrid> ();
        canRunAlgorithm = false;
    }

    void Update()
    {

        CalculateOptimalPath(enemy.position, goal.position);
        //if(canRunAlgorithm)
        //{
        //    CalculateOptimalPath(enemy.position, goal.position);
        //}
    }


    void CalculateOptimalPath(Vector3 enemyPos, Vector3 goalPos)
    {

        Stopwatch sw = new Stopwatch();
        sw.Start();
        //The start and end of the path. Used to determin the path that needs calculating. 
        Node enemyStartNode = grid.NodePosInWorld(enemyPos);
        Node goalNode = grid.NodePosInWorld(goalPos);

        //groups of open and closed nodes used in pathfinding calculations. 
        //Open = is yet to be checked but is in range. Closed = has been checked and processed. 
        List<Node> openNodesList = new List<Node>();
        List<Node> closedNodesList = new List<Node>();

        openNodesList.Add(enemyStartNode);

        while(openNodesList.Count > 0) 
        {
            Node currentNode = openNodesList[0];
            for(int i = 1; i < openNodesList.Count; i++) 
            {
                
                if(openNodesList[i].fcost < currentNode.fcost || openNodesList[i].fcost == currentNode.fcost) 
                {
                    if (openNodesList[i].h < currentNode.h)
                    {
                        currentNode = openNodesList[i];
                    }
                        
                    
                }
            }
               
            openNodesList.Remove(currentNode);
            closedNodesList.Add(currentNode);
            
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

                int newMovementCost = currentNode.g + GetDistanceBetweenNodes(currentNode, adjacentNode);
               if (newMovementCost < adjacentNode.g || !openNodesList.Contains(adjacentNode))
               {
                  adjacentNode.g = newMovementCost;
                  adjacentNode.h = GetDistanceBetweenNodes(adjacentNode, goalNode);
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

        //grid.FPath = finalPath;
        // Vector3 movementVec = Vector3.zero;
        //if(finalPath != null)
        //{
        //    foreach(Node n in finalPath)
        //    {
        //        if(fraction < 1)
        //        {
        //            fraction += Time.deltaTime * enemySpeed;
        //            enemy.transform.position = n.nodeWorldPosition;
        //        }
        //        //movementVec += n.nodeWorldPosition;
        //       // enemy.transform.position = n.nodeWorldPosition;
        //    }
        // enemy.transform.position
        enemyStart = enemy.position;
        //enemyGoal = goal.position;
        
        foreach(Node n in finalPath)
        {
            
            
              // fraction += Time.deltaTime * enemySpeed;
              // enemy.transform.position = Vector3.Lerp(enemyStart, n.nodeWorldPosition, fraction);
               enemy.transform.position = Vector3.SmoothDamp(enemyStart, finalPath[0]/*n*/.nodeWorldPosition + positionAddition, ref currentVelocity, enemySpeed);
            
        }
        
        //}
    }   
}
