using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Selector : MonoBehaviour
{
    public Cell hover;
    public Cell selected;

    public LayerMask field;

    public Building buildMode;

    bool allowedToSelect = true;

    private void Update()
    {
        if (allowedToSelect)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, 100f, field))
            {
                hover = hitInfo.transform.GetComponent<Cell>();
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current.IsPointerOverGameObject())
                    return;
                if (hover)
                {
                    SelectHovered(hover);
                }
                else
                {
                    selected = null;
                }
            }
        }
    }

    void SelectHovered(Cell hovered)
    {
        if (hovered.GetComponent<Cell>() != selected || selected == null)
        {
            selected = hovered;
            if(!selected.IsBase() && (!selected.IsOccupied() || buildMode == Building.EMPTY))
                selected.Build(buildMode);
        }
        else
        {
            selected = null;
        }

    }

    public void SetAllowed(bool status)
    {
        allowedToSelect = status;
    }

    public void SetBuildMode(Building mode)
    {
        buildMode = mode;
    }

    public void SetBuildModeToEmpty()
    {
        SetBuildMode(Building.EMPTY);
    }
    public void SetBuildModeToHouse()
    {
        SetBuildMode(Building.HOUSE);
    }
    public void SetBuildModeToRoad()
    {
        SetBuildMode(Building.ROAD);
    }
    public void SetBuildModeToFarm()
    {
        SetBuildMode(Building.FARM);
    }
    public void SetBuildModeToTower()
    {
        SetBuildMode(Building.TOWER);
    }
    public void SetBuildModeToStatue()
    {
        SetBuildMode(Building.STATUE);
    }
    public void SetBuildModeToCourtHouse()
    {
        SetBuildMode(Building.COURTHOUSE);
    }
    public void SetBuildModeToMine()
    {
        SetBuildMode(Building.MINE);
    }
}
