using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
    public Transform player;
    public LayerMask obsructedAreaMask;
   
    public Vector2 levelGridSize;
    
    //Area of each node on the grid
    public float nodeRadius;

    public List<Node> FPath;

    //Array of nodes that make up the level grid.
    Node[,] grid;

    float nodeDiameter;
    int gridX, gridY;


    

    //Sets initial values and determines grid x and y values based on how many nodes.
    void Awake()
    {
        nodeDiameter = nodeRadius * 2;
        gridX = Mathf.RoundToInt(levelGridSize.x / nodeDiameter);
        gridY = Mathf.RoundToInt(levelGridSize.y / nodeDiameter);

        GenerateLevelGrid();
    }

    /*Calculates the nodes in the grid based on the pre determined level grid size. 
     i.e, how many whole nodes succesfully fit into the grid*/

    /*Gets each world point at which each node will be located. */
    void GenerateLevelGrid()
    {
        grid = new Node[gridX, gridY];

        Vector3 bottomLeftCorner = transform.position - Vector3.right * levelGridSize.x / 2 - Vector3.forward * levelGridSize.y / 2;

        for(int x = 0; x < gridX; x++)
        {
            for(int y = 0; y < gridY; y++)
            {
                Vector3 worldPos = bottomLeftCorner + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);

                bool notObstructed = !(Physics.CheckSphere(worldPos, nodeRadius, obsructedAreaMask));
                grid[x,y] = new Node(notObstructed, worldPos, x, y);
            }
        }
    }

    public List<Node> GetAdjacentNodes(Node node)
    {
        List<Node> adjacentNodes = new List<Node>();

        for(int x = -1; x <= 1; x++)
        {
            for(int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                {
                    

                    int checkX = node.nodePosX + x;
                    int checkY = node.nodePosY + y;

                    if(checkX >= 0 && checkX < gridX && checkY >=0 && checkY<gridY)
                    {
                        adjacentNodes.Add(grid[checkX, checkY]);
                    }
                    continue;
                }
            }
        }

        return adjacentNodes;
    }

    public Node NodeFromWorldPoint(Vector3 nodeWorldPos)
    {
        float percentX = (nodeWorldPos.x + levelGridSize.x / 2) / levelGridSize.x;
        float percentY = (nodeWorldPos.z + levelGridSize.y / 2) / levelGridSize.y;

        //Preventing erorrs if pos is outside grid bounds.
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridX - 1) * percentX);
        int y = Mathf.RoundToInt((gridY - 1) * percentY);

        return grid[x,y];
    }

   

    //public AStar algorithm;
    void OnDrawGizmos()
    {
        
        Gizmos.DrawWireCube(transform.position, new Vector3(levelGridSize.x, 1, levelGridSize.y));
        if(grid != null) 
        {
            Node playerNode = NodeFromWorldPoint(player.position);
            foreach(Node n in grid) 
            {
                Gizmos.color = (n.notObstructed)?Color.white:Color.red;

                if(playerNode == n)
                {
                    Gizmos.color = Color.black;
                    if (FPath != null)
                    {
                         if (FPath.Contains(n))
                         {
                             Gizmos.color = Color.blue;
                             Debug.Log("fuck");
                          }
                       
                    }

                }
                
                
                    

                Gizmos.DrawCube(n.nodeWorldPos, Vector3.one * (nodeDiameter-.1f));
            }
        }
    }
}
