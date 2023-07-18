using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timeRemaining = 5f;

    public bool timerRunning = false;

    public TMP_Text timeTxt;

    private void Update()
    {
        if (timerRunning)
        {
            if(timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime();
            }
            else
            {
                print("Out of time");
                timerRunning = false;
            }
        }
    }

    private void DisplayTime()
    {
        float min = Mathf.FloorToInt(timeRemaining / 60f);
        float sec = Mathf.FloorToInt(timeRemaining % 60f);
        timeTxt.text = string.Format("{0:00}:{1:00}", min, sec);
    }

    public void StartTimer()
    {
        timerRunning = true;
    }

    public void StopTimer()
    {
        timerRunning = false;
    }

    public void AddTime(float timeToAdd)
    {
        timeRemaining += timeToAdd;
    }
}
