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

    // Start is called before the first frame update
    void Start()
    {
        MaleNameEvent.OnMaleNameSet += SetMalePlayerName;
        FemaleNameEvent.OnFemaleNameSet += SetFemalePlayerName;
    }

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
            MaleNameEvent.OnMaleNameSet += SetMalePlayerName;
            FemaleNameEvent.OnFemaleNameSet += SetFemalePlayerName;
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
            MaleNameEvent.OnMaleNameSet -= SetMalePlayerName;
            FemaleNameEvent.OnFemaleNameSet -= SetFemalePlayerName;
        }
    }
}
