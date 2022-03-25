using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VersusManager : MonoBehaviour
{
    public GameObject img1;
    public GameObject img2;
    public Sprite [] sprites;
    Image imageSprite1;
    Image imageSprite2;

    int uid1 = SelectorManager.user1;
    int uid2 = SelectorManager.user2;
    // Start is called before the first frame update
    void Start()
    {
        imageSprite1 = img1.GetComponent<Image> ();
        imageSprite2 = img2.GetComponent<Image> ();
        imageSprite1.sprite = sprites [uid1];
        imageSprite2.sprite = sprites [uid2];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
