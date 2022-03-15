using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timeRemaining;
    public float timeValue = 99;

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
}
