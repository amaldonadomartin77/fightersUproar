using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOptionsToggleBehavior : MonoBehaviour
{

    private GameObject self;

    private void Awake()
    {
        self = this.gameObject;
    }

    void Start()
    {
        switch(Settings.s.roundTime)
        {
            case 30:
                if (self.name == "30s")
                    self.transform.GetComponent<Toggle>().isOn = true;
                break;
            case 60:
                if (self.name == "60s")
                    self.transform.GetComponent<Toggle>().isOn = true;
                break;
            case 90:
                if (self.name == "90s")
                    self.transform.GetComponent<Toggle>().isOn = true;
                break;
            case 100:
                if (self.name == "Infinite")
                    self.transform.GetComponent<Toggle>().isOn = true;
                break;
        }
        switch (Settings.s.numberOfRounds)
        {
            case 1:
                if (self.name == "1")
                    self.transform.GetComponent<Toggle>().isOn = true;
                break;
            case 3:
                if (self.name == "3")
                    self.transform.GetComponent<Toggle>().isOn = true;
                break;
            case 5:
                if (self.name == "5")
                    self.transform.GetComponent<Toggle>().isOn = true;
                break;
        }
        if (self.name == "MusicOn")
            self.transform.GetComponent<Toggle>().isOn = Settings.s.musicEnabled;
        if (self.name == "MusicOff")
            self.transform.GetComponent<Toggle>().isOn = !(Settings.s.musicEnabled);
        if (self.name == "SoundOn")
            self.transform.GetComponent<Toggle>().isOn = Settings.s.soundEnabled;
        if (self.name == "SoundOff")
            self.transform.GetComponent<Toggle>().isOn = !(Settings.s.soundEnabled);
    }
}
