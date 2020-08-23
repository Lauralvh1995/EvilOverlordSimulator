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
    Event BuildingIsBuilt;

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
                    break;
                case Building.ROAD:
                    rend.material.SetColor("_BaseColor", Color.white);
                    break;
                case Building.FARM:
                    rend.material.SetColor("_BaseColor", Color.green);
                    break;
                case Building.MINE:
                    rend.material.SetColor("_BaseColor", Color.yellow);
                    break;
                case Building.STATUE:
                    rend.material.SetColor("_BaseColor", Color.blue);
                    break;
                case Building.COURTHOUSE:
                    rend.material.SetColor("_BaseColor", Color.red);
                    break;
                case Building.TOWER:
                    rend.material.SetColor("_BaseColor", Color.grey);
                    break;
            }
            
        }
        else
        {
            physicalObject.gameObject.SetActive(false);
        }
        BuildingIsBuilt.Invoke();

        if(content == Building.ROAD || content == Building.EMPTY)
        {
            obstacle.enabled = false;
        }
        else
        {
            obstacle.enabled = true;
        }
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
