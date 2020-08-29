using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class Player : MonoBehaviour
{
    public static Player instance;

    [SerializeField]
    int Wealth = 1;
    [SerializeField]
    int Food = 1;
    [SerializeField]
    int PowerProjection = 1;
    [SerializeField]
    int Stability = 1;
    [SerializeField]
    int Morale = 0;
    [SerializeField]
    int Flair = 1;

    [SerializeField]
    int Gold = 5;
    [SerializeField]
    List<Minion> Minions;

    public Grid grid;

    [SerializeField]
    IntEvent SubtractGoldCost;
    [SerializeField]
    Event UIUpdate;
    [SerializeField]
    MinionEvent MinionRecruited;
    [SerializeField]
    Event DayTick;
    [SerializeField]
    Event WeekTick;
    [SerializeField]
    IntEvent paidMinions;


    public int emptyCost = -1;
    public int roadCost = 1;
    public int houseCost = 2;
    public int mineCost = 3;
    public int farmCost = 3;
    public int towerCost = 4;
    public int statueCost = 4;
    public int courtCost = 4;

    private bool allowedToBuildCurrentBuilding = false;

    public Building buildMode;
    private void Awake()
    {
        instance = this;
        Minions = new List<Minion>();
    }
    private void Start()
    {
        UIUpdate.Invoke();
    }

    private void OnEnable()
    {
        SubtractGoldCost.AddListener(RemoveGold);
        MinionRecruited.AddListener(AddMinionToList);
        DayTick.AddListener(AddGold);
        WeekTick.AddListener(PayMinions);
    }
    private void OnDisable()
    {
        SubtractGoldCost.RemoveListener(RemoveGold);
        MinionRecruited.RemoveListener(AddMinionToList);
        DayTick.RemoveListener(AddGold);
        WeekTick.RemoveListener(PayMinions);
    }
    public List<Minion> GetMinions()
    {
        return Minions;
    }

    private void Update()
    {
        switch (buildMode)
        {
            case Building.EMPTY:
                allowedToBuildCurrentBuilding = true;
                break;
            case Building.ROAD:
                allowedToBuildCurrentBuilding = Gold >= roadCost;
                break;
            case Building.HOUSE:
                allowedToBuildCurrentBuilding = Gold >= houseCost;
                break;
            case Building.MINE:
                allowedToBuildCurrentBuilding = Gold >= mineCost;
                break;
            case Building.FARM:
                allowedToBuildCurrentBuilding = Gold >= farmCost;
                break;
            case Building.TOWER:
                allowedToBuildCurrentBuilding = Gold >= towerCost;
                break;
            case Building.COURTHOUSE:
                allowedToBuildCurrentBuilding = Gold >= courtCost;
                break;
            case Building.STATUE:
                allowedToBuildCurrentBuilding = Gold >= statueCost;
                break;
            case Building.BASE:
                allowedToBuildCurrentBuilding = false;
                break;
            case Building.NONE:
                allowedToBuildCurrentBuilding = false;
                break;
        }
    }
    void AddGold()
    {
        Gold += Wealth;
        UpdateStats();
    }

    void AddGold(int value)
    {
        Gold += value;
        UpdateStats();
    }
    void RemoveGold(int value)
    {
        Gold -= value;
        UpdateStats();
    }

    public void UpdateStats()
    {
        int tempWealth = 1;
        int tempFood = 1;
        int tempPP = 1;
        int tempStability = 1;
        int tempFlair = 1;

        foreach (Cell c in grid.Cells)
        {
            if (c.IsActive())
            {
                switch (c.GetBuilding().content)
                {
                    case Building.MINE:
                        tempWealth++;
                        break;
                    case Building.FARM:
                        tempFood++;
                        break;
                    case Building.TOWER:
                        tempPP++;
                        break;
                    case Building.STATUE:
                        tempFlair++;
                        break;
                    case Building.COURTHOUSE:
                        tempStability++;
                        break;
                }
            }
        }
        Wealth = tempWealth;
        Food = tempFood;
        Flair = tempFlair;
        Stability = tempStability;
        PowerProjection = tempPP;

        //adding average minion Happiness to Morale.
        if (Minions.Count > 0)
        {
            float minionMorale = 0;
            foreach (Minion m in Minions)
            {
                minionMorale += m.GetHappiness();
            }
            minionMorale = (minionMorale / Minions.Count) * 100f;
            Morale = Mathf.FloorToInt(minionMorale);
        }
        UIUpdate.Invoke();
    }

    public static int GetWealth()
    {
        return instance.Wealth;
    }
    public static int GetFood()
    {
        return instance.Food;
    }
    public static int GetPowerProjection()
    {
        return instance.PowerProjection;
    }
    public static int GetStability()
    {
        return instance.Stability;
    }
    public static int GetMorale()
    {
        return instance.Morale;
    }
    public static int GetFlair()
    {
        return instance.Flair;
    }
    public static int GetGold()
    {
        return instance.Gold;
    }
    public static int GetMinionCount()
    {
        return instance.Minions.Count;
    }

    public void SetBuildMode(Building mode)
    {
        buildMode = mode;
        UIUpdate.Invoke();
    }

    public void SetBuildModeToEmpty()
    {
        SetBuildMode(Building.EMPTY);
    }
    public void SetBuildModeToHouse()
    {
        SetBuildMode(Building.HOUSE);
    }
    public void SetBuildModeToRoad()
    {
        SetBuildMode(Building.ROAD);
    }
    public void SetBuildModeToFarm()
    {
        SetBuildMode(Building.FARM);
    }
    public void SetBuildModeToTower()
    {
        SetBuildMode(Building.TOWER);
    }
    public void SetBuildModeToStatue()
    {
        SetBuildMode(Building.STATUE);
    }
    public void SetBuildModeToCourtHouse()
    {
        SetBuildMode(Building.COURTHOUSE);
    }
    public void SetBuildModeToMine()
    {
        SetBuildMode(Building.MINE);
    }
    public void SetBuildModeToNone()
    {
        SetBuildMode(Building.NONE);
    }

    public bool IsAllowedToBuild()
    {
        return allowedToBuildCurrentBuilding;
    }

    void AddMinionToList(Minion minion)
    {
        Minions.Add(minion);
        UpdateStats();
    }

    void PayMinions()
    {
        if (Gold > Minions.Count)
        {
            Gold -= Minions.Count;
            paidMinions.Invoke(1);
        }
        else
        {
            Gold = 0;
            paidMinions.Invoke(0);
        }
        UpdateStats();
    }
}
