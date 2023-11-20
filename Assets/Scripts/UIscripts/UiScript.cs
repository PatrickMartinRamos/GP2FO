using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiScript : MonoBehaviour
{
    public TextMeshProUGUI healthDisplay;
    public TextMeshProUGUI magazinDisplay;

    public GameObject player;
    playerManager playerManager;
    playerCombat playerCombat;

    public GameObject bulletReloadImageParent;
    public Image reloadArrowImage;

    public TextMeshProUGUI displayReloadTime;

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
        health = playerManager.playerHealth;
        healthDisplay.text = "Health: " + health.ToString();
    }

    public void displayMagazine()
    {
        ammoBag = playerCombat.ammoBag;
        magazine = playerCombat.magazineSize;
        magazinDisplay.text = "Ammo " + magazine.ToString()+ "/ "+ammoBag;
    }

    public void showReloadImage()
    {
        bulletReloadImageParent.SetActive(true);
        reloadArrowImage.transform.DORotate(new Vector3(0, 0, -360), 2.0f, RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.Linear);
        magazinDisplay.enabled = false;
    }
    public void hideReloadImage()
    {
        bulletReloadImageParent.SetActive(false);
        magazinDisplay.enabled = true;
    }
}
