using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public int x, y;
    public int size;

    public Transform buildingPrefab;

    public BoxCollider box;

    [SerializeField]
    Minion minion;

    [SerializeField]
    Event MinionClaimsBuilding;

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

    public void CheckOwnership()
    {
        if(minion != null)
            Debug.Log(minion.GetName());

        minion = null;
        foreach(Minion m in Player.instance.GetMinions())
        {
            if(m.house == building || m.workplace == building)
            {
                minion = m;
            }
        }
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

    public void AssignMinion(Minion newMinion)
    {
        minion = newMinion;
    }

    public void Build(Building type)
    {
        building.SetBuildingType(type);
        if(building.content == Building.EMPTY || building.content == Building.ROAD)
        {
            box.enabled = false;
        }
        else
        {
            box.enabled = true;
        }
        if (minion)
        {
            if (building.content == Building.EMPTY)
            {
                if (building.content == Building.HOUSE)
                {
                    minion.house = null;
                }
                else
                {
                    minion.workplace = null;
                }
                minion = null;
            }
        }
    }

    public bool IsBase()
    {
        return building.content == Building.BASE;
    }

    public bool IsActive()
    {
        return building.IsActive();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (building.content == Building.BASE || building.content == Building.EMPTY || building.content == Building.ROAD)
        {
            return;
        }
        Debug.Log(other.ToString());
        if (other.GetComponent<DragObject>().IsFalling())
        {
            Minion newMinion = other.GetComponent<Minion>();
            switch (building.content)
            {
                case Building.HOUSE:
                    newMinion.house = building;
                    Debug.Log(newMinion.GetName() + " says: The Overlord gave me a house!");
                    break;
                default:
                    newMinion.workplace = building;
                    Debug.Log(newMinion.GetName() + " says: The Overlord gave me a place to work!");
                    break;
            }
            MinionClaimsBuilding.Invoke();
            //CheckOwnership();
        }
    }
}
