using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public float timeRemaining = 5f;

    public bool timerRunning = false;

    public TMP_Text timeTxt;

    public float firstWarning = 0, lastWarning = 0;

    private bool warn1Presented = false, warn2Presented = false;
    private bool firstToken = false;

    public string warn1, warn2;

    public GameObject warningDisplay;
    public TMP_Text warningDisplayTxt;

    private void Update()
    {
        if (timerRunning)
        {
            if(timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                CheckWarnings();
                DisplayTime();
            }
            else
            {
                timerRunning = false;
                SceneManager.LoadScene(6);
            }
        }
    }

    private void CheckWarnings()
    {
        if(timeRemaining <= firstWarning && !warn1Presented)
        {
            FirstWarning();
        }

        if(timeRemaining <= lastWarning && !warn2Presented)
        {
            LastWarning();
        }
    }
    

    private void FirstWarning()
    {
        //display warning
        warningDisplayTxt.text = warn1;
        DisplayWarning();
        warn1Presented = true;
    }

    private void LastWarning()
    {
        //display warning
        warningDisplayTxt.text = warn2;
        DisplayWarning();
        warn2Presented = true;
    }

    private void DisplayWarning()
    {
        warningDisplay.SetActive(true);
        Invoke("WarningOff", 3f);
    }

    public void PlayerToolTip(string tip)
    {
        warningDisplayTxt.text = tip;
        warningDisplay.SetActive(true);
        Invoke("WarningOff", 3f);
    }

    private void WarningOff()
    {
        warningDisplay.SetActive(false);
        warningDisplayTxt.text = "";
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
        if (!firstToken)
        {
            warningDisplayTxt.text = "You found a tempo token! These delay doomsday just a bit.";
            DisplayWarning();
            firstToken = true;
        }

        timeRemaining += timeToAdd;

        if(timeRemaining > firstWarning)
        {
            warn1Presented = false;
        }

        if(timeRemaining > lastWarning)
        {
            warn2Presented = false;
        }
    }
}
