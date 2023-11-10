using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseMenuScript : MonoBehaviour
{

    gameManager gameManager;

    private void Start()
    {
        gameManager = FindAnyObjectByType<gameManager>();
    }

    private void Update()
    {

    }

    public void TogglePauseMenu()
    {

    }



    public void ReturnToMenu()
    {
        // Add code to return to the main menu or perform other actions.
    }
}
