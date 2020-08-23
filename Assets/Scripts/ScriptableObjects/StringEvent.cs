using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Event", menuName = "String Event")]
public class StringEvent : Event<string>
{
    public UnityEvent<string> OnTrigger;

    public override void Invoke(string s)
    {
        OnTrigger.Invoke(s);
    }

    public override void AddListener(UnityAction<string> action)
    {
        OnTrigger.AddListener(action);
    }

    public override void RemoveListener(UnityAction<string> action)
    {
        OnTrigger.RemoveListener(action);
    }
}
