using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum Building
{
    NONE,
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
public class BuildingObject : MonoBehaviour
{
    public Building content;
    public Transform physicalObject;

    [SerializeField]
    IntEvent RemoveGoldFromPlayer;
    [SerializeField]
    Event BuildingIsBuilt;

    [SerializeField]
    private bool active;

    [SerializeField]
    private NavMeshObstacle obstacle;

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
        int buildCost = 0;
        if (building != Building.NONE)
        {
            content = building;

            if (content != Building.EMPTY)
            {
                physicalObject.gameObject.SetActive(true);
                Renderer rend = physicalObject.GetComponent<Renderer>();
                rend.material.shader = Shader.Find("HDRP/Lit");
                switch (content)
                {
                    case Building.BASE:
                        rend.material.SetColor("_BaseColor", Color.cyan);
                        break;
                    case Building.HOUSE:
                        rend.material.SetColor("_BaseColor", Color.magenta);
                        buildCost = Player.instance.houseCost;
                        break;
                    case Building.ROAD:
                        rend.material.SetColor("_BaseColor", Color.white);
                        buildCost = Player.instance.roadCost;
                        break;
                    case Building.FARM:
                        rend.material.SetColor("_BaseColor", Color.green);
                        buildCost = Player.instance.farmCost;
                        break;
                    case Building.MINE:
                        rend.material.SetColor("_BaseColor", Color.yellow);
                        buildCost = Player.instance.mineCost;
                        break;
                    case Building.STATUE:
                        rend.material.SetColor("_BaseColor", Color.blue);
                        buildCost = Player.instance.statueCost;
                        break;
                    case Building.COURTHOUSE:
                        rend.material.SetColor("_BaseColor", Color.red);
                        buildCost = Player.instance.courtCost;
                        break;
                    case Building.TOWER:
                        rend.material.SetColor("_BaseColor", Color.grey);
                        buildCost = Player.instance.towerCost;
                        break;
                }
            }
            else
            {
                physicalObject.gameObject.SetActive(false);
                buildCost = Player.instance.emptyCost;
            }
        }
        if (content == Building.ROAD || content == Building.EMPTY || content == Building.NONE)
        {
            obstacle.carving = false;
        }
        else
        {
            obstacle.carving = true;
        }
        RemoveGoldFromPlayer.Invoke(buildCost);
        BuildingIsBuilt.Invoke();
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
