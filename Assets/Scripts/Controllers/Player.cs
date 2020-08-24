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
    IntEvent BuildingBuilt;
    [SerializeField]
    Event MinionRecruited;
    [SerializeField]
    Event DayTick;
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
        BuildingBuilt.AddListener(RemoveGold);
        MinionRecruited.AddListener(UpdateStats);
        DayTick.AddListener(AddGold);
        //DayTick.AddListener(UpdateStats);
    }
    private void OnDisable()
    {
        femaleNameEvent.RemoveListener(SetFemalePlayerName);
        maleNameEvent.RemoveListener(SetMalePlayerName);
        DialogueStarted.RemoveListener(OnDialogueStarted);
        DialogueEnded.RemoveListener(OnDialogueEnded);
        BuildingBuilt.RemoveListener(RemoveGold);
        MinionRecruited.RemoveListener(UpdateStats);
        DayTick.RemoveListener(AddGold);
        //DayTick.RemoveListener(UpdateStats);

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
        UnityEngine.Debug.Log("Current Gold: " + Gold);
        HUD.UpdateTexts();
    }

    void AddGold(int value)
    {
        Gold += value;
        UnityEngine.Debug.Log("Current Gold: " + Gold);
        HUD.UpdateTexts();
    }
    void RemoveGold(int value)
    {
        Gold -= value;
        UnityEngine.Debug.Log("Current Gold: " + Gold);
        HUD.UpdateTexts();
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
}
