using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeManager : MonoBehaviour
{
    public static Action OnSecondChanged;
    public static Action OnMinuteChanged;

    public static int Second{get; private set;}
    public static int Minute{get;private set;}

    private float secondToRealTime = 1f;
    private float timer;

    void Start()
    {
        Second = 0;
        Minute = 0;
        timer = secondToRealTime;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            Second++;

            OnSecondChanged?.Invoke();

            if(Second >= 60)
            {
                Second = 0;
                Minute++;
                OnMinuteChanged?.Invoke();
                
            }

            timer = secondToRealTime;
        }
    }
}