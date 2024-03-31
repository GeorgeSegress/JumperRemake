using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrogRespawner : MonoBehaviour
{
    public GameObject gameOverText;
    public GameObject winText;
    public GameObject myFrog;
    public Transform frogSpawn;
    public GameObject[] myLiveSymbols;
    public GameObject[] countdowns;

    public Slider timeSlider;

    public int lives = 5;
    public int scores = 0;
    public float maxTime = 30;
    private bool gameGoing = false;

    private void Start()
    {
        timeSlider.maxValue = maxTime;
        timeSlider.value = maxTime;

        StartCoroutine(GameStart());
    }

    private IEnumerator GameStart()
    {
        for (int i = 0; i < countdowns.Length; i++)
        {
            if (i != 0)
                countdowns[i - 1].SetActive(false);
            countdowns[i].SetActive(true);
            yield return new WaitForSeconds(1);
        }
        countdowns[countdowns.Length - 1].SetActive(false);
        gameGoing = true;
        Respawn(false);
    }

    private void Update()
    {
        if (gameGoing)
        {
            maxTime -= Time.deltaTime;
            timeSlider.value = maxTime;

            if (maxTime <= 0)
            {
                GameOver();
            }
        }
    }

    public void MadeScore()
    {
        scores++;
        if (scores >= 5)
            GameWon();
        else
            Respawn(false);
    }

    public void Respawn(bool livesNeeded)
    {
        if(gameGoing && lives > 0)
        {
            if (livesNeeded)
            {
                myLiveSymbols[lives - 1].gameObject.SetActive(false);
                lives--;
            }
            Instantiate(myFrog, frogSpawn.position, frogSpawn.rotation);
        } else if(lives == 0 && gameGoing)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        gameGoing = false;
        gameOverText.SetActive(true);
        if (FindObjectOfType<FrogController>() != null)
            FindObjectOfType<FrogController>().Death(false);
    }

    public void GameWon()
    {
        gameGoing = false;
        winText.SetActive(true);
    }
}
