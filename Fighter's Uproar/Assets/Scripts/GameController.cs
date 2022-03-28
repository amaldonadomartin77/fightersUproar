using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour {

    // Take in game objects
	public GameObject playerOne, playerTwo, victoryMenu;
    public int maxWins, maxTime;
    public bool movementAllowed;

    private GameObject p1UI, p2UI, uiText, fadeImage;
    private Timer timer;
    private Vector3 initialP1Pos, initialP2Pos;
    private int p1WinCount, p2WinCount, currentRound;

    private void Awake()
    {
        initialP1Pos = playerOne.transform.position;
        initialP2Pos = playerTwo.transform.position;
        p1UI = GameObject.Find("/Canvas/P1UI");
        p2UI = GameObject.Find("/Canvas/P2UI");
        uiText = GameObject.Find("/Canvas/TextObjects");
        fadeImage = GameObject.Find("/Canvas/Fade");
        p1WinCount = 0;
        p2WinCount = 0;
        currentRound = 1;
        timer = GameObject.Find("TimeRemaining").GetComponent<Timer>();
        movementAllowed = false;
        fadeImage.GetComponent<Image>().color = new Color(0, 0, 0, 1);
    }

    private void Start()
    {
        BeginRound();
    }

    // Update is called once per frame
    void Update() {
        if (CheckForKO(playerTwo))
        {
            StartCoroutine(HandleVictory(false, 1));
            playerTwo.GetComponent<HealthSystem>().SetHealth(0.001f);
        }

        if (CheckForKO(playerOne))
        {
            StartCoroutine(HandleVictory(false, 2));
            playerOne.GetComponent<HealthSystem>().SetHealth(0.001f);
        }

        if (timer.GetTime() == 0)
        {
            if (playerOne.GetComponent<HealthSystem>().GetHealthNormalized() > playerTwo.GetComponent<HealthSystem>().GetHealthNormalized())
                StartCoroutine(HandleVictory(true, 1));
            else if (playerOne.GetComponent<HealthSystem>().GetHealthNormalized() < playerTwo.GetComponent<HealthSystem>().GetHealthNormalized())
                StartCoroutine(HandleVictory(true, 2));
            else
                StartCoroutine(HandleVictory(true, 3));
            timer.ResetTimer();
            timer.ForceDisplay(true);
        }

        if (Input.GetKey("escape"))
        {
            SceneManager.LoadScene("LevelSelectScene");
        }
    }

    private bool CheckForKO(GameObject player)
    {
        if (player.transform.GetComponent<HealthSystem>().GetHealthNormalized() == 0.0f)
            return true;
        else
            return false;
    }

    private void PlayerOneWins()
    {
        p1WinCount++;
        if (playerOne.GetComponent<HealthSystem>().GetHealthNormalized() == 1.0f)
            p1UI.transform.Find("RoundsWon").gameObject.GetComponent<PlayerRounds>().Victory("Perfect");
        else
            p1UI.transform.Find("RoundsWon").gameObject.GetComponent<PlayerRounds>().Victory("Normal");

        if (p1WinCount == maxWins)
            MatchIsOver(1);
        else
            ResetRound();
    }

    private void PlayerTwoWins()
    {
        p2WinCount++;
        if (playerTwo.GetComponent<HealthSystem>().GetHealthNormalized() == 1.0f)
            p2UI.transform.Find("RoundsWon").gameObject.GetComponent<PlayerRounds>().Victory("Perfect");
        else
            p2UI.transform.Find("RoundsWon").gameObject.GetComponent<PlayerRounds>().Victory("Normal");
        if (p2WinCount == maxWins)
            MatchIsOver(2);
        else
            ResetRound();
    }

    private bool IsFinalRound()
    {
        if (maxWins == 1)
            return true;
        if (p1WinCount == 1 && p2WinCount == 1 && maxWins == 2)
            return true;
        if (p1WinCount == 2 && p2WinCount == 2 && maxWins == 3)
            return true;
        else
            return false;
    }
    private void BeginRound()
    {
        StartCoroutine(BeginRoundAnimation());
    }

    private void ResetRound()
    {
        //Reset player position
        playerOne.transform.position = initialP1Pos;
        playerTwo.transform.position = initialP2Pos;

        //Reset player health and meter values
        playerOne.transform.GetComponent<HealthSystem>().ResetHealth();
        playerOne.transform.GetComponent<MeterSystem>().ResetMeter();
        playerTwo.transform.GetComponent<HealthSystem>().ResetHealth();
        playerTwo.transform.GetComponent<MeterSystem>().ResetMeter();

        //Reset the health and meter gauges
        p1UI.transform.Find("HealthGauge").gameObject.GetComponent<HealthGauge>().ResetGauge();
        p1UI.transform.Find("SpecialGauge").gameObject.GetComponent<MeterGauge>().ResetGauge();
        p2UI.transform.Find("HealthGauge").gameObject.GetComponent<HealthGauge>().ResetGauge();
        p2UI.transform.Find("SpecialGauge").gameObject.GetComponent<MeterGauge>().ResetGauge();

        timer.ForceDisplay(false);
        timer.ResetTimer();
        currentRound++;

        BeginRound();
    }

    private IEnumerator ScreenFadeIn()
    {
        Image fade = fadeImage.GetComponent<Image>();
        while (fade.color.a > 0)
        {
            fade.color = new Color(0, 0, 0, fade.color.a - (Time.deltaTime * 2));
            yield return null;
        }
    }
    private IEnumerator ScreenFadeOut()
    {
        Image fade = fadeImage.GetComponent<Image>();
        while (fade.color.a < 1)
        {
            fade.color = new Color(0, 0, 0, fade.color.a + (Time.deltaTime * 2));
            yield return null;
        }
    }

    private IEnumerator BeginRoundAnimation()
    {
        yield return StartCoroutine(ScreenFadeIn());

        TextMeshProUGUI roundText = uiText.transform.Find("RoundIndicator").gameObject.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI fightText = uiText.transform.Find("Fight").gameObject.GetComponent<TextMeshProUGUI>();

        if (IsFinalRound())
            roundText.text = "FINAL ROUND";
        else
            roundText.text = "ROUND " + currentRound;

        roundText.color = new Color(roundText.color.r, roundText.color.g, roundText.color.b, 0);
        fightText.color = new Color(fightText.color.r, fightText.color.g, fightText.color.b, 0);

        yield return new WaitForSeconds(0.5f);
        while (roundText.color.a < 1)
        {
            roundText.color = new Color(roundText.color.r, roundText.color.g, roundText.color.b, roundText.color.a + (Time.deltaTime * 3));
            yield return null;
        }

        yield return new WaitForSeconds(1);
        while (roundText.color.a > 0.0f)
        {
            roundText.color = new Color(roundText.color.r, roundText.color.g, roundText.color.b, roundText.color.a - (Time.deltaTime * 3));
            yield return null;
        }

        yield return new WaitForSeconds(1);
        movementAllowed = true;

        while (fightText.color.a < 1)
        {
            fightText.color = new Color(1.0f, 1.0f, 1.0f, fightText.color.a + (Time.deltaTime * 20));
            yield return null;
        }
        while (fightText.color.r > 0.64f)
        {
            fightText.color = new Color(fightText.color.r - (Time.deltaTime * 1f), fightText.color.g - (Time.deltaTime * 1f), fightText.color.b - (Time.deltaTime * 1f), fightText.color.a);
            yield return null;
        }
        while (fightText.color.a > 0.0f)
        {
            fightText.color = new Color(fightText.color.r, fightText.color.g, fightText.color.b, fightText.color.a - (Time.deltaTime * 2));
            yield return null;
        }
    }

    private IEnumerator BeginTimeUpAnimation()
    {
        TextMeshProUGUI timeUpText = uiText.transform.Find("TimeOver").gameObject.GetComponent<TextMeshProUGUI>();

        timeUpText.color = new Color(timeUpText.color.r, timeUpText.color.g, timeUpText.color.b, 0);

        while (timeUpText.color.a < 1)
        {
            timeUpText.color = new Color(timeUpText.color.r, timeUpText.color.g, timeUpText.color.b, timeUpText.color.a + (Time.deltaTime * 10));
            yield return null;
        }
        yield return new WaitForSeconds(2f);
        while (timeUpText.color.a > 0.0f)
        {
            timeUpText.color = new Color(timeUpText.color.r, timeUpText.color.g, timeUpText.color.b, timeUpText.color.a - (Time.deltaTime * 2));
            yield return null;
        }
    }

    private IEnumerator BeginKOAnimation()
    {
        TextMeshProUGUI koText = uiText.transform.Find("KO").gameObject.GetComponent<TextMeshProUGUI>();

        koText.color = new Color(koText.color.r, koText.color.g, koText.color.b, 0);

        while (koText.color.a < 1)
        {
            koText.color = new Color(1.0f, 1.0f, 1.0f, koText.color.a + (Time.deltaTime * 20));
            yield return null;
        }
        while (koText.color.r > 0.70f)
        {
            koText.color = new Color(koText.color.r - (Time.deltaTime * 0.5f), koText.color.g - (Time.deltaTime * 0.5f), koText.color.b - (Time.deltaTime * 0.5f), koText.color.a);
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        while (koText.color.a > 0.0f)
        {
            koText.color = new Color(koText.color.r, koText.color.g, koText.color.b, koText.color.a - (Time.deltaTime * 2));
            yield return null;
        }
    }

    private IEnumerator BeginWinAnimation(int player, bool perfect)
    {
        TextMeshProUGUI winnerText = uiText.transform.Find("Winner").gameObject.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI perfectText = uiText.transform.Find("Perfect").gameObject.GetComponent<TextMeshProUGUI>();

        if (player == 1)
            winnerText.text = "P1 WINS";
        else if (player == 2)
            winnerText.text = "P2 WINS";
        else
            winnerText.text = "DRAW";

        winnerText.color = new Color(winnerText.color.r, winnerText.color.g, winnerText.color.b, 0);

        while (winnerText.color.a < 1)
        {
            winnerText.color = new Color(winnerText.color.r, winnerText.color.g, winnerText.color.b, winnerText.color.a + (Time.deltaTime * 5));
            yield return null;
        }

        yield return new WaitForSeconds(1.5f);

        if (perfect)
        {
            perfectText.color = new Color(perfectText.color.r, perfectText.color.g, perfectText.color.b, 0);
            while (perfectText.color.a < 1)
            {
                perfectText.color = new Color(winnerText.color.r, winnerText.color.g, winnerText.color.b, winnerText.color.a + (Time.deltaTime * 5));
                yield return null;
            }

            yield return new WaitForSeconds(1.5f);
        }

        while (winnerText.color.a > 0.0f)
        {
            winnerText.color = new Color(winnerText.color.r, winnerText.color.g, winnerText.color.b, winnerText.color.a - (Time.deltaTime * 5));
            perfectText.color = new Color(perfectText.color.r, perfectText.color.g, perfectText.color.b, perfectText.color.a - (Time.deltaTime * 5.2f));
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
    }

    private IEnumerator HandleVictory(bool timeOver, int player)
    {
        movementAllowed = false;

        if (timeOver)
            yield return StartCoroutine(BeginTimeUpAnimation());
        else
            yield return StartCoroutine(BeginKOAnimation());

        yield return new WaitForSeconds(0.5f);

        if (player == 1)
        {
            if (playerOne.GetComponent<HealthSystem>().GetHealthNormalized() == 1.0f)
                yield return StartCoroutine(BeginWinAnimation(1, true));
            else
                yield return StartCoroutine(BeginWinAnimation(1, false));

            if(p1WinCount != maxWins - 1)
                yield return StartCoroutine(ScreenFadeOut());

            PlayerOneWins();
        }

        else if (player == 2)
        {
            if (playerTwo.GetComponent<HealthSystem>().GetHealthNormalized() == 1.0f)
                yield return StartCoroutine(BeginWinAnimation(2, true));
            else
                yield return StartCoroutine(BeginWinAnimation(2, false));
            
            if (p2WinCount != maxWins - 1)
                yield return StartCoroutine(ScreenFadeOut());

            PlayerTwoWins();
        }
        else
        {
            yield return StartCoroutine(BeginWinAnimation(0, false));
            yield return StartCoroutine(ScreenFadeOut());
            ResetRound();
        }
    }

    public void MatchIsOver(int player)
    {
        victoryMenu.SetActive(true);
        if (player == 1)
            victoryMenu.transform.Find("VictoryText").gameObject.GetComponent<TextMeshProUGUI>().text = "P1 VICTORY";
        else
            victoryMenu.transform.Find("VictoryText").gameObject.GetComponent<TextMeshProUGUI>().text = "P2 VICTORY";

    }

    public void ResetMatch()
    {
        victoryMenu.SetActive(false);
        StartCoroutine(ResetEntireMatch());
    }

    public void CharacterSelect()
    {
        victoryMenu.SetActive(false);
        StartCoroutine(CharSel());
    }

    public void TitleScreen()
    {
        victoryMenu.SetActive(false);
        StartCoroutine(Title());
    }

    private IEnumerator ResetEntireMatch()
    {
        yield return new WaitForSeconds(1f);
        p1WinCount = 0;
        p2WinCount = 0;
        currentRound = 0;
        yield return StartCoroutine(ScreenFadeOut());
        p1UI.transform.Find("RoundsWon").gameObject.GetComponent<PlayerRounds>().ResetDisplay();
        p2UI.transform.Find("RoundsWon").gameObject.GetComponent<PlayerRounds>().ResetDisplay();
        ResetRound();
    }

    private IEnumerator CharSel()
    {
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(ScreenFadeOut());
        SceneManager.LoadScene("LevelSelectScene");
    }

    private IEnumerator Title()
    {
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(ScreenFadeOut());
        SceneManager.LoadScene("MainMenu");
    }
}
