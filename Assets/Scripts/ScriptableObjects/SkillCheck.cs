using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public enum Skill
{
    Wealth,
    Food,
    PowerProjection,
    Stability,
    Morale,
    Flair
}
[CreateAssetMenu(fileName = "New Skill Check", menuName = "Skill Check")]
public class SkillCheck : ScriptableObject
{
    public string checkName;
    public Player player;
    public Skill skill;
    public int target;

    public Conversation passConvo;
    public Conversation failConvo;

    public bool Pass()
    {
        Debug.Log("Checking skill: " + skill.ToString() + ". Target value: " + target);
        switch (skill)
        {
            case Skill.Wealth:
                if(player.GetWealth() >= target)
                {
                    Debug.Log("Actual Value of "+ skill.ToString() +": " + player.GetWealth());
                    return true;
                }
                break;
            case Skill.Food:
                if (player.GetFood() >= target)
                {
                    Debug.Log("Actual Value of " + skill.ToString() + ": " + player.GetFood());
                    return true;
                }
                break;
            case Skill.PowerProjection:
                if (player.GetPowerProjection() >= target)
                {
                    Debug.Log("Actual Value of " + skill.ToString() + ": " + player.GetPowerProjection());
                    return true;
                }
                break;
            case Skill.Stability:
                if (player.GetStability() >= target)
                {
                    Debug.Log("Actual Value of " + skill.ToString() + ": " + player.GetStability());
                    return true;
                }
                break;
            case Skill.Morale:
                if (player.GetMorale() >= target)
                {
                    Debug.Log("Actual Value of " + skill.ToString() + ": " + player.GetMorale());
                    return true;
                }
                break;
            case Skill.Flair:
                if (player.GetFlair() >= target)
                {
                    Debug.Log("Actual Value of " + skill.ToString() + ": " + player.GetFlair());
                    return true;
                }
                break;
        }
        return false;
    }
}
