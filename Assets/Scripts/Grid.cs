using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class Grid : MonoBehaviour
{
    public int width;
    public int height;

    public Cell[,] Cells { get; private set; }

    public Cell cellPrefab;

    [SerializeField]
    Event buildingAppears;

    [SerializeField]
    Event UIUpdate;

    private void OnEnable()
    {
        buildingAppears.AddListener(CheckCellStatus);
    }
    private void OnDisable()
    {
        buildingAppears.RemoveListener(CheckCellStatus);
    }


    private void Awake()
    {
        Initialize();
    }

    public void CheckCellStatus()
    {
        foreach (Cell cell in Cells)
        {
            cell.CheckOccupied();
            CheckIfBuildingInCellShouldBeActive(cell);
            CheckMinionOwnership(cell);
        }
        UIUpdate.Invoke();
    }

    private void CheckMinionOwnership(Cell cell)
    {
        cell.AssignMinion(null);
        foreach(Minion m in Player.instance.GetMinions())
        {
            if(m.workplace == cell.GetBuilding() || m.house == cell.GetBuilding())
            {
                cell.AssignMinion(m);
            }
        }
    }

    public void Initialize()
    {
        Cells = new Cell[height, width];
        float xOffset = 0.5f;
        for (int x = 0; x < width; x++)
        {
            float yOffset = 0.5f;
            for (int y = 0; y < height; y++)
            {
                Cell newTile = Instantiate(cellPrefab);
                newTile.transform.SetParent(transform);
                newTile.transform.localPosition = new Vector3(xOffset, 0.001f, yOffset);
                newTile.name = string.Format("Cell {0}x{1}", x, y);

                Cells[x, y] = newTile;
                Cells[x, y].x = x;
                Cells[x, y].y = y;

                yOffset += cellPrefab.size;
            }
            xOffset += cellPrefab.size;
        }
        //Initializing the Base
        Cells[12, 12].Build(Building.BASE);
        Cells[11, 11].Build(Building.BASE);
        Cells[13, 13].Build(Building.BASE);
        Cells[12, 13].Build(Building.BASE);
        Cells[12, 11].Build(Building.BASE);
        Cells[11, 12].Build(Building.BASE);
        Cells[11, 13].Build(Building.BASE);
        Cells[13, 12].Build(Building.BASE);
        Cells[13, 11].Build(Building.BASE);
        CheckCellStatus();
    }

    public void CheckIfBuildingInCellShouldBeActive(Cell c)
    {
        //bool baseFound = false;
        HashSet<Cell> roadCluster = new HashSet<Cell>();
        Building b = c.GetContent().content;
        //add origin to set
        //check up, left, down and right from origin
        //check if same building,
        //no more of this building, check for roads
        //keep checking road until Base is encountered
        roadCluster.Add(c);
        //making the Building Cluster
        if (!b.Equals(Building.EMPTY) && !b.Equals(Building.ROAD) && !b.Equals(Building.BASE))
        {
            bool newlyAdded = true;
            while (newlyAdded)
            {
                foreach (Cell i in roadCluster.ToList())
                {
                    newlyAdded = false;
                    if (i.y < height - 1 && (Cells[i.x, i.y + 1].GetContent().content == Building.ROAD || Cells[i.x, i.y + 1].GetContent().content == Building.BASE))
                    {
                        if(Cells[i.x, i.y +1].GetContent().content == Building.BASE)
                        {
                            c.SetStatusOfBuilding(true);
                            return;
                        }
                        if (roadCluster.Add(Cells[i.x, i.y + 1]))
                        {
                            newlyAdded = true;
                        }
                    }
                    if (i.y > 0 && (Cells[i.x, i.y - 1].GetContent().content == Building.ROAD || Cells[i.x, i.y - 1].GetContent().content == Building.BASE))
                    {
                        if (Cells[i.x, i.y - 1].GetContent().content == Building.BASE)
                        {
                            c.SetStatusOfBuilding(true);
                            return;
                        }
                        if (roadCluster.Add(Cells[i.x, i.y - 1]))
                        {
                            newlyAdded = true;
                        }
                    }
                    if (i.x < width - 1 && (Cells[i.x + 1, i.y].GetContent().content == Building.ROAD || Cells[i.x + 1, i.y].GetContent().content == Building.BASE))
                    {
                        if (Cells[i.x + 1, i.y].GetContent().content == Building.BASE)
                        {
                            c.SetStatusOfBuilding(true);
                            return;
                        }
                        if (roadCluster.Add(Cells[i.x + 1, i.y]))
                        {
                            newlyAdded = true;
                        }
                    }
                    if (i.x > 0 && (Cells[i.x - 1, i.y].GetContent().content == Building.ROAD || Cells[i.x - 1, i.y].GetContent().content == Building.BASE))
                    {
                        if (Cells[i.x - 1, i.y].GetContent().content == Building.BASE)
                        {
                            c.SetStatusOfBuilding(true);
                            return;
                        }
                        if (roadCluster.Add(Cells[i.x - 1, i.y]))
                        {
                            newlyAdded = true;
                        }
                    }
                }
            }
        }
    }
}

