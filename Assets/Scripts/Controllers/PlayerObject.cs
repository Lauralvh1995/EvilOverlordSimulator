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

    public Conversation defaultConvo;

    [SerializeField] private Event maleNameEvent;
    [SerializeField] private Event femaleNameEvent;

    private void OnEnable()
    {
        femaleNameEvent.AddListener(SetFemalePlayerName);
        maleNameEvent.AddListener(SetMalePlayerName);
    }
    private void OnDisable()
    {
        femaleNameEvent.RemoveListener(SetFemalePlayerName);
        maleNameEvent.RemoveListener(SetMalePlayerName);
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

        if (dialogueHolder.HasStartedConversation())
        {
            HUD.EnableHUD(false);
            cameraController.EnableMovement(false);
            selector.setAllowed(false);
        }
        else
        {
            HUD.EnableHUD(true);
            selector.setAllowed(true);
            cameraController.EnableMovement(true);
        }
    }
}
