using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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


    public bool Pass()
    {
        switch (skill)
        {
            case Skill.Wealth:
                if(player.GetWealth() >= target)
                {
                    return true;
                }
                break;
            case Skill.Food:
                if (player.GetFood() >= target)
                {
                    return true;
                }
                break;
            case Skill.PowerProjection:
                if (player.GetPowerProjection() >= target)
                {
                    return true;
                }
                break;
            case Skill.Stability:
                if (player.GetStability() >= target)
                {
                    return true;
                }
                break;
            case Skill.Morale:
                if (player.GetMorale() >= target)
                {
                    return true;
                }
                break;
            case Skill.Flair:
                if (player.GetFlair() >= target)
                {
                    return true;
                }
                break;
        }
        return false;
    }
}
