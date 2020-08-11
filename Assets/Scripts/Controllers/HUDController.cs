using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    public GameObject SideBar;
    public GameObject BottomBar;
    
    public void EnableHUD(bool status)
    {
        SideBar.SetActive(status);
        BottomBar.SetActive(status);
    }
}
