using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Poison : MonoBehaviour
{
    public AudioSource source;
    public AudioClip glass, bubbling;

    // Start is called before the first frame update
    void Start()
    {
        source = GameObject.Find("soundSource").GetComponent<AudioSource>();
        source.PlayOneShot(glass);
        source.PlayOneShot(bubbling);
    }

    // Update is called once per frame
    void Update()
    {
        Invoke("Disappear", 7.5f);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("In poison");
        if (collision.gameObject.transform == gameObject.transform.parent)
        {
            collision.gameObject.GetComponent<FighterController>().TakeDamageNoHitStun(0.1f);
        }
    }

    private void Disappear()
    {
        Destroy(gameObject);
    }
}
