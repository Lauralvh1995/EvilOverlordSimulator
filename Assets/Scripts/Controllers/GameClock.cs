using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClock : MonoBehaviour
{

    [SerializeField]
    int dayLengthRealTime;
    [SerializeField]
    Event dayTick;
    [SerializeField]
    Event weekTick;
    [SerializeField]
    Event monthTick;

    private int day;
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
        if (day % 7 == 0 && day != 0)
        {
            weekTick.Invoke();
        }

        if (day % 28 == 0 && day != 0)
        {
            monthTick.Invoke();
            day = 0;
        }
    }

    public void Pause(bool status)
    {
        paused = status;
    }
}
