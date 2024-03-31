using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : MonoBehaviour
{
    protected GameObject myPlayer;

    public float mySpeed;
    protected int edge = 15;
    protected bool landed = false;
    protected bool onTop;

    protected virtual void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(mySpeed, 0);
        if (mySpeed < 0)
            edge = -15;
    }

    protected virtual void Update()
    {
        if ((edge > 0 && transform.position.x > edge) || (edge < 0 && transform.position.x < edge))
            Death();

        if(landed && myPlayer != null)
        {
            onTop = myPlayer.transform.position.y == transform.position.y;

            if(onTop)
            {
                myPlayer.transform.position +=  new Vector3(mySpeed * Time.deltaTime, 0);
            }
        } else if(landed && myPlayer == null)
        {
            landed = false;
            onTop = false;
        }
    }

    protected virtual void Death()
    {
        if(myPlayer != null)
            myPlayer.GetComponent<FrogController>().RemoveLog(myPlayer);
        Destroy(gameObject);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            myPlayer = collision.gameObject;
            collision.GetComponent<FrogController>().AddLog(gameObject);
            landed = true;
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<FrogController>().RemoveLog(gameObject);
            landed = false;
        }
    }
}
