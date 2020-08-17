using System.Collections;
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
    FARM,
    STATUE
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
                    rend.material.SetColor("_BaseColor", Color.black);
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
            BuildingIsBuilt.Invoke();
        }
        else
        {
            physicalObject.gameObject.SetActive(false);
        }
    }
}
