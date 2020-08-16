using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public int x, y;
    public int size;

    public Transform buildingPrefab;

    [SerializeField]
    BuildingObject building;
    [SerializeField]
    bool occupied;
    private void Start()
    {
        BuildObject();
    }

    public void CheckOccupied()
    {
        if (!building.content.Equals(Building.EMPTY))
        {
            occupied = true;
        }
        else
        {
            occupied = false;
        }
    }

    public bool IsOccupied()
    {
        return occupied;
    }

    public BuildingObject GetContent()
    {
        return building;
    }

    public void SetStatusOfBuilding(bool status)
    {
        building.SetActive(status);
    }

    public void BuildObject()
    {
        //instantiate building object prefab here
        building = Instantiate(buildingPrefab.GetComponent<BuildingObject>());
        building.transform.SetParent(transform);
        building.transform.localPosition = Vector3.zero;
        building.name = string.Format("Building {0}x{1}", x, y);
    }
}
