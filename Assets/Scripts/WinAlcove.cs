using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WinAlcove : MonoBehaviour
{
    public Sprite wonAlcove;
    private bool alreadyWon;
    public GameObject winSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !alreadyWon)
        {
            collision.GetComponent<FrogController>().Death(false);
            Scored();
        }
    }

    void Scored()
    {
        if (!alreadyWon)
        {
            Instantiate(winSound);
            alreadyWon = true;
            GetComponent<SpriteRenderer>().sprite = wonAlcove;
            FindObjectOfType<FrogRespawner>().MadeScore();
        }
    }
}
