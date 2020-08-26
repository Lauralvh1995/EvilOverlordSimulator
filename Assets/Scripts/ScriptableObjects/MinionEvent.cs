using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class MyMinionEvent : UnityEvent<Minion>
{

}
[CreateAssetMenu(fileName = "New Event", menuName = "Minion Event")]
public class MinionEvent : Event<Minion>
{
    public MyMinionEvent OnTrigger = new MyMinionEvent();

    public override void Invoke(Minion m)
    {
        OnTrigger.Invoke(m);
    }

    public override void AddListener(UnityAction<Minion> action)
    {
        OnTrigger.AddListener(action);
    }

    public override void RemoveListener(UnityAction<Minion> action)
    {
        OnTrigger.RemoveListener(action);
    }
}
