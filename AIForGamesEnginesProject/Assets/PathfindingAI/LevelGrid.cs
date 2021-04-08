using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
    public LayerMask obsructedAreaMask;
   
    public Vector2 levelGridSize;
    
    //Area of each node on the grid
    public float nodeRadius;
    
    //Array of nodes that make up the level grid.
    Node[,] grid;

    float nodeDiameter;
    int gridX, gridY;

    //Sets initial values and determines grid x and y values based on how many nodes.
    void Start()
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
                grid[x, y] = new Node(notObstructed, worldPos);
            }
        }
    }

    //Allows us to see the size of the level grid in the editor. 
    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(levelGridSize.x, 1, levelGridSize.y));
        if(grid != null)
        {
            foreach(Node n in grid )
            {
                Gizmos.color = (n.notObstructed)?Color.white:Color.red;
                Gizmos.DrawCube(n.nodeWorldPos, Vector3.one * (nodeDiameter-.1f));
            }
        }
    }
}
