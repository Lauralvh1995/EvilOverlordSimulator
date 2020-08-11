using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public int width;
    public int height;

    private Cell[,] cells;
    public Cell cellPrefab;

    private void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        cells = new Cell[height, width];
        float xOffset = 0.5f;
        for (int x = 0; x < width; x++)
        {
            float yOffset = 0.5f;
            for (int y = 0; y < height; y++)
            {
                Cell newTile = Instantiate(cellPrefab);
                newTile.transform.SetParent(transform);
                newTile.transform.localPosition = new Vector3(xOffset, 0, yOffset);
                newTile.name = string.Format("Cell {0}x{1}", x, y);

                cells[x, y] = newTile;
                cells[x, y].x = x;
                cells[x, y].y = y;

                yOffset += cellPrefab.size;
            }
            xOffset += cellPrefab.size;
        }
    }

}
