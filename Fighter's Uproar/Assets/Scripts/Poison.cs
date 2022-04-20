using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Invoke("Disappear", 10);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("In poison");
        if (collision.gameObject.tag == "Bella")
        {
            collision.gameObject.GetComponent<FighterController>().TakeDamageNoHitStun(0.1f);
        }
    }

    private void Disappear()
    {
        Destroy(gameObject);
    }
}
