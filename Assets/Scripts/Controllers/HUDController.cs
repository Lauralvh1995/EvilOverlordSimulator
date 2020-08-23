using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    public List<GameObject> HUDElements;
    public Player player;

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

    [SerializeField]
    Event UpdateStats;

    private void OnEnable()
    {
        UpdateStats.AddListener(UpdateTexts);
    }
    private void OnDisable()
    {
        UpdateStats.RemoveListener(UpdateTexts);
    }

    public void EnableHUD(bool status)
    {
        foreach (GameObject o in HUDElements)
        {
            o.SetActive(status);
        }
    }

    void UpdateTexts() {
        wealthText.text = player.GetWealth().ToString();
        foodText.text = player.GetFood().ToString();
        stabilityText.text = player.GetStability().ToString();
        powerProjectionText.text = player.GetPowerProjection().ToString();
        moraleText.text = player.GetMorale().ToString();
        flairText.text = player.GetFlair().ToString();
        goldText.text = player.GetGold().ToString();
        minionText.text = player.GetMinionCount().ToString();
    }


}
