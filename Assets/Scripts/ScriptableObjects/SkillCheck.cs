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
                if(PlayerObject.GetWealth() >= target)
                {
                    Debug.Log("Actual Value of "+ skill.ToString() +": " + PlayerObject.GetWealth());
                    return true;
                }
                break;
            case Skill.Food:
                if (PlayerObject.GetFood() >= target)
                {
                    Debug.Log("Actual Value of " + skill.ToString() + ": " + PlayerObject.GetFood());
                    return true;
                }
                break;
            case Skill.PowerProjection:
                if (PlayerObject.GetPowerProjection() >= target)
                {
                    Debug.Log("Actual Value of " + skill.ToString() + ": " + PlayerObject.GetPowerProjection());
                    return true;
                }
                break;
            case Skill.Stability:
                if (PlayerObject.GetStability() >= target)
                {
                    Debug.Log("Actual Value of " + skill.ToString() + ": " + PlayerObject.GetStability());
                    return true;
                }
                break;
            case Skill.Morale:
                if (PlayerObject.GetMorale() >= target)
                {
                    Debug.Log("Actual Value of " + skill.ToString() + ": " + PlayerObject.GetMorale());
                    return true;
                }
                break;
            case Skill.Flair:
                if (PlayerObject.GetFlair() >= target)
                {
                    Debug.Log("Actual Value of " + skill.ToString() + ": " + PlayerObject.GetFlair());
                    return true;
                }
                break;
        }
        return false;
    }
}
