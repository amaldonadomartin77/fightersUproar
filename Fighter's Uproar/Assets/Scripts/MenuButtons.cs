using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{

    public void doFight()
    {
        StartCoroutine(LoadCharSelect(1.5f));
    }

    public void doSettings()
    {
        StartCoroutine(LoadSettings(1.5f));
    }

    public void doQuit()
    {
        StartCoroutine(Quit(1.5f));
    }

    private IEnumerator LoadCharSelect(float time)
    {
        yield return new WaitForSeconds(time);
        Debug.Log("LoadScene");
        SceneManager.LoadScene("LevelSelectScene");
    }

    private IEnumerator LoadSettings(float time)
    {
        yield return new WaitForSeconds(time);
        Debug.Log("LoadScene");
        SceneManager.LoadScene("OptionsMenu");
    }

    private IEnumerator Quit(float time)
    {
        yield return new WaitForSeconds(time);
        Debug.Log("Quit");
        Application.Quit();
    }
}
