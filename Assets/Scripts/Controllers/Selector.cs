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
            if (Player.instance.buildMode == Building.NONE)
            {
                //doe ding met popup
            }
            else
            {
                if (!selected.IsBase() && (!selected.IsOccupied() || Player.instance.buildMode == Building.EMPTY))
                {
                    if (Player.instance.buildMode == Building.EMPTY && !selected.IsOccupied())
                    {
                        return;
                    }
                    if (Player.instance.IsAllowedToBuild())
                        selected.Build(Player.instance.buildMode);
                }
            }
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
}
