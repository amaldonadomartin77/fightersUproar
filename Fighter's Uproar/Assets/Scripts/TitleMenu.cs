using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TitleMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public TextMeshProUGUI titleName, subTitle, pressStart;
    public AudioSource audioSource;
    public AudioClip soundClip;

    private bool pressed = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeInText(1f, pressStart));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !pressed)
        {
            audioSource.PlayOneShot(soundClip);
            StartCoroutine(FadeOutText(1f, pressStart));
            StartCoroutine(FadeOutLogo(0.8f, titleName, subTitle));
            pressed = true;
        }
    }

    private IEnumerator FadeInText(float timeSpeed, TextMeshProUGUI text)
    {
        yield return new WaitForSeconds(1);
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
        while (text.color.a < 0.75f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + (Time.deltaTime * timeSpeed));
            yield return null;
        }
    }
    private IEnumerator FadeOutText(float timeSpeed, TextMeshProUGUI text)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
        while (text.color.a > 0.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime * timeSpeed));
            yield return null;
        }
    }

    private IEnumerator FadeOutLogo(float timeSpeed, TextMeshProUGUI title, TextMeshProUGUI subTitle)
    {
        yield return new WaitForSeconds(0.5f);
        title.color = new Color(title.color.r, title.color.g, title.color.b, 1);
        subTitle.color = new Color(subTitle.color.r, subTitle.color.g, subTitle.color.b, 1);
        while (title.color.a > 0.0f)
        {
            title.color = new Color(title.color.r, title.color.g, title.color.b, title.color.a - (Time.deltaTime * timeSpeed));
            subTitle.color = new Color(subTitle.color.r, subTitle.color.g, subTitle.color.b, subTitle.color.a - (Time.deltaTime * timeSpeed));
            yield return null;
        }
        mainMenu.SetActive(true);
    }
}