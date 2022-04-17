using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageSelectManager : MonoBehaviour
{

    public Text stageInfo_0_Text;
    public Text stageInfo_1_Text;
    public Text stageInfo_2_Text;

    public Button stage1Btn;
    public Button stage2Btn;
    public Button selectBtn;

    public AudioSource audioSource;
    public AudioClip cursorSound;
    public AudioClip backSound;
    public AudioClip confirmSound;

    public static int stageID;


    string[] stage1_info = { "STAGE 1", 
                            "ROCK",
                            "-Many many rock\n-Many many many rock\n-Many many many many rock" };

    string[] stage2_info = { "STAGE 2", 
                            "WATER",
                            "-Many many water\n-Many many many water\n-Many many many many water" };


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
