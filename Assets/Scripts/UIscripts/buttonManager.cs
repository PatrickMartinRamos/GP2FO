using Cinemachine;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using static buttonManager;


public class buttonManager : MonoBehaviour
{
    public CinemachineVirtualCamera topDownView;
    public CinemachineFreeLook thirdPersonView;

    private gameManager gameManager;
    private spawnManager spawnManager;
    private playerManager playerManager;
    private playerCombat playerCombat;

    public GameObject MainMenuUI;
    public GameObject ingameUI;
    public GameObject deathScreenMenu;

    public GameObject pauseMenu;
    private bool isPaused;

    public GameObject player;

    public Image deathScreen;

    public enum GameState
    {
        MainMenu,
        InGame,
        Paused,
        deathScene
    }
    public GameState currentGameState = GameState.MainMenu;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
               ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
        Debug.Log(currentGameState);
    }

    // Start is called before the first frame update
    private void Start()
    {
        gameManager = FindObjectOfType<gameManager>();
        spawnManager = FindObjectOfType<spawnManager>();
        playerManager = FindObjectOfType<playerManager>();
        playerCombat = FindObjectOfType<playerCombat>();
    }
    public void switchView()
    {      
        topDownView.gameObject.SetActive(true);      
        thirdPersonView.gameObject.SetActive(false);
        MainMenuUI.gameObject.SetActive(false);
        ingameUI.gameObject.SetActive(true);
        currentGameState = GameState.InGame;
        Debug.Log(currentGameState);


    }

    public void activateGameContol()
    {
        gameManager.isGameStart = true;
    }

    public void startEnmeySpawn()
    {
       spawnManager.startSpawningEnemy();
    }

    public void PauseGame()
    {
        if (currentGameState == GameState.InGame)
        {
            isPaused = true;
            currentGameState = GameState.Paused;
            gameManager.isGameStart = false;
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
            Debug.Log("pause");
        }    
    }
    
    public void ResumeGame()
    {
        if (currentGameState == GameState.Paused)
        {
            isPaused = false;
            currentGameState = GameState.InGame;
            gameManager.isGameStart = true;
            Time.timeScale = 1f;
            pauseMenu.SetActive(false);
            Debug.Log("resume");
        }
    }
    public void returnToMenu()
    {
        if(currentGameState == GameState.Paused || currentGameState == GameState.deathScene)
        {
            currentGameState = GameState.MainMenu;
            pauseMenu.SetActive(false);
            gameManager.isGameStart = false;
            gameManager.shouldSpawn = false;
            Time.timeScale = 1f;
            isPaused = false;

            topDownView.gameObject.SetActive(false);
            thirdPersonView.gameObject.SetActive(true);
            MainMenuUI.gameObject.SetActive(true);
            ingameUI.gameObject.SetActive(false);
            deathScreenMenu.gameObject.SetActive(false);
            deathScreen.DOFade(0f, 01f);

            playerManager.ResetPlayerStats();
            playerCombat.resetGun();
            gameManager.resetTime();

            player.SetActive(true);

            GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject obj in objectsWithTag)
            {
                Destroy(obj);
            }
        }
    }
    public void showDeathScreen()
    {
        player.SetActive(false);

        currentGameState = GameState.deathScene;
        gameManager.shouldSpawn = false;
        gameManager.isGameStart = false;
        deathScreenMenu.gameObject.SetActive(true);

       
        deathScreen.DOFade(0.6f, 5f).OnComplete(() =>
        {
            if(deathScreen == null)
            {
                deathScreen.DOFade(0f, 0f);
            }   
        });
    }
}
