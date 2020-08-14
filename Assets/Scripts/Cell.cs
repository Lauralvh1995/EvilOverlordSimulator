using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public int x, y;
    public int size;

    [SerializeField]
    BuildingObject content;
    [SerializeField]
    bool occupied;

    public void CheckOccupied()
    {
        if (content != null)
        {
            occupied = true;
        }
        else
        {
            occupied = false;
        }
    }

    public bool isOccupied()
    {
        return occupied;
    }

    public BuildingObject GetContent()
    {
        return content;
    }

    public void SetStatusOfBuilding(bool status)
    {
        content.SetActive(status);
    }
}
