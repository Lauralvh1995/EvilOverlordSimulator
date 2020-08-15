﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Event", menuName = "Event")]
public class StoryEvent : ScriptableObject
{
    public UnityEvent OnTrigger;

    public void Invoke()
    {
        OnTrigger.Invoke();
    }

    public void AddListener(UnityAction action)
    {
        OnTrigger.AddListener(action);
    }

    public void RemoveListener(UnityAction action)
    {
        OnTrigger.RemoveListener(action);
    }
}
