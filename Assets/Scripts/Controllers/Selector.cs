using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Selector : MonoBehaviour
{
    public Cell hover;
    public Cell selected;

    public Transform selectorGraphic;
    public LayerMask field;
    bool allowedToSelect = true;

    public PopUp popup;
    public BasePopUp basePopUp;

    private void Update()
    {
        if (allowedToSelect)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, 100f, field))
            {
                hover = hitInfo.transform.GetComponent<Cell>();
                selectorGraphic.transform.position = new Vector3(hover.transform.position.x, 0f, hover.transform.position.z);
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
                    popup.Hide();
                    basePopUp.Hide();
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
                switch (hovered.GetBuilding().content)
                {
                    case Building.BASE:
                        basePopUp.SetPopUpInfo(hovered.transform.position, "BASE", "Recruit Minions here");
                        break;
                    case Building.ROAD:
                        break;
                    case Building.EMPTY:
                        break;
                    default:
                        {
                            popup.SetPopUpInfo(hovered.transform.position,
                            hovered.GetBuilding().content.ToString(),
                            "Some text about this building",
                            hovered.GetBuilding().IsActive(),
                            hovered.GetMinion());
                            break;
                        }
                }
            }
            else
            {
                popup.Hide();
                basePopUp.Hide();
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
            popup.Hide();
            basePopUp.Hide();
        }

    }

    public void SetAllowed(bool status)
    {
        allowedToSelect = status;
    }
}
