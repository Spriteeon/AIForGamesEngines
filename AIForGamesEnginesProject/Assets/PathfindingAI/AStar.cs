using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar : MonoBehaviour
{
    public Transform Seeker, Target;

    LevelGrid grid;

    public List<Node> finalPath;

    void Awake()
    {
        grid = GetComponent<LevelGrid> ();
    }

    void Update()
    {
        FindPath(Seeker.position, Target.position);
    }


    void FindPath(Vector3 startPos, Vector3 endPos)
    {
        //The start and end of the path. Used to determin the path that needs calculating. 
        Node startNode = grid.NodeFromWorldPoint(startPos);
        Node endNode = grid.NodeFromWorldPoint(endPos);

        //groups of open and closed nodes used in pathfinding calculations. 
        //Open = is yet to be checked but is in range. Closed = has been checked and processed. 
        List<Node> openNodes = new List<Node>();
        HashSet<Node> closedNodes = new HashSet<Node>();

        openNodes.Add(startNode);

        while(openNodes.Count > 0) 
        {
            Node node = openNodes[0];
            for(int i = 1; i < openNodes.Count; i++) 
            {
                
                if(openNodes[i].fCost < node.fCost || openNodes[i].fCost == node.fCost) 
                {
                    if (openNodes[i].hCost < node.hCost)
                    {
                        node = openNodes[i];
                    }
                        
                    
                }
            }
               
            openNodes.Remove(node);
            closedNodes.Add(node);
            
            if(node == endNode) 
            {
                RetracePath(startNode, endNode);
                 return;
            }


            foreach(Node adjacentNode in grid.GetAdjacentNodes(node)) 
            {
               if (!adjacentNode.notObstructed || closedNodes.Contains(adjacentNode)) 
                 {
                    continue;
                   //break;
                 }   
                
               int newMovementCostToAdjacent = node.gCost + GetDistanceBetweenNodes(node, adjacentNode);
               if (newMovementCostToAdjacent < adjacentNode.gCost || !openNodes.Contains(adjacentNode))
               {
                  adjacentNode.gCost = newMovementCostToAdjacent;
                  adjacentNode.hCost = GetDistanceBetweenNodes(adjacentNode, endNode);
                  adjacentNode.parentNode = node;

                 if (!openNodes.Contains(adjacentNode))
                 {
                        openNodes.Add(adjacentNode);
                 }
                     

               }
            }


        }


        
    }

    void RetracePath(Node startNode, Node endNode)
    {
        finalPath = new List<Node>();
        Node currentNode = endNode;

        while(currentNode != startNode)
        {
            finalPath.Add(currentNode);
            currentNode = currentNode.parentNode;
        }
        finalPath.Reverse();

        grid.FPath = finalPath;
    }



    int GetDistanceBetweenNodes(Node nodeA, Node nodeB)
    {
        int distX = Mathf.Abs(nodeA.nodePosX - nodeB.nodePosX);
        int distY = Mathf.Abs(nodeA.nodePosY - nodeB.nodePosY);

        if(distX > distY)
                return 14 * distY + 10 * (distX - distY);
       
        
        
        return 14 * distX + 10 * (distY - distX);
        
    }
}
