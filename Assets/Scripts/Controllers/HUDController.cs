using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    public List<GameObject> HUDElements;
    
    public void EnableHUD(bool status)
    {
        foreach(GameObject o in HUDElements)
        {
            o.SetActive(status);
        }
    }
}
