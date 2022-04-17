using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject titleMenu;
    private static bool disableFade;
    GameObject menuHeader, background, fightButton, charButton, optionButton, quitButton, aceRender, bellaRender, isaacRender, katsuRender;

    // Start is called before the first frame update
    void Start()
    {
        titleMenu.SetActive(false);
        menuHeader = GameObject.Find("MenuHeader");
        background = GameObject.Find("MenuBackground");
        fightButton = GameObject.Find("FightButton");
        charButton = GameObject.Find("CharacterButton");
        optionButton = GameObject.Find("OptionsButton");
        quitButton = GameObject.Find("QuitButton");
        aceRender = GameObject.Find("AceRender");
        bellaRender = GameObject.Find("BellaRender");
        isaacRender = GameObject.Find("IsaacRender");
        katsuRender = GameObject.Find("KatsuRender");

        float time;

        if (disableFade)
            time = 5000;
        else
            time = 1.5f;

        StartCoroutine(FadeInText(time, menuHeader.GetComponent<TextMeshProUGUI>()));
        StartCoroutine(FadeInImage(time, background.GetComponent<Image>()));
        StartCoroutine(FadeInImage(time, aceRender.GetComponent<Image>()));
        StartCoroutine(FadeInImage(time, bellaRender.GetComponent<Image>()));
        StartCoroutine(FadeInImage(time, isaacRender.GetComponent<Image>()));
        StartCoroutine(FadeInImage(time, katsuRender.GetComponent<Image>()));
        disableFade = true;
    }

    private IEnumerator FadeInText(float timeSpeed, TextMeshProUGUI text)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
        while (text.color.a < 1f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + (Time.deltaTime * timeSpeed));
            yield return null;
        }
    }

    private IEnumerator FadeInImage(float timeSpeed, Image image)
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
        while (image.color.a < 1f)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a + (Time.deltaTime * timeSpeed));
            yield return null;
        }
    }

    private IEnumerator FadeInButton(float timeSpeed, Image image)
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
        while (image.color.a < 1f)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a + (Time.deltaTime * timeSpeed));
            yield return null;
        }
    }
}
