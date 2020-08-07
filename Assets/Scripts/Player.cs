using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
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

    Character character;

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
