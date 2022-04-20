using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class SelectorManager : MonoBehaviour
{
    //serializedField the gameobject and to create a 2*2 array.
    //followed by the online tutorial

    public TextMeshProUGUI playerTitleName;

    public TextMeshProUGUI playerInfo_0_Text;
    public TextMeshProUGUI playerInfo_1_Text;
    public TextMeshProUGUI playerInfo_2_Text;

    public TextMeshProUGUI playerSkill_1_0_Text;
    public TextMeshProUGUI playerSkill_1_1_Text;
    public TextMeshProUGUI playerSkill_2_0_Text;
    public TextMeshProUGUI playerSkill_2_1_Text;
    public TextMeshProUGUI playerSkill_3_0_Text;
    public TextMeshProUGUI playerSkill_3_1_Text;

    public Button char1Btn;
    public Button char2Btn;
    public Button char3Btn;
    public Button char4Btn;
    public Button selectBtn;

    public int userid;
    bool isOne;


    // public GameObject BK1;
    // public GameObject BK2;
    // public GameObject BK3;
    // public GameObject BK4;

    public AudioSource audioSource;
    public AudioClip cursorSound;
    public AudioClip backSound;
    public AudioClip confirmSound;

    string[] characterNameList = { "ACE", "BELLA", "ISAAC", "KATSU" };

    // Dictionary<string,string> char1_info = new Dictionary<string, string>();
    // Dictionary<string,string> char2_info = new Dictionary<string, string>();

    string[] char1_info = { "CHARACTER 1", "ACE",
                            "A lone wolf who loves no one but his bike. A mercenary who wields a rifle and a knife for close attacks. Keeping his enemies at a distance is his forte." };
    string[] char1_skill1 = { "Slice", "A quick knife slice that deals heavy damage" };
    string[] char1_skill2 = { "Laser Shot", "A powerful shot that cuts the across the arena" };
    string[] char1_skill3 = { "Poison Nade", "An AOE ground attack that spews a puddle of poison that can inflict damage to the opponent" };

    string[] char2_info = { "CHARACTER 2", "BELLA", 
                            "A fierce young woman determined to prove her strength.  Prefers to crush her opponents with her fists as opposed to shooting.  Enjoys keeping things up close and personal." };
    string[] char2_skill1 = { "Punch", " A signature punch move that deals heavy damage" };
    string[] char2_skill2 = { "Rocket Punch", "A special punch move that launches a projectile at the opponent" };
    string[] char2_skill3 = { "Dragon Punch", "Bella’s signature rising attack move that delivers a striking uppercut against the opponent" };

    string[,] characterSkillList = { { "Skill 1 descrption will be here: ----", "Skill 2 descrption will be here:------------------", "Skill 2 descrption will be here:-------------------" }, { "Skill 1 descrption will be here:-----------------", "Skill 2 descrption will be here", "Skill 3 descrption will be here" },
                                        { "Skill 1 descrption will be here", "Skill 2 descrption will be here", "CCA" }, { "AACC", "BBAA", "CCAA" } };

    // public GameObject selector;
    public GameObject[] row1;
    // public GameObject[]row2;

    //make a 2 by 2 array
    int cols;
    int rows = 1;

    Vector2 positionIndex;
    GameObject currentSlot;
    // bool isMoving = false;

    //declear to make a 2 dimenional array
    GameObject[,] grid;

    // Start is called before the first frame update
    void Start()
    {
        userid = 0;
        isOne = true;

        playerTitleName.text = "PLAYER 1";
        playerTitleName.color = new Color(0.482f, 0.573f, 0.937f, 1.0f);
        playerInfo_0_Text.color = new Color(0.482f, 0.573f, 0.937f, 1.0f);
        playerInfo_1_Text.color = new Color(0.482f, 0.573f, 0.937f, 1.0f);

        cols = row1.Length;         //4
        // print("cols: " + cols);
        grid = new GameObject[rows, cols];          // 4,1
        AddRowToGrid(0, row1);
        // AddRowToGrid(1,row2);

        //starting point (0,0)
        positionIndex = new Vector2(0, 0);
        currentSlot = grid[0, 0];
        char1Btn.Select();
        ClickChar1();
    }


    void AddRowToGrid(int index, GameObject[] row)
    {

        for (int i = 0; i < row.Length; i++)
        {

            grid[index, i] = row[i];
            // print("info: "+index+"--"+i);

        }
    }


    public void ClickChar1()
    {
        userid = 0;
        char1Btn.gameObject.SetActive(true);
        char2Btn.gameObject.SetActive(true);
        char3Btn.gameObject.SetActive(true);
        char4Btn.gameObject.SetActive(true);
        playerInfo_0_Text.text = char1_info[0];
        playerInfo_1_Text.text = char1_info[1];
        playerInfo_2_Text.text = char1_info[2];
        playerSkill_1_0_Text.text = char1_skill1[0];
        playerSkill_1_1_Text.text = char1_skill1[1];
        playerSkill_2_0_Text.text = char1_skill2[0];
        playerSkill_2_1_Text.text = char1_skill2[1];
        playerSkill_3_0_Text.text = char1_skill3[0];
        playerSkill_3_1_Text.text = char1_skill3[1];
        audioSource.PlayOneShot(cursorSound);
       //selectBtn.gameObject.SetActive(true);
        char1Btn.gameObject.SetActive(false);
        selectBtn.interactable = true;
    }


    public void ClickChar2()
    {
        userid = 1;
        char1Btn.gameObject.SetActive(true);
        char2Btn.gameObject.SetActive(true);
        char3Btn.gameObject.SetActive(true);
        char4Btn.gameObject.SetActive(true);
        playerInfo_0_Text.text = char2_info[0];
        playerInfo_1_Text.text = char2_info[1];
        playerInfo_2_Text.text = char2_info[2];
        playerSkill_1_0_Text.text = char2_skill1[0];
        playerSkill_1_1_Text.text = char2_skill1[1];
        playerSkill_2_0_Text.text = char2_skill2[0];
        playerSkill_2_1_Text.text = char2_skill2[1];
        playerSkill_3_0_Text.text = char2_skill3[0];
        playerSkill_3_1_Text.text = char2_skill3[1];
        audioSource.PlayOneShot(cursorSound);
        //selectBtn.gameObject.SetActive(true);
        char2Btn.gameObject.SetActive(false);
        selectBtn.interactable = true;
    }

    public void ClickChar3()
    {
        userid = 2;
        char1Btn.gameObject.SetActive(true);
        char2Btn.gameObject.SetActive(true);
        char3Btn.gameObject.SetActive(true);
        char4Btn.gameObject.SetActive(true);
        playerInfo_0_Text.text = "CHARACTER 3";
        playerInfo_1_Text.text = "ISAAC";
        playerInfo_2_Text.text = "COMING SOON";
        playerSkill_1_0_Text.text = "N/A";
        playerSkill_1_1_Text.text = "Character is coming soon.";
        playerSkill_2_0_Text.text = "N/A";
        playerSkill_2_1_Text.text = "Character is coming soon.";
        playerSkill_3_0_Text.text = "N/A";
        playerSkill_3_1_Text.text = "Character is coming soon.";
        audioSource.PlayOneShot(cursorSound);
        selectBtn.interactable = false;
        char3Btn.gameObject.SetActive(false);
    }

    public void ClickChar4()
    {
        userid = 3;
        char1Btn.gameObject.SetActive(true);
        char2Btn.gameObject.SetActive(true);
        char3Btn.gameObject.SetActive(true);
        char4Btn.gameObject.SetActive(true);
        playerInfo_0_Text.text = "CHARACTER 4";
        playerInfo_1_Text.text = "KATSU";
        playerInfo_2_Text.text = "COMING SOON";
        playerSkill_1_0_Text.text = "N/A";
        playerSkill_1_1_Text.text = "Character is coming soon.";
        playerSkill_2_0_Text.text = "N/A";
        playerSkill_2_1_Text.text = "Character is coming soon.";
        playerSkill_3_0_Text.text = "N/A";
        playerSkill_3_1_Text.text = "Character is coming soon.";
        audioSource.PlayOneShot(cursorSound);
        selectBtn.interactable = false;
        char4Btn.gameObject.SetActive(false);
    }



    // Update is called once per frame
    void Update()
    {
        // print("userid: "+userid);

        // playerNameText.text = characterNameList[(int)positionIndex.x];
        // playerSkill1Text.text = characterSkillList[(int)positionIndex.x, 0];
        // playerSkill2Text.text = characterSkillList[(int)positionIndex.x, 1];
        // playerSkill3Text.text = characterSkillList[(int)positionIndex.x, 2];
        // print("p: "+(int)positionIndex.y+ (int)positionIndex.x);

        //move the selector
        //checking if the keyboard is pressed or not
        // float x = Input.GetAxisRaw("Horizontal"); //returns -0,1
        // // float y = Input.GetAxisRaw( "Vertical" );
        // if (x != 0 && !isMoving)
        // {
        //     audioSource.PlayOneShot(cursorSound);
        //     MoveSelector(x, 0);
        // }


        // //if press the enter key
        // if (Input.GetKeyDown(KeyCode.Return))
        // {

        //     // string levelID = currentSlot.GetComponet<LevelSelectItemScript>().levelID;
        //     if(userid == 2 || userid == 3){
        //         return;
        //     }
        //     if(isOne == true){
        //         user1 = userid;
        //         isOne = false;
        //         playerTitleName.text = "Player 2";
        //     }
        //     else{
        //         user2 = userid;
        //         StartCoroutine(LoadGameplay(1f));
        //     }

        // }


    }
    //void OnTriggerStay(Collider other){

    //}


    // void MoveSelector(float x, float y)
    // {
    //     if (isMoving == false)
    //     {

    //         isMoving = true;

    //         if (x > 0)
    //         {
    //             if (positionIndex.x < cols - 1)
    //             {
    //                 positionIndex.x += 1;
    //                 userid +=1;
    //                 // print("pp: "+positionIndex.x);
    //             }

    //         }
    //         else if (x < 0)
    //         {

    //             if (positionIndex.x > 0)
    //             {

    //                 positionIndex.x -= 1;
    //                 userid -=1;
    //                 //  print("pp: "+positionIndex.x);
    //             }
    //         }
    //         //  else if(y > 0){

    //         //      if (positionIndex.y > 0){

    //         //          positionIndex.y -= 1;

    //         //      }
    //         //  }
    //         //  else if( y < 0){

    //         //      if (positionIndex.y < rows - 1){

    //         //          positionIndex.y += 1;
    //         //      } 
    //         //  }
    //     }
    //     currentSlot = grid[(int)positionIndex.y, (int)positionIndex.x];
    //     // print("p: " + (int)positionIndex.y + (int)positionIndex.x);
    //     if ((int)positionIndex.x == 0)
    //     {
    //         BK1.SetActive(false);
    //         BK2.SetActive(true);
    //         BK3.SetActive(true);
    //         BK4.SetActive(true);
    //     }
    //     else if ((int)positionIndex.x == 1)
    //     {
    //         BK1.SetActive(true);
    //         BK2.SetActive(false);
    //         BK3.SetActive(true);
    //         BK4.SetActive(true);
    //     }
    //     else if ((int)positionIndex.x == 2)
    //     {
    //         BK1.SetActive(true);
    //         BK2.SetActive(true);
    //         BK3.SetActive(false);
    //         BK4.SetActive(true);
    //     }
    //     else if ((int)positionIndex.x == 3)
    //     {
    //         BK1.SetActive(true);
    //         BK2.SetActive(true);
    //         BK3.SetActive(true);
    //         BK4.SetActive(false);
    //     }
    //     //  selector.transform.position = currentSlot.transform.position;
    //     Invoke("ResetMoving", 0.2f);
    // }

    // void ResetMoving()
    // {
    //     isMoving = false;
    //     // print("here");
    // }


    public void ClickSelectBtn()
    {
        if (userid == 2 || userid == 3)
        {
            audioSource.PlayOneShot(backSound);
            ClickChar1();
            return;
        }
        if (isOne == true)
        {
            Settings.s.playerOneCharacter = userid;
            isOne = false;
            playerTitleName.text = "PLAYER 2";
            playerTitleName.color = new Color(0.886f, 0.353f, 0.247f, 1.0f);
            playerInfo_0_Text.color = new Color(0.886f, 0.353f, 0.247f, 1.0f);
            playerInfo_1_Text.color = new Color(0.886f, 0.353f, 0.247f, 1.0f);
            char1Btn.Select();
            ClickChar1();
        }
        else
        {
            Settings.s.playerTwoCharacter = userid;
            StartCoroutine(LoadGameplay(1f));
        }
    }

    public void ClickBackBtn()
    {
        if (isOne == true)
            StartCoroutine(LoadCharSelect(1f));
        else
        {
            audioSource.PlayOneShot(backSound);
            playerTitleName.text = "PLAYER 1";
            playerTitleName.color = new Color(0.482f, 0.573f, 0.937f, 1.0f);
            playerInfo_0_Text.color = new Color(0.482f, 0.573f, 0.937f, 1.0f);
            playerInfo_1_Text.color = new Color(0.482f, 0.573f, 0.937f, 1.0f);
            isOne = true;
            ClickChar1();
        }
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
        SceneManager.LoadScene("StageSelect");
    }
}

