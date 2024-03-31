using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turts : Log
{
    private Collider2D myCollider;

    protected override void Start()
    {
        base.Start();

        myCollider = GetComponent<Collider2D>();
    }

    public void GoUp()
    {
        myCollider.enabled = true;
    }

    public void GoDown()
    {
        if (landed && myPlayer != null)
            myPlayer.GetComponent<FrogController>().RemoveLog(gameObject);
        myCollider.enabled = false;
    }
}
