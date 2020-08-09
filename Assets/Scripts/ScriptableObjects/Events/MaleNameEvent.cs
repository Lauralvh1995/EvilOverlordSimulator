using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "MaleNameEvent", menuName = "Events/MaleNameEvent")]
public class MaleNameEvent : StoryEvent
{
    public delegate void SetMaleName();
    public static event SetMaleName OnMaleNameSet;

    public override void DoSomething()
    {
        if (OnMaleNameSet != null)
            OnMaleNameSet();
    }
}
