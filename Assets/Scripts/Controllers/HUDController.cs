﻿using System.Collections;
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
    }
}
