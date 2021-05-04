using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
    public Transform player;
   
    
    public LayerMask obstructedMask;
   
    public Vector2 gridWorldSize;
    
    //Area of each node on the grid
    public float nodeRad;//Array of nodes that make up the level grid.
    float nodeDiam;
    Node[,] grid;
    
    int gridWidth, gridHeight;

   

    //Sets initial values and determines grid x and y values based on how many nodes.
    void Awake()
    {
        nodeDiam = nodeRad*2;
        gridWidth = Mathf.RoundToInt(this.gridWorldSize.x/nodeDiam);
        gridHeight = Mathf.RoundToInt(this.gridWorldSize.y/nodeDiam);

        GenerateLevelGrid();
    }

    /*Calculates the nodes in the grid based on the pre determined level grid size. 
     i.e, how many whole nodes succesfully fit into the grid*/

    /*Gets each world point at which each node will be located. */
    void GenerateLevelGrid()
    {
        grid = new Node[gridWidth,gridHeight];

        Vector3 lowerLeftCorner = this.transform.position - Vector3.right * gridWorldSize.x/2 - Vector3.forward * gridWorldSize.y / 2;

        for(int x = 0; x < gridWidth; x++)
        {
            for(int y = 0; y < gridHeight; y++)
            {
                Vector3 worldPos = lowerLeftCorner + Vector3.right * (x * nodeDiam + nodeRad) + Vector3.forward * (y * nodeDiam + nodeRad);
                //Vector3 worldPos = transform.position;
                bool walkable = !(Physics.CheckSphere(worldPos,nodeRad,obstructedMask));
                grid[x,y] = new Node(walkable,worldPos, x,y);
            }
        }
    }

    public List<Node> GetAdjacentNodes(Node currentNode)
    {
        List<Node> adjacentNodes = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for(int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                {
                    continue;
                }

                int X = currentNode.nodePosOnGridX + x;
                int Y = currentNode.nodePosOnGridY + y;

                if(X >= 0 && X < gridWidth && Y >= 0 && Y < gridHeight)
                {
                    adjacentNodes.Add(grid[X, Y]);
                }
            }
        }

        return adjacentNodes;
    }

    public Node NodePosInWorld(Vector3 worldNodePosition)
    {
        float percentX = (worldNodePosition.x + gridWorldSize.x/2) / gridWorldSize.x;
        float percentY = (worldNodePosition.z + gridWorldSize.y/2) / gridWorldSize.y;

        //Preventing erorrs if pos is outside grid bounds.
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x_ = Mathf.RoundToInt((gridWidth-1) * percentX);
        int y_ = Mathf.RoundToInt((gridHeight-1) * percentY);
        
        //Debug.Log(grid[x_, y_]);
        return grid[x_,y_];
       
        
    }

    public List<Node> finalPath;

    
    //Allows visualisation of grid in 3d space as well as specific elements in different colours. 
    //For debugging ONLY.
    void OnDrawGizmos()
    {
        
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x,1,gridWorldSize.y));
        if(grid != null) 
        {
            Node playerNode = NodePosInWorld(player.position);
            foreach(Node n in grid) 
            {
                Gizmos.color = (n.notObstructed)?Color.white:Color.red;

                if(finalPath != null)
                {
                    if(finalPath.Contains(n))
                    {
                        Gizmos.color = Color.blue;
                    }
                }
                else
                {
                    Debug.Log("Null final path");
                }

                if(playerNode == n)
                {
                    Gizmos.color = Color.black;
                   

                }
                
                  

                Gizmos.DrawCube(n.nodeWorldPosition, Vector3.one * (nodeDiam-.1f));
            }
        }
    }
}
