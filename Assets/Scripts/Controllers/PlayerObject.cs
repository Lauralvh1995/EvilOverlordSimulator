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

    public Conversation defaultConvo;

    [SerializeField] private Event maleNameEvent;
    [SerializeField] private Event femaleNameEvent;
    [SerializeField]
    Event DialogueStarted;
    [SerializeField]
    Event DialogueEnded;

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
    }
    private void OnDisable()
    {
        femaleNameEvent.RemoveListener(SetFemalePlayerName);
        maleNameEvent.RemoveListener(SetMalePlayerName);
        DialogueStarted.RemoveListener(OnDialogueStarted);
        DialogueEnded.RemoveListener(OnDialogueEnded);
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
            player.character.FullName = "??????";
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
}
