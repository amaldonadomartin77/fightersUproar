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
        timeValue = gameController.maxTime;
    }

    void Update()
    {
        if (timeValue >= 0)
        {
            if (gameController.movementAllowed)
                timeValue -= Time.deltaTime;
        }
        //end the match here
        else
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
        timeValue = gameController.maxTime;
    }
}
