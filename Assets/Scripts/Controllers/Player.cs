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
    int Morale = 1;
    [SerializeField]
    int Flair = 1;

    [SerializeField]
    int Gold = 5;
    [SerializeField]
    List<Minion> Minions;

    public DialogueController dialogueHolder;
    public HUDController HUD;
    public CameraController cameraController;
    public Selector selector;
    public GameClock gameClock;
    public Grid grid;

    public Conversation defaultConvo;

    public Character character;

    [SerializeField] 
    Event maleNameEvent;
    [SerializeField] 
    Event femaleNameEvent;
    [SerializeField]
    Event DialogueStarted;
    [SerializeField]
    Event DialogueEnded;
    [SerializeField]
    IntEvent SubtractGoldCost;
    [SerializeField]
    Event UIUpdate;
    [SerializeField]
    Event MinionRecruited;
    [SerializeField]
    Event DayTick;

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
    }
    private void Start()
    {
        selector = GetComponent<Selector>();
        gameClock = GetComponent<GameClock>();
    }

    private void OnEnable()
    {
        femaleNameEvent.AddListener(SetFemalePlayerName);
        maleNameEvent.AddListener(SetMalePlayerName);
        DialogueStarted.AddListener(OnDialogueStarted);
        DialogueEnded.AddListener(OnDialogueEnded);
        UIUpdate.AddListener(UpdateStats);
        SubtractGoldCost.AddListener(RemoveGold);
        MinionRecruited.AddListener(UpdateStats);
        DayTick.AddListener(AddGold);
    }
    private void OnDisable()
    {
        femaleNameEvent.RemoveListener(SetFemalePlayerName);
        maleNameEvent.RemoveListener(SetMalePlayerName);
        DialogueStarted.RemoveListener(OnDialogueStarted);
        DialogueEnded.RemoveListener(OnDialogueEnded);
        UIUpdate.RemoveListener(UpdateStats);
        SubtractGoldCost.RemoveListener(RemoveGold);
        MinionRecruited.RemoveListener(UpdateStats);
        DayTick.RemoveListener(AddGold);

    }

    void SetMalePlayerName()
    {
        character.FullName = "Uther";
        UnityEngine.Debug.Log("Set player name to: " + character.FullName);
    }

    void SetFemalePlayerName()
    {
        character.FullName = "Bethori";
        UnityEngine.Debug.Log("Set player name to: " + character.FullName);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Reset();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

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
        }
    }
    private void Reset()
    {
        character.FullName = "??????";
        Wealth = 1;
        Food = 1;
        PowerProjection = 1;
        Stability = 1;
        Morale = 1;
        Flair = 1;
        Gold = 5;
        dialogueHolder.conversation = defaultConvo;
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

    void OnDialogueStarted()
    {
        HUD.EnableHUD(false);
        cameraController.EnableMovement(false);
        selector.SetAllowed(false);
        gameClock.Pause(true);
    }
    void OnDialogueEnded()
    {
        HUD.EnableHUD(true);
        selector.SetAllowed(true);
        cameraController.EnableMovement(true);
        gameClock.Pause(false);
    }

    void UpdateStats()
    {
        UnityEngine.Debug.Log("Updating stats");
        int tempWealth = 1;
        int tempFood = 1;
        int tempPP = 1;
        int tempStability = 1;
        int tempMorale = 1;
        int tempFlair = 1;

        foreach(Cell c in grid.Cells)
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
                    case Building.HOUSE:
                        tempMorale++;
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
        Morale = tempMorale;
        Food = tempFood;
        Flair = tempFlair;
        Stability = tempStability;
        PowerProjection = tempPP;
        HUD.UpdateButtons();
        HUD.UpdateTexts();
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

    public bool IsAllowedToBuild()
    {
        return allowedToBuildCurrentBuilding;
    }
}
