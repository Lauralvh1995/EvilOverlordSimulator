using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public class QuestionEvent : UnityEvent<Question> { }

public class DialogueController : MonoBehaviour
{
    public Conversation conversation;
    public QuestionEvent questionEvent;

    public GameObject speaker;
    public GameObject question;
    public Image background;

    private SpeakerUIController speakerUI;


    private int activeLineIndex = 0;
    private bool conversationStarted = false;
    private bool inQuestion = false;

    [SerializeField]
    Event DialogueStarted;
    [SerializeField]
    Event DialogueEnded;

    public void ChangeConversation(Conversation nextConversation)
    {
        conversationStarted = false;
        conversation = nextConversation;
        AdvanceLine();
    }
    private void Awake()
    {
        speakerUI = speaker.GetComponent<SpeakerUIController>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !inQuestion)
        {
            if (conversation != null)
                AdvanceLine();
        }
    }

    private void EndConversation()
    {
        conversation = null;
        conversationStarted = false;
        background.gameObject.SetActive(false);
        DialogueEnded.Invoke();
        speakerUI.Hide();
    }

    public bool HasStartedConversation()
    {
        return conversationStarted;
    }

    private void Initialize()
    {
        conversationStarted = true;
        DialogueStarted.Invoke();
        activeLineIndex = 0;
        if(conversation.background != null)
            background.sprite = conversation.background;
        background.gameObject.SetActive(true);
        speakerUI.Speaker = conversation.lines[activeLineIndex].character;
    }
    public void AdvanceLine()
    {
        if (conversation == null) 
        { 
            return; 
        }
        if (!conversationStarted) 
        { 
            Initialize(); 
        }
        if (activeLineIndex < conversation.lines.Length)
        { 
            DisplayLine(); }
        else
        {
            AdvanceConversation();
        }
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

                AdvanceLine();
            }
            else
            {
                Debug.Log("Failed");
                //play fail convo
                if (conversation.check.failConvo != null)
                    conversation = conversation.check.failConvo;

                AdvanceLine();
            }
        }
        //do check if there is a question and NO skill check
        else if (conversation.question && !conversation.check)
        {
            inQuestion = true;
            questionEvent.Invoke(conversation.question);
        }
        else if(conversation.storyEvent != null)
        {
            conversation.storyEvent.Invoke();
        }
        else
        {
            EndConversation();
        }

    }

    public void ExitQuestion()
    {
        inQuestion = false;
    }

    private void DisplayLine()
    {
        Line line = conversation.lines[activeLineIndex];
        Character character = null;
        if (line.character != null)
        {
             character = line.character;
        }
        SetDialogue(speakerUI, character, line.text, line.position, line.emote);
        activeLineIndex++;
    }

    private void SetDialogue(SpeakerUIController activeSpeakerUI, Character character, string text, Position position, Emote emote)
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
        switch (emote)
        {
            case Emote.ANGRY:
                activeSpeakerUI.SetPortrait(character.angryPortrait);
                break;
            case Emote.CONFUSED:
                activeSpeakerUI.SetPortrait(character.confusedPortrait);
                break;
            case Emote.HAPPY:
                activeSpeakerUI.SetPortrait(character.happyPortrait);
                break;
            case Emote.UNHAPPY:
                activeSpeakerUI.SetPortrait(character.unhappyPortrait);
                break;
            case Emote.SAD:
                activeSpeakerUI.SetPortrait(character.sadPortrait);
                break;
            case Emote.SILLY:
                activeSpeakerUI.SetPortrait(character.sillyPortrait);
                break;
            default:
                activeSpeakerUI.SetPortrait(character.portrait);
                break;
        }
        activeSpeakerUI.Show();
        activeSpeakerUI.Dialogue = "";
        StopAllCoroutines();
        StartCoroutine(EffectTypewriter(text, activeSpeakerUI));

    }
    private IEnumerator EffectTypewriter(string text, SpeakerUIController controller)
    {
        foreach (char character in text.ToCharArray())
        {
            controller.dialogue.text += character;
            // yield return new  WaitForSeconds(0.1f);
            yield return null;
        }
    }
}
