﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Building
{
    EMPTY,
    BASE,
    ROAD,
    HOUSE,
    MINE,
    COURTHOUSE,
    TOWER,
    FARM
}

public class BuildingObject : MonoBehaviour
{
    public Building content;
    public Transform physicalObject;

    [SerializeField]
    Event BuildingIsBuilt;

    [SerializeField]
    private bool active;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetActive(bool status)
    {
        active = status;
    }

    public void SetBuildingType(Building building)
    {
        content = building;
        if (content != Building.EMPTY)
        {
            physicalObject.gameObject.SetActive(true);
            BuildingIsBuilt.Invoke();
        }
        else
        {
            physicalObject.gameObject.SetActive(false);
        }
    }
}
