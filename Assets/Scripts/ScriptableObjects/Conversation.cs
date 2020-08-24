using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Position{
    LEFT,
    RIGHT,
    CENTER
}
public enum Emote
{
    NEUTRAL,
    HAPPY,
    UNHAPPY,
    ANGRY,
    SAD,
    CONFUSED,
    SILLY
}
[System.Serializable]
public struct Line
{
    public Character character;
    public Position position;
    public Emote emote;
    [TextArea(2, 5)]
    public string text;
}
[CreateAssetMenu(fileName = "New Conversation", menuName = "Conversation")]
public class Conversation : ScriptableObject
{
    public string conversationName;

    public Line[] lines;

    public SkillCheck check;
    public Question question;
    public Event storyEvent;
    public Sprite background;
}
