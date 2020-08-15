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

    [SerializeField] private StoryEvent maleNameEvent;
    [SerializeField] private StoryEvent femaleNameEvent;

    void SetMalePlayerName()
    {
        player.character.FullName = "Uther";
    }

    void SetFemalePlayerName()
    {
        player.character.FullName = "Bethori";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            player.character.FullName = "??????";
            femaleNameEvent.AddListener(SetFemalePlayerName);
            maleNameEvent.AddListener(SetMalePlayerName);
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
            femaleNameEvent.RemoveListener(SetFemalePlayerName);
            maleNameEvent.RemoveListener(SetMalePlayerName);
        }
    }
}
