using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SelectorManager : MonoBehaviour
{
    //serializedField the gameobject and to create a 2*2 array.
    //followed by the online tutorial

    public Text playerTitleName;

    public Text playerInfo_0_Text;
    public Text playerInfo_1_Text;
    public Text playerInfo_2_Text;

    public Text playerSkill_1_0_Text;
    public Text playerSkill_1_1_Text;
    public Text playerSkill_2_0_Text;
    public Text playerSkill_2_1_Text;
    public Text playerSkill_3_0_Text;
    public Text playerSkill_3_1_Text;

    public Button char1Btn;
    public Button char2Btn;
    public Button char3Btn;
    public Button char4Btn;
    public Button selectBtn;


    public static int user1;
    public static int user2;
    int userid;
    bool isOne;


    // public GameObject BK1;
    // public GameObject BK2;
    // public GameObject BK3;
    // public GameObject BK4;

    public AudioSource audioSource;
    public AudioClip cursorSound;
    public AudioClip backSound;
    public AudioClip confirmSound;

    string[] characterNameList = { "Ace", "Bella", "Isaac", "Katsu" };

    // Dictionary<string,string> char1_info = new Dictionary<string, string>();
    // Dictionary<string,string> char2_info = new Dictionary<string, string>();

    string[] char1_info = { "CHARACTER 1", "Ace", 
                            "-Collecting data for the perfect fighter\n-Obsessed with finding breakthroughs\n-Modified body" };
    string[] char1_skill1 = { "Skill1", "diu diu diu  11" };
    string[] char1_skill2 = { "Skill2", "diu diu diu di di  12" };
    string[] char1_skill3 = { "Skill3", "diu diu diu pa pa  13" };

    string[] char2_info = { "CHARACTER 2", "Bella", 
                            "Loves no one but his bike, lone wolf.\n-Wields a single rifle, knife for punch.\n-Mercenary" };
    string[] char2_skill1 = { "Skill1", "zi zi diu diu diu  21" };
    string[] char2_skill2 = { "Skill2", "di di diu diu diu  22" };
    string[] char2_skill3 = { "Skill3", "pa pa diu diu diu  23" };

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
        user1 = 0;
        user2 = 0;
        userid = 0;
        isOne = true;

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
        userid = 0;
        audioSource.PlayOneShot(cursorSound);
        selectBtn.gameObject.SetActive(true);
        char1Btn.gameObject.SetActive(false);
    }


    public void ClickChar2()
    {
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
        userid = 1;
        audioSource.PlayOneShot(cursorSound);
        selectBtn.gameObject.SetActive(true);
        char2Btn.gameObject.SetActive(false);
    }

    public void ClickChar3()
    {
        char1Btn.gameObject.SetActive(true);
        char2Btn.gameObject.SetActive(true);
        char3Btn.gameObject.SetActive(true);
        char4Btn.gameObject.SetActive(true);
        playerInfo_0_Text.text = "";
        playerInfo_1_Text.text = "Comming Soon";
        playerInfo_2_Text.text = "";
        playerSkill_1_0_Text.text = "";
        playerSkill_1_1_Text.text = "";
        playerSkill_2_0_Text.text = "";
        playerSkill_2_1_Text.text = "";
        playerSkill_3_0_Text.text = "";
        playerSkill_3_1_Text.text = "";
        userid = 3;
        audioSource.PlayOneShot(cursorSound);
        selectBtn.gameObject.SetActive(false);
        char3Btn.gameObject.SetActive(false);
    }

    public void ClickChar4()
    {
        char1Btn.gameObject.SetActive(true);
        char2Btn.gameObject.SetActive(true);
        char3Btn.gameObject.SetActive(true);
        char4Btn.gameObject.SetActive(true);
        playerInfo_0_Text.text = "";
        playerInfo_1_Text.text = "Comming Soon";
        playerInfo_2_Text.text = "";
        playerSkill_1_0_Text.text = "";
        playerSkill_1_1_Text.text = "";
        playerSkill_2_0_Text.text = "";
        playerSkill_2_1_Text.text = "";
        playerSkill_3_0_Text.text = "";
        playerSkill_3_1_Text.text = "";
        userid = 3;
        audioSource.PlayOneShot(cursorSound);
        selectBtn.gameObject.SetActive(false);
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
            return;
        }
        if (isOne == true)
        {
            user1 = userid;
            isOne = false;
            playerTitleName.text = "Player 2";
            char1Btn.Select();
            ClickChar1();
        }
        else
        {
            user2 = userid;
            StartCoroutine(LoadGameplay(1f));
        }
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
        SceneManager.LoadScene("StageSelect");
    }
}

