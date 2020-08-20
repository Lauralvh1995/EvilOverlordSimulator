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
            day++;
            Debug.Log("Day: " + day);
            dayTick.Invoke();
        }
        if (day % 7 == 0 && day != 0)
        {
            weekTick.Invoke();
            Debug.Log("A week has passed");
        }

        if(day % 28 == 0 && day != 0)
        {
            monthTick.Invoke();
            Debug.Log("A Month has passed");
            day = 0;
        }
    }

    public void Pause(bool status)
    {
        paused = status;
    }
}
