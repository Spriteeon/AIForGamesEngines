using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
    public LayerMask obsructedAreaMask;
    public Vector2 levelGridSize;
    public float nodeRadius;
    Node[,] grid;

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(levelGridSize.x, 1, levelGridSize.y));
    }
}
