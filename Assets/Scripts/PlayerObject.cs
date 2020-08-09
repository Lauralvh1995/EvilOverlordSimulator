using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : MonoBehaviour
{
    public Player player;
    // Start is called before the first frame update
    void Start()
    {
        MaleNameEvent.OnMaleNameSet += SetMalePlayerName;
        FemaleNameEvent.OnFemaleNameSet += SetFemalePlayerName;
    }

    void SetMalePlayerName()
    {
        player.character.FullName = "Uther";
    }

    void SetFemalePlayerName()
    {
        player.character.FullName = "Bethori";
    }
}
