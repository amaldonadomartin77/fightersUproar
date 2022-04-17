using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRounds : MonoBehaviour
{

    public Sprite imageNone, imageVictory, imagePerfect, imageSpecial, imageChip;
    public int roundsWon;


    private GameObject roundOne, roundTwo, roundThree;
    private GameController gameController;

    // Start is called before the first frame update
    void Awake()
    {
        roundOne = transform.Find("1stRound").gameObject;
        roundTwo = transform.Find("2ndRound").gameObject;
        roundThree = transform.Find("3rdRound").gameObject;
        roundsWon = 0;
    }
    private void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        if (Settings.s.numberOfRounds >= 3)
            roundTwo.transform.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        if (Settings.s.numberOfRounds == 5)
            roundThree.transform.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        roundOne.transform.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }

    public void Victory(string victoryType)
    {
        roundsWon++;
        if (roundsWon == 1)
            roundOne.transform.GetComponent<Image>().sprite = GetImage(victoryType);
        if (roundsWon == 2)
            roundTwo.transform.GetComponent<Image>().sprite = GetImage(victoryType);
        if (roundsWon == 3)
            roundThree.transform.GetComponent<Image>().sprite = GetImage(victoryType);
    }

    public void ResetDisplay()
    {
        roundsWon = 0;
        roundOne.transform.GetComponent<Image>().sprite = imageNone;
        roundTwo.transform.GetComponent<Image>().sprite = imageNone;
        roundThree.transform.GetComponent<Image>().sprite = imageNone;
    }

    private Sprite GetImage(string victoryType)
    {
        if (victoryType == "Perfect")
            return imagePerfect;
        if (victoryType == "Special")
            return imageSpecial;
        if (victoryType == "Chip")
            return imageChip;
        else
            return imageVictory;
    }
}
