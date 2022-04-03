using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOptions : MonoBehaviour
{
    public void SetTime30(bool toggle)
    {
        if (toggle)
            Settings.s.roundTime = 30;
    }

    public void SetTime60(bool toggle)
    {
        if (toggle)
            Settings.s.roundTime = 60;
    }

    public void SetTime90(bool toggle)
    {
        if (toggle)
            Settings.s.roundTime = 90;
    }

    public void SetTimeInfinite(bool toggle)
    {
        if (toggle)
            Settings.s.roundTime = 100;
    }

    public void SetOneRound(bool toggle)
    {
        if (toggle)
            Settings.s.numberOfRounds = 1;
    }

    public void SetThreeRounds(bool toggle)
    {
        if (toggle)
            Settings.s.numberOfRounds = 3;
    }

    public void SetFiveRounds(bool toggle)
    {
        if (toggle)
            Settings.s.numberOfRounds = 5;
    }

    public void SetMusic(bool toggle)
    {
        if (toggle)
            Settings.s.musicEnabled = true;
        else
            Settings.s.musicEnabled = false;
    }

    public void SetSound(bool toggle)
    {
        if (toggle)
            Settings.s.soundEnabled = true;
        else
            Settings.s.soundEnabled = false;
    }

    public void ReturnToTitle()
    {
        StartCoroutine(LoadPrevScene());
    }

    private IEnumerator LoadPrevScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("MainMenu");
    }
}
