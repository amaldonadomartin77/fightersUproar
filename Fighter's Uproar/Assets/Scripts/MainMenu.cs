using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject titleMenu;
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
        StartCoroutine(FadeInText(1.5f, menuHeader.GetComponent<TextMeshProUGUI>()));
        StartCoroutine(FadeInImage(1.5f, background.GetComponent<Image>()));
        StartCoroutine(FadeInImage(1.5f, aceRender.GetComponent<Image>()));
        StartCoroutine(FadeInImage(1.5f, bellaRender.GetComponent<Image>()));
        StartCoroutine(FadeInImage(1.5f, isaacRender.GetComponent<Image>()));
        StartCoroutine(FadeInImage(1.5f, katsuRender.GetComponent<Image>()));
    }

    // Update is called once per frame
    void Update()
    {

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
