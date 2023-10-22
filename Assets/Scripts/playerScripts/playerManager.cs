using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerManager : MonoBehaviour
{
    public playerStats playerStats;

    private float playerHealth;

    public void Start()
    {
        playerStats.health = playerHealth;
    }

    public void Update()
    {
        
    }

}
