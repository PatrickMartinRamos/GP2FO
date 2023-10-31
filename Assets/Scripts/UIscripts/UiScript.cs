using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiScript : MonoBehaviour
{
    public TextMeshProUGUI healthDisplay;
    public TextMeshProUGUI magazinDisplay;

    public GameObject player;
    playerManager playerManager;
    playerCombat playerCombat;

    private float health;
    private int magazine;
    private int ammoBag;

    public void Start()
    {
        playerManager = player.GetComponent<playerManager>();
        playerCombat = player.GetComponent<playerCombat>();
    }
    private void Update()
    {
        displayHealth();
        displayMagazine();
    }
    public void displayHealth()
    {
        health = playerManager.playerStats.health;
        healthDisplay.text = "Health: " + health.ToString();
    }

    public void displayMagazine()
    {
        ammoBag = playerCombat.ammoBag;
        magazine = playerCombat.magazineSize;
        magazinDisplay.text = "Ammo " + magazine.ToString()+ "/ "+ammoBag;
    }
}
