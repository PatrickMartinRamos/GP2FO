using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    public float timer;
    public TextMeshProUGUI displayTime;
    public GameObject surviveScreen;
    scoreManager scoreManager;
    buttonManager buttonManager;

    public bool isGameStart;
    public bool shouldSpawn = false;

    public TextMeshProUGUI displayFinalScore;
    public TMP_InputField setTimer;
    private float initialTime;

    public bool isVictory = false;

    public dropItems dropItems;
    public GameObject player;
    public Transform playerSpawnPos;

    private void Start()
    {
        buttonManager = FindAnyObjectByType<buttonManager>();
        scoreManager = FindAnyObjectByType<scoreManager>();

        initialTime = timer ;
    }

    // Update is called once per frame
    void Update()
    {
        activateVictory();
        timeSet();
    }

    public void timeSet()
    {
        if (timer > 0 && isGameStart)
        {
            timer -= Time.deltaTime;
            displayTime.text = FormatTime(timer);

            if (timer <= 0)
            {
                // Debug.Log("time end");
                isVictory = true;
                isGameStart = false;
                shouldSpawn = false;
                resetGameObjects();
            }
        }
    }


    public void activateVictory()
    {
        if(isVictory)
        {
            buttonManager.player.SetActive(false);
            surviveScreen.SetActive(true);
            buttonManager.currentGameState = buttonManager.GameState.victoryScene;
            displayFinalScore.text = "Final Score \n" + scoreManager.score.ToString();
        }
        else
        {
            surviveScreen.SetActive(false);
        }
    }

    string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void resetTime()
    {
        timer = initialTime;
        isVictory = false;
    }

    public void resetGameObjects()
    {
        //destroy enemy and dropItems when called
        GameObject[] enemyWithTag = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject obj in enemyWithTag)
        {
            Destroy(obj);
        }
        GameObject[] ammoPack = GameObject.FindGameObjectsWithTag("ammoPack");
        foreach (GameObject obj in ammoPack)
        {
            Destroy(obj);
        }

        GameObject[] healthPack = GameObject.FindGameObjectsWithTag("healthPack");
        foreach (GameObject obj in healthPack)
        {
            Destroy(obj);
        }
    }

    public void UpdateTimerFromInputField()
    {
        if (float.TryParse(setTimer.text, out float newTime))
        {
            timer = newTime;
            initialTime = newTime;
        }
        else
        {
            Debug.LogError("Invalid input for timer.");
        }
    }

    public void resetPlayerPos()
    {
        player.transform.DOMove(playerSpawnPos.position, .1f);
        player.transform.DORotate(new Vector3(0, 190,0), .1f);
    }
}
