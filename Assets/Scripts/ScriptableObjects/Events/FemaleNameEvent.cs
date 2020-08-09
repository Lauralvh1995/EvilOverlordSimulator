using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "FemaleNameEvent", menuName = "Events/FemaleNameEvent")]
public class FemaleNameEvent : StoryEvent
{
    public delegate void SetMaleName();
    public static event SetMaleName OnFemaleNameSet;

    public override void DoSomething()
    {
        if (OnFemaleNameSet != null)
            OnFemaleNameSet();
    }
}
