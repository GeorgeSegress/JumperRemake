using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cars : MonoBehaviour
{
    public float mySpeed;
    private int edge = 8;

    private void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(mySpeed, 0);
        if (mySpeed < 0)
            edge = -8;
    }

    private void FixedUpdate()
    {
        if((edge > 0 && transform.position.x > edge) || (edge < 0 && transform.position.x < edge))
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<FrogController>().Death(true);
        }
    }
}
