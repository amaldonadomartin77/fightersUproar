using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Fireball : MonoBehaviour
{
    public float speed = 4.5f;
    public AudioSource source;
    public AudioClip cast, impact;

    // Start is called before the first frame update
    void Start()
    {
        source = GameObject.Find("soundSource").GetComponent<AudioSource>();
        cast = (AudioClip)AssetDatabase.LoadAssetAtPath("Assets/Sounds/Gameplay/fireballcast.ogg", typeof(AudioClip));
        impact = (AudioClip)AssetDatabase.LoadAssetAtPath("Assets/Sounds/Gameplay/fireballimpact.ogg", typeof(AudioClip));
        source.PlayOneShot(cast);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * Time.deltaTime * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.transform == gameObject.transform.parent)
        {
            collision.gameObject.GetComponent<FighterController>().TakeDamage(10, 1, false);
            source.PlayOneShot(impact);
            Destroy(gameObject);
        }

    }
}
