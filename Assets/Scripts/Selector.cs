using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour
{
    public Cell hover;
    public Cell selected;

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo))
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

    void SelectHovered(Cell hovered)
    {
        if (hovered.GetComponent<Cell>() != selected || selected == null)
        {
            selected = hovered.GetComponent<Cell>();
        }
        else
        {
            selected = null;
        }

    }
}
