using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{

    public void doFight()
    {
        StartCoroutine(LoadCharSelect(1f));
    }

    public void doQuit()
    {
        StartCoroutine(Quit(1f));
    }

    private IEnumerator LoadCharSelect(float time)
    {
        yield return new WaitForSeconds(time);
        Debug.Log("LoadScene");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private IEnumerator Quit(float time)
    {
        yield return new WaitForSeconds(time);
        Debug.Log("Quit");
        Application.Quit();
    }
}
