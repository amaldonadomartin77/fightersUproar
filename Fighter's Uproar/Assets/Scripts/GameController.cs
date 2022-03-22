using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    // Take in game objects
	public GameObject playerOne, playerTwo;
    public int maxWins;

    private GameObject p1UI, p2UI;
    private Vector3 initialP1Pos, initialP2Pos;
    private int p1WinCount, p2WinCount, currentRound;

    private void Awake()
    {
        initialP1Pos = playerOne.transform.position;
        initialP2Pos = playerTwo.transform.position;
        p1UI = GameObject.Find("/Canvas/P1UI");
        p2UI = GameObject.Find("/Canvas/P2UI");
        p1WinCount = 0;
        p2WinCount = 0;
    }

    // Update is called once per frame
    void Update() {
        if (CheckForKO(playerTwo))
        {
            Debug.Log("Player 1 wins!");
            PlayerOneWins();
        }

        if (CheckForKO(playerOne))
        {
            Debug.Log("Player 2 wins!");
            PlayerTwoWins();
        }

        if (Input.GetKey("escape"))
        {
            ResetRound();
            Debug.Log("reset");
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
        ResetRound();
    }

    private void PlayerTwoWins()
    {
        p2WinCount++;
        if (playerTwo.GetComponent<HealthSystem>().GetHealthNormalized() == 1.0f)
            p2UI.transform.Find("RoundsWon").gameObject.GetComponent<PlayerRounds>().Victory("Perfect");
        else
            p2UI.transform.Find("RoundsWon").gameObject.GetComponent<PlayerRounds>().Victory("Normal");
        ResetRound();
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
    }
}
