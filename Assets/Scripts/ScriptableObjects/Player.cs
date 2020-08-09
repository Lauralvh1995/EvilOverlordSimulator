using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player", menuName = "Player")]
public class Player : ScriptableObject
{
    [SerializeField]
    int Wealth = 1;
    [SerializeField]
    int Food = 1;
    [SerializeField]
    int PowerProjection = 1;
    [SerializeField]
    int Stability = 1;
    [SerializeField]
    int Morale = 1;
    [SerializeField]
    int Flair = 1;

    public Character character;

    public int GetWealth()
    {
        return Wealth;
    }
    public int GetFood()
    {
        return Food;
    }
    public int GetPowerProjection()
    {
        return PowerProjection;
    }
    public int GetStability()
    {
        return Stability;
    }
    public int GetMorale()
    {
        return Morale;
    }
    public int GetFlair()
    {
        return Flair;
    }

}
