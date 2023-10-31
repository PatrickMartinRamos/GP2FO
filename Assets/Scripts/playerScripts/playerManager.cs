using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerManager : MonoBehaviour
{
    public playerStats playerStats;

    public void Start()
    {
        
    }

    public void Update()
    {
        handleHealth();
        //Debug.Log(playerStats.health);
    }

    public void handleHealth()
    {
        if (playerStats.health <= 0)
        {
            //death animation
            //game overscreen
            Destroy(gameObject);
            //Debug.Log("die");
        }
    }
}

