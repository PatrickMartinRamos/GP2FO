using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ammoPack : MonoBehaviour
{
    playerCombat playerCombat;
    public dropItems ammoDrop;

    private void Start()
    {
        playerCombat = FindAnyObjectByType<playerCombat>();

    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
            playerCombat.ammoBag += ammoDrop.ammoPack;
        }
    }
}
