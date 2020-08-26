using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    public List<GameObject> HUDElements;

    public Text wealthText;
    public Text foodText;
    public Text stabilityText;
    public Text powerProjectionText;
    public Text moraleText;
    public Text flairText;
    public Text goldText;
    public Text minionText;

    public Button mineButton;
    public Button farmButton;
    public Button houseButton;
    public Button statueButton;
    public Button courtButton;
    public Button towerButton;
    public Button roadButton;
    public Button emptyButton;
    public Button noneButton;

    public void EnableHUD(bool status)
    {
        foreach (GameObject o in HUDElements)
        {
            o.SetActive(status);
        }
    }

    public void UpdateTexts() {
        wealthText.text = Player.GetWealth().ToString();
        foodText.text = Player.GetFood().ToString();
        stabilityText.text = Player.GetStability().ToString();
        powerProjectionText.text = Player.GetPowerProjection().ToString();
        moraleText.text = Player.GetMorale().ToString();
        flairText.text = Player.GetFlair().ToString();
        goldText.text = Player.GetGold().ToString();
        minionText.text = Player.GetMinionCount().ToString();
    }

    public void UpdateButtons()
    {
        if(Player.GetGold() >= Player.instance.roadCost)
        {
            roadButton.interactable = true;
        }
        else
        {
            roadButton.interactable = false;
        }
        if (Player.GetGold() >= Player.instance.houseCost)
        {
            houseButton.interactable = true;
        }
        else
        {
            houseButton.interactable = false;
        }
        if (Player.GetGold() >= Player.instance.mineCost)
        {
            mineButton.interactable = true;
        }
        else
        {
            mineButton.interactable = false;
        }
        if (Player.GetGold() >= Player.instance.farmCost)
        {
            farmButton.interactable = true;
        }
        else
        {
            farmButton.interactable = false;
        }
        if (Player.GetGold() >= Player.instance.towerCost)
        {
            towerButton.interactable = true;
        }
        else
        {
            towerButton.interactable = false;
        }
        if (Player.GetGold() >= Player.instance.courtCost)
        {
            courtButton.interactable = true;
        }
        else
        {
            courtButton.interactable = false;
        }
        if (Player.GetGold() >= Player.instance.statueCost)
        {
            statueButton.interactable = true;
        }
        else
        {
            statueButton.interactable = false;
        }

        emptyButton.GetComponent<Image>().color = Color.white;
        houseButton.GetComponent<Image>().color = Color.white;
        farmButton.GetComponent<Image>().color = Color.white;
        mineButton.GetComponent<Image>().color = Color.white;
        towerButton.GetComponent<Image>().color = Color.white;
        statueButton.GetComponent<Image>().color = Color.white;
        courtButton.GetComponent<Image>().color = Color.white;
        roadButton.GetComponent<Image>().color = Color.white;
        switch (Player.instance.buildMode)
        {
            case Building.NONE:
                noneButton.interactable = false;
                break;
            case Building.EMPTY:
                noneButton.interactable = true;
                emptyButton.GetComponent<Image>().color = Color.green;
                break;
            case Building.HOUSE:
                noneButton.interactable = true;
                houseButton.GetComponent<Image>().color = Color.green;
                break;
            case Building.FARM:
                noneButton.interactable = true;
                farmButton.GetComponent<Image>().color = Color.green;
                break;
            case Building.MINE:
                noneButton.interactable = true;
                mineButton.GetComponent<Image>().color = Color.green;
                break;
            case Building.TOWER:
                noneButton.interactable = true;
                towerButton.GetComponent<Image>().color = Color.green;
                break;
            case Building.STATUE:
                noneButton.interactable = true;
                statueButton.GetComponent<Image>().color = Color.green;
                break;
            case Building.COURTHOUSE:
                noneButton.interactable = true;
                courtButton.GetComponent<Image>().color = Color.green;
                break;
            case Building.ROAD:
                noneButton.interactable = true;
                roadButton.GetComponent<Image>().color = Color.green;
                break;
        }
    }
}
