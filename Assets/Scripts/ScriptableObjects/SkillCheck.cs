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
                if(Player.GetWealth() >= target)
                {
                    Debug.Log("Actual Value of "+ skill.ToString() +": " + Player.GetWealth());
                    return true;
                }
                break;
            case Skill.Food:
                if (Player.GetFood() >= target)
                {
                    Debug.Log("Actual Value of " + skill.ToString() + ": " + Player.GetFood());
                    return true;
                }
                break;
            case Skill.PowerProjection:
                if (Player.GetPowerProjection() >= target)
                {
                    Debug.Log("Actual Value of " + skill.ToString() + ": " + Player.GetPowerProjection());
                    return true;
                }
                break;
            case Skill.Stability:
                if (Player.GetStability() >= target)
                {
                    Debug.Log("Actual Value of " + skill.ToString() + ": " + Player.GetStability());
                    return true;
                }
                break;
            case Skill.Morale:
                if (Player.GetMorale() >= target)
                {
                    Debug.Log("Actual Value of " + skill.ToString() + ": " + Player.GetMorale());
                    return true;
                }
                break;
            case Skill.Flair:
                if (Player.GetFlair() >= target)
                {
                    Debug.Log("Actual Value of " + skill.ToString() + ": " + Player.GetFlair());
                    return true;
                }
                break;
        }
        return false;
    }
}
