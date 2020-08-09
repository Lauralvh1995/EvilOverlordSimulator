using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : MonoBehaviour
{
    public Player player;

    public GameObject dialogueHolder;
    public Conversation defaultConvo;
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


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            player.character.FullName = "??????";
            MaleNameEvent.OnMaleNameSet -= SetMalePlayerName;
            FemaleNameEvent.OnFemaleNameSet -= SetFemalePlayerName;

            dialogueHolder.GetComponent<DialogueDisplay>().conversation = defaultConvo;
        }
    }
}
