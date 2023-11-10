using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    public float timeRemaining;
    public TextMeshProUGUI displayTime;

    public bool isGameStart;
    public bool shouldSpawn = false;
    private float initialTime;

    public float dropRate;

    private void Start()
    {
        initialTime = timeRemaining;    
    }

    // Update is called once per frame
    void Update()
    {
        if(timeRemaining>0 && isGameStart)
        {
            timeRemaining -= Time.deltaTime;
            displayTime.text = FormatTime(timeRemaining);
        }
        if (timeRemaining <= 0)
        {
            Debug.Log("time end");
            isGameStart = false;
            shouldSpawn = false;
           
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
        timeRemaining = initialTime;
    }
}
