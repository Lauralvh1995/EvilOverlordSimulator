using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public void CheckCellStatus()
    {
        foreach (Cell cell in cells)
        {
            cell.CheckOccupied();
            //CheckIfBuildingInCellShouldBeActive(cell);
        }
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

    public void CheckIfBuildingInCellShouldBeActive(Cell c)
    {
        bool newlyAdded = true;
        HashSet<Cell> buildingCluster = new HashSet<Cell>();
        HashSet<Cell> roadCluster = new HashSet<Cell>();
        Building b = c.GetContent().content;
        //add origin to set
        //check up, left, down and right from origin
        //check if same building,
        //no more of this building, check for roads
        //keep checking road until Base is encountered
        buildingCluster.Add(c);
        //making the Building Cluster
        while (newlyAdded)
        {
            foreach (Cell i in buildingCluster.ToList())
            {
                newlyAdded = false;
                if (i.y < height - 1 && cells[i.x, i.y + 1].GetContent().content == b)
                {
                    if (buildingCluster.Add(cells[i.x, i.y + 1]))
                    {
                        newlyAdded = true;
                    }
                }
                if (i.y > 0 && cells[i.x, i.y - 1].GetContent().content == b)
                {
                    if (buildingCluster.Add(cells[i.x, i.y - 1]))
                    {
                        newlyAdded = true;
                    }
                }
                if (i.x < width - 1 && cells[i.x + 1, i.y].GetContent().content == b)
                {
                    if (buildingCluster.Add(cells[i.x + 1, i.y]))
                    {
                        newlyAdded = true;
                    }
                }
                if (i.x > 0 && cells[i.x - 1, i.y].GetContent().content == b)
                {
                    if (buildingCluster.Add(cells[i.x - 1, i.y]))
                    {
                        newlyAdded = true;
                    }
                }
            }
        }
        Debug.Log("This cluster is "+buildingCluster.Count+ " cells big");
        //checking for road starts
        newlyAdded = true;
        while (newlyAdded)
        {
            foreach (Cell i in buildingCluster.ToList())
            {
                newlyAdded = false;
                if (i.y < height - 1 && (cells[i.x, i.y + 1].GetContent().content is Road || cells[i.x, i.y + 1].GetContent().content is Base))
                {
                    if (roadCluster.Add(cells[i.x, i.y + 1]))
                    {
                        newlyAdded = true;
                    }
                }
                if (i.y > 0 && (cells[i.x, i.y - 1].GetContent().content is Road || cells[i.x, i.y + 1].GetContent().content is Base))
                {
                    if (roadCluster.Add(cells[i.x, i.y - 1]))
                    {
                        newlyAdded = true;
                    }
                }
                if (i.x < width - 1 && (cells[i.x + 1, i.y].GetContent().content is Road || cells[i.x, i.y + 1].GetContent().content is Base))
                {
                    if (roadCluster.Add(cells[i.x + 1, i.y]))
                    {
                        newlyAdded = true;
                    }
                }
                if (i.x > 0 && (cells[i.x - 1, i.y].GetContent().content is Road || cells[i.x, i.y + 1].GetContent().content is Base))
                {
                    if (roadCluster.Add(cells[i.x - 1, i.y]))
                    {
                        newlyAdded = true;
                    }
                }
            }
        }
        
        if (roadCluster.Count > 0)
        {
            foreach (Cell a in roadCluster)
            {
                if (a.GetContent().content is Base)
                {
                    Debug.Log("Found a Base");
                    foreach (Cell d in buildingCluster)
                    {
                        d.SetStatusOfBuilding(true);
                    }
                    return;
                }
            }
            //checking for roads
            newlyAdded = true;
            while (newlyAdded)
            {
                foreach (Cell i in roadCluster.ToList())
                {
                    newlyAdded = false;
                    if (i.y < height - 1 && (cells[i.x, i.y + 1].GetContent().content is Road || cells[i.x, i.y + 1].GetContent().content is Base))
                    {
                        if (roadCluster.Add(cells[i.x, i.y + 1]))
                        {
                            newlyAdded = true;
                        }
                    }
                    if (i.y > 0 && (cells[i.x, i.y - 1].GetContent().content is Road || cells[i.x, i.y + 1].GetContent().content is Base))
                    {
                        if (roadCluster.Add(cells[i.x, i.y - 1]))
                        {
                            newlyAdded = true;
                        }
                    }
                    if (i.x < width - 1 && (cells[i.x + 1, i.y].GetContent().content is Road || cells[i.x, i.y + 1].GetContent().content is Base))
                    {
                        if (roadCluster.Add(cells[i.x + 1, i.y]))
                        {
                            newlyAdded = true;
                        }
                    }
                    if (i.x > 0 && (cells[i.x - 1, i.y].GetContent().content is Road || cells[i.x, i.y + 1].GetContent().content is Base))
                    {
                        if (roadCluster.Add(cells[i.x - 1, i.y]))
                        {
                            newlyAdded = true;
                        }
                    }
                }
            }
            foreach (Cell a in roadCluster)
            {
                if (a.GetContent().content is Base)
                {
                    Debug.Log("Found a Base");
                    foreach (Cell d in buildingCluster)
                    {
                        d.SetStatusOfBuilding(true);
                    }
                    return;
                }
            }
        }
        
    }
}

