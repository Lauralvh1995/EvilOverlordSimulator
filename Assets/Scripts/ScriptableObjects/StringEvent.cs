using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Event", menuName = "String Event")]
public class StringEvent : Event
{
    public UnityEvent<string> OnStringTrigger;

    public void Invoke(string s)
    {
        OnStringTrigger.Invoke(s);
    }

    public void AddListener(UnityAction<string> action)
    {
        OnStringTrigger.AddListener(action);
        Debug.Log("Added " + action.ToString() + " as listener");
    }

    public void RemoveListener(UnityAction<string> action)
    {
        OnStringTrigger.RemoveListener(action);
        Debug.Log("Removed " + action.ToString() + " as listener");
    }
}
