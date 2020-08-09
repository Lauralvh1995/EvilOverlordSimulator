using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StoryEvent : ScriptableObject
{
    public delegate void EventHappens();
    public static event EventHappens OnEventHappen;

    public abstract void DoSomething();
}
