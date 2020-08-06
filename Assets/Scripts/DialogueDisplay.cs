using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueDisplay : MonoBehaviour
{
    //TODO: rewrite for any amount of conversation members
    public Conversation conversation;

    public GameObject speakerLeft;
    public GameObject speakerRight;

    private SpeakerUI speakerUILeft;
    private SpeakerUI speakerUIRight;

    private int activeLineIndex = 0;
    private void Start()
    {
        speakerUILeft = speakerLeft.GetComponent<SpeakerUI>();
        speakerUIRight = speakerRight.GetComponent<SpeakerUI>();

        speakerUILeft.Speaker = conversation.speakerLeft;
        speakerUIRight.Speaker = conversation.speakerRight;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AdvanceConversation();
        }
    }

    void AdvanceConversation()
    {
        if(activeLineIndex < conversation.lines.Length)
        {
            DisplayLine();
            activeLineIndex++;
        }
        else
        {
            speakerUILeft.Hide();
            speakerUIRight.Hide();
            activeLineIndex = 0;
        }
    }

    private void DisplayLine()
    {
        Line line = conversation.lines[activeLineIndex];
        Character character = line.character;

        if (speakerUILeft.SpeakerIs(character))
        {
            SetDialogue(speakerUILeft, speakerUIRight, line.text);
        }
        else
        {
            SetDialogue(speakerUIRight, speakerUILeft, line.text);
        }
    }

    private void SetDialogue(SpeakerUI activeSpeakerUI, SpeakerUI inactiveSpeakerUI, string text)
    {
        activeSpeakerUI.Dialogue = text;
        activeSpeakerUI.Show();
        inactiveSpeakerUI.Hide();
    }
}
