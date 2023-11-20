using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class scoreManager : MonoBehaviour
{

    public int score = 0;
    public TextMeshProUGUI scoreDisplay;
    public TextMeshProUGUI deathScreenSoreDisp;
    // Start is called before the first frame update
    void Start()
    {
        updateScore();
    }

    // Update is called once per frame
    public void addScore(int points)
    {
        score += points;
        updateScore();
    }

    public void updateScore()
    {
        scoreDisplay.text = "score: " + score.ToString();
        deathScreenSoreDisp.text = "score: \n" + score.ToString();
    }
    public void resetScore()
    {
        score = 0;
        updateScore();
    }
}
