using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour
{

    public weaponStats statM4AI;
    private float initialWeapDamage;


    private void Start()
    {
        initialWeapDamage = statM4AI.weaponDamage;
    }

    public void FixedUpdate()
    {
      transform.Translate(Vector3.forward * statM4AI.bulletSpeed * Time.deltaTime);
    }

    public void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Buildings"))
        {
            Destroy(gameObject);
        }
    }
    public void OnTriggerEnter(Collider other)
    {       
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemyScript zombieEnemy = other.GetComponent<enemyScript>();

            zombieEnemy.takeDamage(initialWeapDamage);

           // Debug.Log("enemyHit");
            //Destroy(other.gameObject);
            Destroy(gameObject);
        } 
    }

    public void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
