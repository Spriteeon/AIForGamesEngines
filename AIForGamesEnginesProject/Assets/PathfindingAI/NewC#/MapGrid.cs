using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGrid
{
    private int gridWidth;
    private int gridHeight;
    private int[,] gridArray;

    public MapGrid(int gridWidth, int gridHeight)
    {
        this.gridWidth = gridWidth;
        this.gridHeight = gridHeight;

        gridArray = new int[gridWidth, gridHeight];
    }
}
