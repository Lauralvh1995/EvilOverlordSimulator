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
    Event BorkAdvice;
    [SerializeField]
    Event BorkPraise;

    [SerializeField]
    int YeharaFavor = 0;
    [SerializeField]
    int VonEckensteinFavor = 0;
    [SerializeField]
    int MaliceFavor = 0;

    [SerializeField]
    Conversation YeharaFavorable;
    [SerializeField]
    Conversation YeharaUnfavorable;
    [SerializeField]
    Conversation MaliceFavorable;
    [SerializeField]
    Conversation MaliceUnfavorable;
    [SerializeField]
    Conversation VonEckensteinFavorable;
    [SerializeField]
    Conversation VonEckensteinUnfavorable;
    [SerializeField]
    Conversation BorkPraiseConvo;
    [SerializeField]
    Conversation BorkAdviceConvo;

    [SerializeField]
    Conversation firstMeeting;

    Conversation nextConversation;

    private void OnEnable()
    {
        MaleNameChosen.AddListener(OnMaleNameChosen);
        FemaleNameChosen.AddListener(OnFemaleNameChosen);
        YeharaFavorEvent.AddListener(UpdateYeharaFavor);
        MaliceFavorEvent.AddListener(UpdateMaliceFavor);
        VonEckensteinFavorEvent.AddListener(UpdateVonEckensteinFavor);
        BorkPraise.AddListener(BorkPraisesYou);
        BorkAdvice.AddListener(BorkGivesAdvice);
        MonthTick.AddListener(AdvanceMonth);
    }
    private void OnDisable()
    {
        MaleNameChosen.RemoveListener(OnMaleNameChosen);
        FemaleNameChosen.RemoveListener(OnFemaleNameChosen);
        YeharaFavorEvent.RemoveListener(UpdateYeharaFavor);
        MaliceFavorEvent.RemoveListener(UpdateMaliceFavor);
        VonEckensteinFavorEvent.RemoveListener(UpdateVonEckensteinFavor);
        BorkPraise.RemoveListener(BorkPraisesYou);
        BorkAdvice.RemoveListener(BorkGivesAdvice);
        MonthTick.RemoveListener(AdvanceMonth);
    }
    void Start()
    {
        dialogueController.ChangeConversation(openingConversation);
    }
    void BorkPraisesYou()
    {
        dialogueController.ChangeConversation(BorkPraiseConvo);
    }
    void BorkGivesAdvice()
    {
        dialogueController.ChangeConversation(BorkAdviceConvo);
    }
    void AdvanceMonth()
    {
        int month = GameClock.GetCurrentMonth();
        string impressedOverlord = GetMostImpressedOverlord();

        if (impressedOverlord.Equals("yehara"))
        {
            if(YeharaFavor > 1)
            {
                nextConversation = YeharaFavorable;
            }
            else
            {
                nextConversation = YeharaUnfavorable;
            }
        }
        if (impressedOverlord.Equals("Malice"))
        {
            if (YeharaFavor > 1)
            {
                nextConversation = MaliceFavorable;
            }
            else
            {
                nextConversation = MaliceUnfavorable;
            }
        }
        if (impressedOverlord.Equals("eckenstein"))
        {
            if (YeharaFavor > 1)
            {
                nextConversation = VonEckensteinFavorable;
            }
            else
            {
                nextConversation = VonEckensteinUnfavorable;
            }
        }

        if (month == 2)
        {
            dialogueController.conversation = nextConversation;
            dialogueController.AdvanceLine();
        }
        else
        {
            dialogueController.conversation = firstMeeting;
            dialogueController.AdvanceLine();
        }
    }

    string GetMostImpressedOverlord()
    {
        List<int> absoluteFavors = new List<int>();
        int absoluteYFavor = Mathf.Abs(YeharaFavor);
        int absoluteEFavor = Mathf.Abs(VonEckensteinFavor);
        int absoluteMFavor = Mathf.Abs(MaliceFavor);
        absoluteFavors.Add(absoluteEFavor);
        absoluteFavors.Add(absoluteMFavor);
        absoluteFavors.Add(absoluteYFavor);

        absoluteFavors.Sort();

        if (absoluteFavors[0].Equals(absoluteYFavor))
        {
            return "yehara";
        }
        if (absoluteFavors[0].Equals(absoluteMFavor))
        {
            return "malice";
        }
        if (absoluteFavors[0].Equals(absoluteEFavor))
        {
            return "eckenstein";
        }
        else
        {
            return "none";
        }
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
