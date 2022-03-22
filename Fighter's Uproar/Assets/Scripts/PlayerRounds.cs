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
        if (gameController.maxWins > 1)
        {
            roundOne.transform.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            roundTwo.transform.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            if (gameController.maxWins == 3)
                roundThree.transform.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }
        else
            roundOne.transform.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
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
