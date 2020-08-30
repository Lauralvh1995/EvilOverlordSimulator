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
    private Canvas VictoryScreen;
    [SerializeField]
    private Canvas GameOverScreen;

    [SerializeField]
    Conversation openingConversation;

    [SerializeField]
    Event MonthTick;

    [SerializeField]
    Event MaleNameChosen;
    [SerializeField]
    Event FemaleNameChosen;
    [SerializeField]
    Event Round2Proper;

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
    Event EckensteinEnding;
    [SerializeField]
    Event MaliceEnding;
    [SerializeField]
    Event YeharaEnding;

    [SerializeField]
    Event Victory;
    [SerializeField]
    Event Defeat;

    [SerializeField]
    int YeharaFavor = 0;
    [SerializeField]
    int VonEckensteinFavor = 0;
    [SerializeField]
    int MaliceFavor = 0;

    [SerializeField]
    int EndingThreshold = 3;

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
    Conversation YeharaGood;
    [SerializeField]
    Conversation YeharaBad;
    [SerializeField]
    Conversation EckensteinGood;
    [SerializeField]
    Conversation EckensteinBad;
    [SerializeField]
    Conversation MaliceGood;
    [SerializeField]
    Conversation MaliceBad;

    [SerializeField]
    Conversation firstMeeting;
    [SerializeField]
    Conversation secondMeeting;

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
        Round2Proper.AddListener(OnSecondMeeting);
        YeharaEnding.AddListener(OnYeharaEnding);
        EckensteinEnding.AddListener(OnEckensteinEnding);
        MaliceEnding.AddListener(OnMaliceEnding);
        Victory.AddListener(OnVictory);
        Defeat.AddListener(OnDefeat);
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
        Round2Proper.RemoveListener(OnSecondMeeting);
        YeharaEnding.RemoveListener(OnYeharaEnding);
        EckensteinEnding.RemoveListener(OnEckensteinEnding);
        MaliceEnding.RemoveListener(OnMaliceEnding);
        Victory.RemoveListener(OnVictory);
        Defeat.RemoveListener(OnDefeat);
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
            dialogueController.ChangeConversation(nextConversation);
        }
        else
        {
            dialogueController.ChangeConversation(firstMeeting);
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

        if (absoluteFavors[2].Equals(absoluteYFavor))
        {
            return "yehara";
        }
        if (absoluteFavors[2].Equals(absoluteMFavor))
        {
            return "malice";
        }
        if (absoluteFavors[2].Equals(absoluteEFavor))
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

    void OnSecondMeeting()
    {
        dialogueController.ChangeConversation(secondMeeting);
    }

    void OnYeharaEnding()
    {
        if(YeharaFavor >= EndingThreshold)
        {
            dialogueController.ChangeConversation(YeharaGood);
        }
        else
        {
            dialogueController.ChangeConversation(YeharaBad);
        }
    }
    void OnMaliceEnding()
    {
        if (MaliceFavor >= EndingThreshold)
        {
            dialogueController.ChangeConversation(MaliceGood);
        }
        else
        {
            dialogueController.ChangeConversation(MaliceBad);
        }
    }
    void OnEckensteinEnding()
    {
        if (VonEckensteinFavor >= EndingThreshold)
        {
            dialogueController.ChangeConversation(EckensteinGood);
        }
        else
        {
            dialogueController.ChangeConversation(EckensteinBad);
        }
    }

    void OnVictory()
    {
        VictoryScreen.gameObject.SetActive(true);
    }
    void OnDefeat()
    {
        GameOverScreen.gameObject.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
