using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField]
    GameObject menuPopUp;
    bool menuEnabled = false;

    public void MenuButtonClick()
    {
        menuEnabled = !menuEnabled;
        menuPopUp.SetActive(menuEnabled);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SaveGame()
    {

    }

}
