using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class StageSelectManager : MonoBehaviour
{

    public TextMeshProUGUI stageInfo_0_Text;
    public TextMeshProUGUI stageInfo_1_Text;
    public TextMeshProUGUI stageInfo_2_Text;

    public Button stage1Btn;
    public Button stage2Btn;
    public Button selectBtn;

    public AudioSource audioSource;
    public AudioClip cursorSound;
    public AudioClip backSound;
    public AudioClip confirmSound;

    public static int stageID;


    string[] stage1_info = { "STAGE 1", 
                            "Forgotten city",
                            "No one remembers how long it has been since that war. No one remembers how prosperous the city used to be. The city buried by wind and sand can no longer see its former glory. Only a few castles can attest to the fact that this place was once a paradise." };

    string[] stage2_info = { "STAGE 2", 
                            "Forgotten castle",
                            "Since the interior of the castle is mainly constructed of stone bricks and tiles, the walls and floors have not been damaged too much. It's hard to believe that this castle was also a pre-war product if it weren't for a lot of dust. But the treasure in the castle has disappeared." };


    void Start()
    {
        stageID = 1;
        stage1Btn.Select();
        ClickStage1();
    }

    public void ClickStage1()
    {
        stage1Btn.gameObject.SetActive(true);
        stage2Btn.gameObject.SetActive(true);

        stageInfo_0_Text.text = stage1_info[0];
        stageInfo_1_Text.text = stage1_info[1];
        stageInfo_2_Text.text = stage1_info[2];

        stageID = 1;
        audioSource.PlayOneShot(cursorSound);
        stage1Btn.gameObject.SetActive(false);
    }


    public void ClickStage2()
    {
        stage1Btn.gameObject.SetActive(true);
        stage2Btn.gameObject.SetActive(true);

        stageInfo_0_Text.text = stage2_info[0];
        stageInfo_1_Text.text = stage2_info[1];
        stageInfo_2_Text.text = stage2_info[2];

        stageID = 2;
        audioSource.PlayOneShot(cursorSound);
        stage2Btn.gameObject.SetActive(false);
    }

    public void ClickSelectBtn()
    {
        StartCoroutine(LoadGameplay(1f));
    }

    public void ClickBackBtn()
    {
        StartCoroutine(LoadCharSelect(1f));
    }

    private IEnumerator LoadCharSelect(float time)
    {
        audioSource.PlayOneShot(backSound);
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene("MainMenu");
    }

    private IEnumerator LoadGameplay(float time)
    {
        audioSource.PlayOneShot(confirmSound);
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene("VersusMode2");
    }
}
