using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timeRemaining;

    public GameController gameController;
    
    private float timeValue;
    private bool disableTimer;

    private void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        timeValue = Settings.s.roundTime;
    }

    void Update()
    {
        if (timeValue >= 0 && timeValue < 99)
        {
            if (gameController.movementAllowed)
                timeValue -= Time.deltaTime;
        }
        //end the match here
        else if (timeValue != 100)
        {
            timeValue = 0;
        }
        DisplayText(timeValue);
    }

    void DisplayText(float timeToDisplay)
    {
        if (disableTimer)
        {
            timeRemaining.text = 0.ToString();
            return;
        }

        if(timeToDisplay == 0)
        {
            timeToDisplay = 0;
        }
        float seconds = Mathf.FloorToInt(timeValue);
        timeRemaining.text = seconds.ToString();

        if (timeToDisplay == 100)
        {
            timeRemaining.text = "\u221e";
        }
    }

    public void ForceDisplay(bool choice)
    {
        disableTimer = choice;
    }

    public float GetTime()
    {
        return timeValue;
    }

    public void ResetTimer()
    {
        timeValue = Settings.s.roundTime;
    }
}
