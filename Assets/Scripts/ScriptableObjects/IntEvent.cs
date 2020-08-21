﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Event", menuName = "Int Event")]
public class IntEvent : Event<int>
{
    public UnityEvent<int> OnTrigger;

    public override void Invoke(int i)
    {
        OnTrigger.Invoke(i);
    }

    public override void AddListener(UnityAction<int> action)
    {
        OnTrigger.AddListener(action);
        Debug.Log("Added " + action.ToString() + " as listener");
    }

    public override void RemoveListener(UnityAction<int> action)
    {
        OnTrigger.RemoveListener(action);
        Debug.Log("Removed " + action.ToString() + " as listener");
    }
}