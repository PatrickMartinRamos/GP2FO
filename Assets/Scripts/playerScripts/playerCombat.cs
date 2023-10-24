using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCombat : MonoBehaviour
{
    public Transform shootingPoint;

    //Weapon Stats
    public weaponStats M4AIstats;
    private float nextFireTime;

    //Animation
    playerAnimation playerAnimation;

    //Scripts
    playerController playerController;

    //Event handler
    public bool isShooting = false;

    public int magazineSize;

    public void Start()
    {
        playerController = GetComponent<playerController>();
        playerAnimation = GetComponent<playerAnimation>();
        magazineSize = M4AIstats.magazineSize;
        Debug.Log(magazineSize);
    }

    public void startShootAction()
    {
        playerAnimation.startShootAnimation();
       // Debug.Log("shooting");
        if(playerController.startShooting && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + M4AIstats.fireRate;

            Instantiate(M4AIstats.bulletPrefab, shootingPoint.position,shootingPoint.rotation);

            //Debug.Log("startshooting");
        }
    }

    public void stopShootAction()
    {
        playerAnimation.stopShootAnimation();
       // Debug.Log("stop");  

    }
}
