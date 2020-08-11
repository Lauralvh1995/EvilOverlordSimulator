using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "FemaleNameEvent", menuName = "Events/FemaleNameEvent")]
public class FemaleNameEvent : StoryEvent
{
    public delegate void SetFemaleName();
    public static event SetFemaleName OnFemaleNameSet;

    public override void DoSomething()
    {
        if (OnFemaleNameSet != null)
            OnFemaleNameSet();
    }
}
