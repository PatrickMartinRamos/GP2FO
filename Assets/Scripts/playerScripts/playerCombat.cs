using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCombat : MonoBehaviour
{
    public Transform shootingPoint;
    public weaponStats M4AIstats;

    playerAnimation playerAnimation;
    playerController playerController;

    //indicate if the player is currently shooting.
    public bool isShooting = false;

    private float nextFireTime;
    public int magazineSize;
    public int ammoBag;
    private int initialAmmo;

    public void Start()
    {
        playerController = GetComponent<playerController>();
        playerAnimation = GetComponent<playerAnimation>();
        magazineSize = M4AIstats.magazineSize;
        ammoBag = M4AIstats.ammoBag;
        initialAmmo = M4AIstats.ammoBag;
    }

    #region Handle StartShootingAction
    public void startShootAction()
    {
        playerAnimation.startShootAnimation();

        // Check if shooting is allowed based on timing and remaining ammo.
        if (playerController.startShooting && Time.time >= nextFireTime && magazineSize > 0)
        {
            nextFireTime = Time.time + M4AIstats.fireRate;

            // Instantiate a bullet object at the shooting point.
            Instantiate(M4AIstats.bulletPrefab, shootingPoint.position, shootingPoint.rotation);

            // Deduct one bullet from the magazine.
            magazineSize--;
        }
    }
    #endregion

    // Stop the shooting animation.
    public void stopShootAction()
    {
        playerAnimation.stopShootAnimation();
    }
    public void resetGun()
    {
        ammoBag = initialAmmo;
    }
}
