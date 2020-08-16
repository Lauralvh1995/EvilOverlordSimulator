using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour
{
    public Cell hover;
    public Cell selected;

    public LayerMask field;

    bool allowedToSelect;

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
            selected = hovered.GetComponent<Cell>();
            selected.BuildObject();
            selected.GetContent().SetBuildingType(Building.HOUSE);
        }
        else
        {
            selected = null;
        }

    }

    public void setAllowed(bool status)
    {
        allowedToSelect = status;
    }
}
