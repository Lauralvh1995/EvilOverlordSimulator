using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum Building
{
    EMPTY,
    BASE,
    ROAD,
    HOUSE,
    MINE,
    COURTHOUSE,
    TOWER,
    FARM,
    STATUE
}
[RequireComponent(typeof(NavMeshObstacle))]
public class BuildingObject : MonoBehaviour
{
    public Building content;
    public Transform physicalObject;

    [SerializeField]
    IntEvent BuildingIsBuilt;

    [SerializeField]
    private bool active;

    [SerializeField]
    private NavMeshObstacle obstacle;

    private void Start()
    {
        obstacle = GetComponent<NavMeshObstacle>();
    }

    public void SetActive(bool status)
    {
        active = status;
    }

    public bool IsActive()
    {
        return active;
    }

    public void SetBuildingType(Building building)
    {
        content = building;
        int buildCost = 0;
        if (content != Building.EMPTY)
        {
            physicalObject.gameObject.SetActive(true);
            Renderer rend = physicalObject.GetComponent<Renderer>();
            rend.material.shader = Shader.Find("HDRP/Lit");
            switch (building)
            {
                case Building.BASE:
                    rend.material.SetColor("_BaseColor", Color.cyan);
                    break;
                case Building.HOUSE:
                    rend.material.SetColor("_BaseColor", Color.magenta);
                    buildCost = 3;
                    break;
                case Building.ROAD:
                    rend.material.SetColor("_BaseColor", Color.white);
                    buildCost = 1;
                    break;
                case Building.FARM:
                    rend.material.SetColor("_BaseColor", Color.green);
                    buildCost = 2;
                    break;
                case Building.MINE:
                    rend.material.SetColor("_BaseColor", Color.yellow);
                    buildCost = 2;
                    break;
                case Building.STATUE:
                    rend.material.SetColor("_BaseColor", Color.blue);
                    buildCost = 4;
                    break;
                case Building.COURTHOUSE:
                    rend.material.SetColor("_BaseColor", Color.red);
                    buildCost = 4;
                    break;
                case Building.TOWER:
                    rend.material.SetColor("_BaseColor", Color.grey);
                    buildCost = 4;
                    break;
            }
        }
        else
        {
            physicalObject.gameObject.SetActive(false);
            buildCost = -1;
        }

        if (content == Building.ROAD || content == Building.EMPTY)
        {
            obstacle.enabled = false;
        }
        else
        {
            obstacle.enabled = true;
        }
        BuildingIsBuilt?.Invoke(buildCost);
    }

    private void OnMouseOver()
    {
        //display tooltip
        Debug.Log("Mouse is over me");
        physicalObject.transform.localScale = new Vector3(0, 1f, 0);
    }

    private void OnMouseExit()
    {
        //stop displaying tooltip
        Debug.Log("Mouse is no longer over me");
        physicalObject.transform.localScale = new Vector3(0, 0.9f, 0);
    }
}
