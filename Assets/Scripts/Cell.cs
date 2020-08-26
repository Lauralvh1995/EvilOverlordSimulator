using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public int x, y;
    public int size;

    public Transform buildingPrefab;

    [SerializeField]
    Minion minion;

    [SerializeField]
    BuildingObject building;
    [SerializeField]
    bool occupied;
    private void Awake()
    {
        BuildObject();
    }

    public BuildingObject GetBuilding()
    {
        return building;
    }
    public Minion GetMinion()
    {
        return minion;
    }
    public void CheckOccupied()
    {
        if (building.content == Building.EMPTY || building.content == Building.NONE)
        {
            occupied = false;
        }
        else
        {
            occupied = true;
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

    public void Build(Building type)
    {
        building.SetBuildingType(type);
    }

    public bool IsBase()
    {
        return building.content == Building.BASE;
    }

    public bool IsActive()
    {
        return building.IsActive();
    }
}
