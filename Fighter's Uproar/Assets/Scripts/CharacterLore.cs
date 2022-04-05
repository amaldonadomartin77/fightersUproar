using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CharacterLore : MonoBehaviour
{
    private int charIndex;
    private GameObject bellaDesc, aceDesc, isaacDesc, katsuDesc;
    private TextMeshProUGUI charName;
    private Image render;

    public Sprite bellaRender, aceRender, isaacRender, katsuRender;

    private void Awake()
    {
        charIndex = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        bellaDesc = GameObject.Find("/Canvas/Descriptions/BellaDesc").gameObject;
        aceDesc = GameObject.Find("/Canvas/Descriptions/AceDesc").gameObject;
        isaacDesc = GameObject.Find("/Canvas/Descriptions/IsaacDesc").gameObject;
        katsuDesc = GameObject.Find("/Canvas/Descriptions/KatsuDesc").gameObject;
        charName = GameObject.Find("/Canvas/CharacterName").gameObject.GetComponent<TextMeshProUGUI>();
        render = GameObject.Find("/Canvas/CharacterRender").gameObject.GetComponent<Image>();

        DisplayInformation();
    }

    public void NextCharacter()
    {
        if (charIndex == 3)
            charIndex = 0;
        else
            charIndex++;
        DisplayInformation();
    }

    public void PrevCharacter()
    {
        if (charIndex == 0)
            charIndex = 3;
        else
            charIndex--;
        DisplayInformation();
    }

    private void DisplayInformation()
    {
        switch(charIndex)
        {
            case 0:
                aceDesc.SetActive(false);
                katsuDesc.SetActive(false);
                bellaDesc.SetActive(true);
                charName.text = "Bellator (Bella)";
                render.sprite = bellaRender;
                break;
            case 1:
                isaacDesc.SetActive(false);
                bellaDesc.SetActive(false);
                aceDesc.SetActive(true);
                charName.text = "Ace";
                render.sprite = aceRender;
                break;
            case 2:
                katsuDesc.SetActive(false);
                aceDesc.SetActive(false);
                isaacDesc.SetActive(true);
                charName.text = "Isaac";
                render.sprite = isaacRender;
                break;
            case 3:
                bellaDesc.SetActive(false);
                isaacDesc.SetActive(false);
                katsuDesc.SetActive(true);
                charName.text = "Katsumaki (Katsu)";
                render.sprite = katsuRender;
                break;
        }
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
