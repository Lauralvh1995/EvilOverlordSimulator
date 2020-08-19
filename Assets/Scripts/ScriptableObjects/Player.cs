using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    [SerializeField]
    int Gold = 5;
    [SerializeField]
    List<Minion> Minions;

    public Character character;

    [SerializeField]
    Event DayTick;
    private void OnEnable()
    {
        DayTick.AddListener(AddGold);
    }
    private void OnDisable()
    {
        DayTick.RemoveListener(AddGold);
    }

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

    void AddGold()
    {
        Gold += Wealth;
        Debug.Log("Current Gold: " + Gold);
    }
}
