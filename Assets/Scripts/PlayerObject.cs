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

    public DialogueDisplay dialogueHolder;
    public GameObject HUD;
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
            HUD.gameObject.SetActive(false);
        }
        else
        {
            HUD.gameObject.SetActive(true);
            MaleNameEvent.OnMaleNameSet -= SetMalePlayerName;
            FemaleNameEvent.OnFemaleNameSet -= SetFemalePlayerName;
        }
    }
}
