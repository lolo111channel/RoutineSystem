using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TimeManager : MonoBehaviour
{
    public int Hours { get; private set; } = 10;
    public int Minutes { get; private set; } = 0;

    private float _timer = 0.0f;
    private float _delay = 0.2f;

    public delegate void TimeChangeEventHandler(int hours, int minutes);
    public event TimeChangeEventHandler OnTimeChange;

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _delay)
        {
            Minutes += 10;
            if (Minutes >= 60)
            {
                Hours += 1;
                if (Hours >= 24)
                {
                    Hours = 0;
                }

                Minutes = 0;
            }

            OnTimeChange?.Invoke(Hours, Minutes);
            _timer = 0.0f;
        }
    }
}
