using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonManager : MonoBehaviour
{


    public CinemachineVirtualCamera topDownView;
    public CinemachineFreeLook thirdPersonView;

    private playerController playerController;
    private spawnManager spawnManager;

    public GameObject MainMenuUI;

    // Start is called before the first frame update
    private void Start()
    {
        playerController = FindObjectOfType<playerController>();
        spawnManager = FindObjectOfType<spawnManager>();
    }
    public void switchView()
    {      
        topDownView.gameObject.SetActive(true);      
        thirdPersonView.gameObject.SetActive(false);
        MainMenuUI.gameObject.SetActive(false);
    }

    public void activateGameContol()
    {
        playerController.isGameStart = true;
    }

    public void startEnmeySpawn()
    {
       spawnManager.startSpawningEnemy();
    }
}
