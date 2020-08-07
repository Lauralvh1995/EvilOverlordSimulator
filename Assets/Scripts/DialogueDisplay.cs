using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueDisplay : MonoBehaviour
{
    public Conversation conversation;

    public GameObject speaker;

    private SpeakerUI speakerUI;

    private int activeLineIndex = 0;
    private void Start()
    {
        speakerUI = speaker.GetComponent<SpeakerUI>();

        if(conversation.lines.Length > 0)
            speakerUI.Speaker = conversation.lines[activeLineIndex].character;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(conversation != null)
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
            speakerUI.Hide();
            activeLineIndex = 0;
            //do check if there is one
            if (conversation.check)
            {
                if (conversation.check.Pass())
                {
                    //play pass convo
                    if (conversation.check.passConvo != null)
                        conversation = conversation.check.passConvo;
                }
                else
                {
                    //play fail convo
                    if (conversation.check.failConvo != null)
                        conversation = conversation.check.failConvo;
                }
            }
            
        }
    }

    private void DisplayLine()
    {
        Line line = conversation.lines[activeLineIndex];
        Character character = line.character;
        SetDialogue(speakerUI, character, line.text, line.position);
    }

    private void SetDialogue(SpeakerUI activeSpeakerUI, Character character, string text, Position position)
    {
        activeSpeakerUI.Dialogue = text;
        activeSpeakerUI.Speaker = character;
        switch (position)
        {
            case Position.LEFT:
                activeSpeakerUI.SetLeft();
                break;
            case Position.RIGHT:
                activeSpeakerUI.SetRight();
                break;
            case Position.CENTER:
                activeSpeakerUI.SetCenter();
                break;
            default:
                activeSpeakerUI.SetLeft();
                break;
        }
        activeSpeakerUI.Show();
    }
}
