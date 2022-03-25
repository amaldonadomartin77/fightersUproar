using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SelectorManager : MonoBehaviour
{
    //serializedField the gameobject and to create a 2*2 array.
    //followed by the online tutorial


    public Text playerNameText;
    public Text playerSkill1Text;
    public Text playerSkill2Text;
    public Text playerSkill3Text;
    public Button selectBtn;
    public Text playerTitleName;

    public static int user1;
    public static int user2;
    int userid;
    bool isOne;


    public GameObject BK1;
    public GameObject BK2;
    public GameObject BK3;
    public GameObject BK4;

    public AudioSource audioSource;
    public AudioClip cursorSound;
    public AudioClip backSound;
    public AudioClip confirmSound;

    string[] characterNameList = { "Ace", "Bella", "Isaac", "Katsu" };

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
    bool isMoving = false;

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
        BK1.SetActive(false);
    }


    void AddRowToGrid(int index, GameObject[] row)
    {

        for (int i = 0; i < row.Length; i++)
        {

            grid[index, i] = row[i];
            // print("info: "+index+"--"+i);

        }
    }


    // Update is called once per frame
    void Update()
    {
        print("userid: "+userid);

        playerNameText.text = characterNameList[(int)positionIndex.x];
        playerSkill1Text.text = characterSkillList[(int)positionIndex.x, 0];
        playerSkill2Text.text = characterSkillList[(int)positionIndex.x, 1];
        playerSkill3Text.text = characterSkillList[(int)positionIndex.x, 2];
        // print("p: "+(int)positionIndex.y+ (int)positionIndex.x);

        //move the selector
        //checking if the keyboard is pressed or not
        float x = Input.GetAxisRaw("Horizontal"); //returns -0,1
        // float y = Input.GetAxisRaw( "Vertical" );
        if (x != 0 && !isMoving)
        {
            audioSource.PlayOneShot(cursorSound);
            MoveSelector(x, 0);
        }


        //if press the enter key
        if (Input.GetKeyDown(KeyCode.Return))
        {

            // string levelID = currentSlot.GetComponet<LevelSelectItemScript>().levelID;

            if(isOne == true){
                user1 = userid;
                isOne = false;
                playerTitleName.text = "Player 2";
            }
            else{
                user2 = userid;
                StartCoroutine(LoadGameplay(1f));
            }

        }


    }
    //void OnTriggerStay(Collider other){

    //}


    void MoveSelector(float x, float y)
    {
        if (isMoving == false)
        {

            isMoving = true;

            if (x > 0)
            {
                if (positionIndex.x < cols - 1)
                {
                    positionIndex.x += 1;
                    userid +=1;
                    // print("pp: "+positionIndex.x);
                }

            }
            else if (x < 0)
            {

                if (positionIndex.x > 0)
                {

                    positionIndex.x -= 1;
                    userid -=1;
                    //  print("pp: "+positionIndex.x);
                }
            }
            //  else if(y > 0){

            //      if (positionIndex.y > 0){

            //          positionIndex.y -= 1;

            //      }
            //  }
            //  else if( y < 0){

            //      if (positionIndex.y < rows - 1){

            //          positionIndex.y += 1;
            //      } 
            //  }
        }
        currentSlot = grid[(int)positionIndex.y, (int)positionIndex.x];
        // print("p: " + (int)positionIndex.y + (int)positionIndex.x);
        if ((int)positionIndex.x == 0)
        {
            BK1.SetActive(false);
            BK2.SetActive(true);
            BK3.SetActive(true);
            BK4.SetActive(true);
        }
        else if ((int)positionIndex.x == 1)
        {
            BK1.SetActive(true);
            BK2.SetActive(false);
            BK3.SetActive(true);
            BK4.SetActive(true);
        }
        else if ((int)positionIndex.x == 2)
        {
            BK1.SetActive(true);
            BK2.SetActive(true);
            BK3.SetActive(false);
            BK4.SetActive(true);
        }
        else if ((int)positionIndex.x == 3)
        {
            BK1.SetActive(true);
            BK2.SetActive(true);
            BK3.SetActive(true);
            BK4.SetActive(false);
        }
        //  selector.transform.position = currentSlot.transform.position;
        Invoke("ResetMoving", 0.2f);
    }

    void ResetMoving()
    {
        isMoving = false;
        // print("here");
    }
    public void ClickSelectBtn()
    {
        if(isOne == true){
            user1 = userid;
            isOne = false;
            playerTitleName.text = "Player 2";
        }
        else{
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    private IEnumerator LoadGameplay(float time)
    {
        audioSource.PlayOneShot(confirmSound);
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

