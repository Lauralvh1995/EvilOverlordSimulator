using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player", menuName = "Player")]
public class Player : ScriptableObject
{
    [SerializeField]
    int Wealth;
    [SerializeField]
    int Food;
    [SerializeField]
    int PowerProjection;
    [SerializeField]
    int Stability;
    [SerializeField]
    int Morale;
    [SerializeField]
    int Flair;

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
