using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class PlayerObject : MonoBehaviour
{
    public Player player;

    public DialogueController dialogueHolder;
    public HUDController HUD;
    public CameraController cameraController;
    public Selector selector;
    public GameClock gameClock;
    public Grid grid;

    public Conversation defaultConvo;

    [SerializeField] 
    Event maleNameEvent;
    [SerializeField] 
    Event femaleNameEvent;
    [SerializeField]
    Event DialogueStarted;
    [SerializeField]
    Event DialogueEnded;
    [SerializeField]
    Event BuildingBuilt;
    [SerializeField]
    Event MinionRecruited;
    [SerializeField]
    Event UpdateStatsForHUD;
    [SerializeField]
    Event DayTick;

    private void Start()
    {
        selector = GetComponent<Selector>();
        gameClock = GetComponent<GameClock>();
        UpdateStatsForHUD.Invoke();
    }

    private void OnEnable()
    {
        femaleNameEvent.AddListener(SetFemalePlayerName);
        maleNameEvent.AddListener(SetMalePlayerName);
        DialogueStarted.AddListener(OnDialogueStarted);
        DialogueEnded.AddListener(OnDialogueEnded);
        BuildingBuilt.AddListener(UpdateStats);
        MinionRecruited.AddListener(UpdateStats);
        DayTick.AddListener(UpdateStats);
    }
    private void OnDisable()
    {
        femaleNameEvent.RemoveListener(SetFemalePlayerName);
        maleNameEvent.RemoveListener(SetMalePlayerName);
        DialogueStarted.RemoveListener(OnDialogueStarted);
        DialogueEnded.RemoveListener(OnDialogueEnded);
        BuildingBuilt.RemoveListener(UpdateStats);
        MinionRecruited.RemoveListener(UpdateStats);
        DayTick.RemoveListener(UpdateStats);
    }

    void SetMalePlayerName()
    {
        player.character.FullName = "Uther";
        UnityEngine.Debug.Log("Set player name to: " + player.character.FullName);
    }

    void SetFemalePlayerName()
    {
        player.character.FullName = "Bethori";
        UnityEngine.Debug.Log("Set player name to: " + player.character.FullName);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            player.Reset();
            dialogueHolder.conversation = defaultConvo;
        }
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
        player.SetStats(tempWealth, tempMorale, tempFood, tempFlair, tempStability, tempPP);

        UpdateStatsForHUD.Invoke();
    }
}
