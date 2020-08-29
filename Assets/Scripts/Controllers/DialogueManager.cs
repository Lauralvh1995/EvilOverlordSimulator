using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    private Character playerCharacter;
    [SerializeField]
    private DialogueController dialogueController;

    [SerializeField]
    Conversation openingConversation;

    [SerializeField]
    Event MonthTick;

    [SerializeField]
    Event MaleNameChosen;
    [SerializeField]
    Event FemaleNameChosen;

    [SerializeField]
    IntEvent YeharaFavorEvent;
    [SerializeField]
    IntEvent MaliceFavorEvent;
    [SerializeField]
    IntEvent VonEckensteinFavorEvent;

    [SerializeField]
    int YeharaFavor = 0;
    [SerializeField]
    int VonEckensteinFavor = 0;
    [SerializeField]
    int MaliceFavor = 0;

    private void OnEnable()
    {
        MaleNameChosen.AddListener(OnMaleNameChosen);
        FemaleNameChosen.AddListener(OnFemaleNameChosen);
        YeharaFavorEvent.AddListener(UpdateYeharaFavor);
        MaliceFavorEvent.AddListener(UpdateMaliceFavor);
        VonEckensteinFavorEvent.AddListener(UpdateVonEckensteinFavor);
    }
    private void OnDisable()
    {
        MaleNameChosen.RemoveListener(OnMaleNameChosen);
        FemaleNameChosen.RemoveListener(OnFemaleNameChosen);
        YeharaFavorEvent.RemoveListener(UpdateYeharaFavor);
        MaliceFavorEvent.RemoveListener(UpdateMaliceFavor);
        VonEckensteinFavorEvent.RemoveListener(UpdateVonEckensteinFavor);
    }
    void Start()
    {
        dialogueController.conversation = openingConversation;
        dialogueController.AdvanceLine();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMaleNameChosen()
    {
        playerCharacter.FullName = "Uther";
    }
    void OnFemaleNameChosen()
    {
        playerCharacter.FullName = "Kaya";
    }

    void UpdateYeharaFavor(int amount)
    {
        YeharaFavor += amount;
    }
    void UpdateMaliceFavor(int amount)
    {
        MaliceFavor += amount;
    }
    void UpdateVonEckensteinFavor(int amount)
    {
        VonEckensteinFavor += amount;
    }
}
