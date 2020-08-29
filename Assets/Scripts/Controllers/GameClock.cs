using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClock : MonoBehaviour
{
    [SerializeField]
    Text DayBox;
    [SerializeField]
    Text MonthBox;
    [SerializeField]
    Text WeekBox;

    [SerializeField]
    int dayLengthRealTime;
    [SerializeField]
    Event dayTick;
    [SerializeField]
    Event weekTick;
    [SerializeField]
    Event monthTick;

    private static int day = 0;
    private static int month = 0;
    private static int week = 0;

    private float timer = 0f;

    private bool paused;

    [SerializeField]
    Event dialogueStarted;
    [SerializeField]
    Event dialogueEnded;

    private void OnEnable()
    {
        dialogueStarted.AddListener(Pause);
        dialogueEnded.AddListener(UnPause);
    }
    private void OnDisable()
    {
        dialogueStarted.RemoveListener(Pause);
        dialogueEnded.RemoveListener(UnPause);
    }

    private void Pause()
    {
        paused = true;
    }
    void UnPause()
    {
        paused = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(!paused)
            timer += Time.deltaTime;

        if(timer > dayLengthRealTime)
        {
            timer = 0f;
            AdvanceDay();
        }
        
    }
    void AdvanceDay()
    {
        day++;
        dayTick.Invoke();
        if (day == 7)
        {
            week++;
            day = 0;
            weekTick.Invoke();
        }

        if (week == 4)
        {
            month++;
            week = 0;
            monthTick.Invoke();
        }
        UpdateDateTexts();
    }
    void UpdateDateTexts()
    {
        int finalDay = day + 1;
        switch (finalDay)
        {
            case 1:
                DayBox.text = finalDay.ToString() + "st";
                break;
            case 2:
                DayBox.text = finalDay.ToString() + "nd";
                break;
            case 3:
                DayBox.text = finalDay.ToString() + "rd";
                break;
            default:
                DayBox.text = finalDay.ToString() + "th";
                break;
        }
        int finalWeek = week + 1;
        switch (finalWeek)
        {
            case 1:
                WeekBox.text = "of New Moon";
                break;
            case 2:
                WeekBox.text = "of Waxing Moon";
                break;
            case 3:
                WeekBox.text = "of Full Moon";
                break;
            case 4:
                WeekBox.text = "of Waning Moon";
                break;
        }
        int finalMonth = month + 1;
        switch(finalMonth)
        {
            case 1:
                MonthBox.text = "of Growing";
                break;
            case 2:
                MonthBox.text = "of Building";
                break;
        }

    }

    public void Pause(bool status)
    {
        paused = status;
    }

    public static int GetCurrentMonth()
    {
        return month;
    }
    public static int GetCurrentWeek()
    {
        return week;
    }
    public static int GetCurrentDay()
    {
        return day + 1;
    }
}
