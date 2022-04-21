using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 4.5f;
    // Start is called before the first frame update
    void Start()
    {

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
            Destroy(gameObject);
        }

    }
}
