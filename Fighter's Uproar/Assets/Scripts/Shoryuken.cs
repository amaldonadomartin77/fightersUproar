using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoryuken : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Invoke("Disappear", 1);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("Hit by shoryu");
        if (collision.gameObject.transform == gameObject.transform.parent)
        {
            collision.gameObject.GetComponent<FighterController>().TakeDamageNoHitStun(15);
        }
    }

    private void Disappear()
    {
        Destroy(gameObject);
    }
}

