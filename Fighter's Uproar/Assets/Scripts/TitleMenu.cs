using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System;

public class TitleMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public TextMeshProUGUI titleName, subTitle, pressStart;
    public AudioSource audioSource;
    public AudioClip soundClip;
    public PlayerInput startInput;
    public bool readyToStart = false;
    private GameObject self;

    public static bool initiated = false;

    private void Awake()
    {
        self = this.gameObject;
        if (initiated)
        {
            mainMenu.SetActive(true);
            self.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeInText(1f, pressStart));
    }

    public void PressStart(InputAction.CallbackContext ctx)
    {
        if (readyToStart && ctx.performed)
        {
            audioSource.PlayOneShot(soundClip);
            StartCoroutine(FadeOutText(1f, pressStart));
            StartCoroutine(FadeOutLogo(0.8f, titleName, subTitle));
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
        readyToStart = true;
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
        initiated = true;
        mainMenu.SetActive(true);
    }
}