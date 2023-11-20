using DG.Tweening;
using UnityEngine;

public class playerManager : MonoBehaviour
{
    public playerStats playerStats;
    public float playerHealth;
    private float initialPlayerHealth;
    public buttonManager buttonManager;


    public void Start()
    {
        buttonManager = FindAnyObjectByType<buttonManager>();
        playerHealth = playerStats.health;

        initialPlayerHealth = playerStats.health;
        ResetPlayerStats();
    }

    public void Update()
    {
        //Debug.Log("player health" + playerHealth);
        handleHealth();
    }

    public void handleHealth()
    {
        Vector3 lookAt = Vector3.zero;
        lookAt.y = 190;
        if (playerHealth <= 0)
        {
            GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject obj in objectsWithTag)
            {          
                Destroy(obj);
            }
            buttonManager.showDeathScreen();    
            Debug.Log("die");
            transform.DORotate(lookAt, .5f);
        }
    }
    public void ResetPlayerStats()
    {
        playerHealth = initialPlayerHealth;
    }

     
}

