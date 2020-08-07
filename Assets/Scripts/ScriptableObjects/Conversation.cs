using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Position{
    LEFT,
    RIGHT,
    CENTER
}
[System.Serializable]
public struct Line
{
    public Character character;
    public Position position;
    [TextArea(2, 5)]
    public string text;

}
[CreateAssetMenu(fileName = "New Conversation", menuName = "Conversation")]
public class Conversation : ScriptableObject
{
    public Line[] lines;

    public SkillCheck check;
    public Conversation passConvo;
    public Conversation failConvo;
}
