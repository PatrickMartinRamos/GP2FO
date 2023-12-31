using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthPack : MonoBehaviour
{
    playerManager playermanager;
    public dropItems healthDrop;

    private void Start()
    {
        playermanager = FindAnyObjectByType<playerManager>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
            playermanager.playerHealth += healthDrop.healthPack ;
        }
    }
}
