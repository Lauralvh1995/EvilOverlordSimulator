using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Event<T> : ScriptableObject
{
    public abstract void Invoke(T t);

    public abstract void AddListener(UnityAction<T> action);

    public abstract void RemoveListener(UnityAction<T> action);
}
