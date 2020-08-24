using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class MyIntEvent : UnityEvent<int>
{

}
[CreateAssetMenu(fileName = "New Event", menuName = "Int Event")]
public class IntEvent : Event<int>
{
    public MyIntEvent OnTrigger = new MyIntEvent();

    public override void Invoke(int i)
    {
        OnTrigger.Invoke(i);
    }

    public override void AddListener(UnityAction<int> action)
    {
        OnTrigger.AddListener(action);
    }

    public override void RemoveListener(UnityAction<int> action)
    {
        OnTrigger.RemoveListener(action);
    }
}
