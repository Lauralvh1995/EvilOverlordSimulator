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

    [SerializeField]
    Event UpdateStats;

    private void Start()
    {
        UpdateTexts();
    }

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
        wealthText.text = PlayerObject.GetWealth().ToString();
        foodText.text = PlayerObject.GetFood().ToString();
        stabilityText.text = PlayerObject.GetStability().ToString();
        powerProjectionText.text = PlayerObject.GetPowerProjection().ToString();
        moraleText.text = PlayerObject.GetMorale().ToString();
        flairText.text = PlayerObject.GetFlair().ToString();
        goldText.text = PlayerObject.GetGold().ToString();
        minionText.text = PlayerObject.GetMinionCount().ToString();
    }


}
