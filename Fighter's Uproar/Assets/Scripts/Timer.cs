using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timeRemaining;
    public GameController gameController;
    
    private float timeValue;

    private void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        timeValue = gameController.maxTime;
    }

    void Update()
    {
        if (timeValue >= 0)
        {
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
        if(timeToDisplay == 0)
        {
            timeToDisplay = 0;
        }
        float seconds = Mathf.FloorToInt(timeValue);
        timeRemaining.text = seconds.ToString();
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
