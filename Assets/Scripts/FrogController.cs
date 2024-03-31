using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogController : MonoBehaviour
{
    public GameObject Explosion;
    public GameObject deathSound;
    public GameObject roadSound;
    public GameObject splashSound;
    public GameObject ribbitSound;
    private List<GameObject> myLogs = new List<GameObject>();
    /// <summary>
    /// start on idle, then leap: 0,1 = up; 2,3 = right; 4,5 = down; 6,7 = left
    /// </summary>
    public Sprite[] moveSprites; 
    private SpriteRenderer myRenderer;

    private Vector2 moveInp;

    public float deadZone;
    public float maxWait = 0.3f;
    private float moveWait = -0.5f;
    public bool swimming = false;

    private Vector2 currentTarget;

    private void Start()
    {
        currentTarget = transform.position;
        myRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        moveInp = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (moveWait > maxWait && moveInp.x > deadZone && transform.position.x < 6)
            StartLeap(Vector2.right);
        else if (moveWait > maxWait && moveInp.x < -deadZone && transform.position.x > -6)
            StartLeap(Vector2.left);
        else if (moveWait > maxWait && moveInp.y > deadZone)
            StartLeap(Vector2.up);
        else if (moveWait > maxWait && moveInp.y < -deadZone && transform.position.y > -7)
            StartLeap(Vector2.down);

        if (moveWait <= maxWait)
            moveWait += Time.deltaTime;
    }

    void StartLeap(Vector2 direction)
    {
        //StopAllCoroutines();
        //transform.position = currentTarget;
        StartCoroutine(Leap(direction));
    }

    public IEnumerator Leap(Vector2 direction)
    {
        moveWait = 0;
        Vector2 newPos = (Vector2)transform.position + direction;
        currentTarget = newPos;
        Instantiate(ribbitSound);

        int spriteNum = 0;
        if (direction == Vector2.right) spriteNum = 2;
        if (direction == Vector2.down) spriteNum = 4;
        if (direction == Vector2.left) spriteNum = 6;
        myRenderer.sprite = moveSprites[spriteNum + 1];

        for(int i = 0; i < 6; i++)
        {
            transform.position = Vector2.Lerp(transform.position, newPos , 0.5f);
            yield return new WaitForSeconds(0.01f);
        }

        
        myRenderer.sprite = moveSprites[spriteNum];
        transform.position = newPos;

        //yield return null;

        if (myLogs.Count == 0 && swimming)
            Death(true);
        else if (swimming)
            Instantiate(splashSound);
        else if (!swimming)
            Instantiate(roadSound);
    }

    public void Swimming(bool yes)
    {
        swimming = yes;
    }

    public void AddLog(GameObject myLog)
    {
        myLogs.Add(myLog);
    }

    public void RemoveLog(GameObject removedLog)
    {
        if(myLogs.Contains(removedLog))
            myLogs.Remove(removedLog);
        if (myLogs.Count == 0 && swimming)
            Death(true);
    }

    public void Death(bool respawn)
    {
        Instantiate(deathSound);
        Instantiate(Explosion, transform.position, Quaternion.identity);
        if(respawn)
            FindObjectOfType<FrogRespawner>().Respawn(true);
        Destroy(gameObject);
    }
}
