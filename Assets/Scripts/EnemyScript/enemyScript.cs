using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyScript : MonoBehaviour
{
    public enemyStats zombieStats;

    public NavMeshAgent enemy;
    private Transform player;
    private float initialZombieHealth;
    public Animator animator;

   

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        initialZombieHealth = zombieStats.zombieHealth;
    }
    public void Update()
    {
        enemy.SetDestination(player.position);
        zombieAttack();
    }

    public void takeDamage(float damage)
    {
        initialZombieHealth -= damage;

        if (initialZombieHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void zombieAttack()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= zombieStats.attackRange)
        {
            animator.SetBool("attack", true);
            Debug.Log("Player is within attack range.");
        }
        else
        {
            animator.SetBool("attack", false);
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, zombieStats.attackRange);
    }
}
    