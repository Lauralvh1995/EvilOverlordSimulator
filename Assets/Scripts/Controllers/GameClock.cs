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
    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer > dayLengthRealTime)
        {
            timer = 0f;
            day++;
            Debug.Log("Day: " + day);
            dayTick.Invoke();
        }
        if (day % 7 == 0)
        {
            weekTick.Invoke();
        }

        if(day % 28 == 0)
        {
            monthTick.Invoke();
            day = 0;
        }
    }
}
