using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public int x, y;
    public int size;

    public LayerMask buildingLayer;

    bool occupied;

    public void CheckOccupied()
    {
        if (Physics.Raycast(transform.position, Vector3.up, 1f, buildingLayer))
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
}
