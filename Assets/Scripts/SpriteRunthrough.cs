using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpriteRunthrough : MonoBehaviour
{
    private SpriteRenderer myRenderer;

    public Sprite[] sprites;

    public float stepLength;
    public float destroyOnEnd;
    public bool repeater;

    private void Start()
    {
        myRenderer = GetComponent<SpriteRenderer>();

        StartCoroutine(SpritesGoing());
    }

    IEnumerator SpritesGoing()
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            myRenderer.sprite = sprites[i];
            yield return new WaitForSeconds(stepLength);
        }
        if (destroyOnEnd > 0)
        {
            yield return new WaitForSeconds(destroyOnEnd);
            Destroy(gameObject);
        }
        if (repeater)
            Repeating();
    }

    void Repeating()
    {
        StartCoroutine(SpritesGoing());
    }
}
