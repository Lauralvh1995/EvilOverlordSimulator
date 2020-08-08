using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class QuestionEvent : UnityEvent<Question> { }

public class DialogueDisplay : MonoBehaviour
{
    public Conversation conversation;
    public QuestionEvent questionEvent;

    public GameObject speaker;
    public GameObject question;

    private SpeakerUI speakerUI;

    private int activeLineIndex = 0;
    private bool conversationStarted = false;

    public void ChangeConversation(Conversation nextConversation)
    {
        conversationStarted = false;
        conversation = nextConversation;
        AdvanceLine();
    }
    private void Start()
    {
        speakerUI = speaker.GetComponent<SpeakerUI>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (conversation != null)
                AdvanceLine();
        }
    }

    private void EndConversation()
    {
        conversation = null;
        conversationStarted = false;
        speakerUI.Hide();
    }

    private void Initialize()
    {
        conversationStarted = true;
        activeLineIndex = 0;
        speakerUI.Speaker = conversation.lines[activeLineIndex].character;

    }
    private void AdvanceLine()
    {
        if (conversation == null) return;
        if (!conversationStarted) Initialize();
        if (activeLineIndex < conversation.lines.Length)
            DisplayLine();
        else
            AdvanceConversation();
    }

    void AdvanceConversation()
    {
        speakerUI.Hide();
        activeLineIndex = 0;
        //do check if there is a skill check and NO question
        if (conversation.check && !conversation.question)
        {
            if (conversation.check.Pass())
            {
                Debug.Log("Passed");
                //play pass convo
                if (conversation.check.passConvo != null)
                    conversation = conversation.check.passConvo;
            }
            else
            {
                Debug.Log("Failed");
                //play fail convo
                if (conversation.check.failConvo != null)
                    conversation = conversation.check.failConvo;
            }
        }
        //do check if there is a question and NO skill check
        else if (conversation.question && !conversation.check)
        {
            questionEvent.Invoke(conversation.question);
        }
        else
        {
            EndConversation();
        }

    }

    private void DisplayLine()
    {
        Line line = conversation.lines[activeLineIndex];
        Character character = line.character;
        SetDialogue(speakerUI, character, line.text, line.position);
        activeLineIndex++;
    }

    private void SetDialogue(SpeakerUI activeSpeakerUI, Character character, string text, Position position)
    {
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
        activeSpeakerUI.Dialogue = "";
        StopAllCoroutines();
        StartCoroutine(EffectTypewriter(text, activeSpeakerUI));

    }
    private IEnumerator EffectTypewriter(string text, SpeakerUI controller)
    {
        foreach (char character in text.ToCharArray())
        {
            controller.dialogue.text += character;
            // yield return new  WaitForSeconds(0.1f);
            yield return null;
        }
    }
}
