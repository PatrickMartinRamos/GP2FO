using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class enemyScript : MonoBehaviour
{
    public enemyStats zombieStats;

    public NavMeshAgent enemy;
    private Transform playerTransform;
    private float initialZombieHealth;
    public Animator animator;

    gameManager gameManager;
    playerManager playerManager;
    scoreManager scoreManager;
    private bool isAttaking = false;

    private float lastAttackTime = 0f;

    public GameObject[] dropItems;
    

    private void Start()
    {
        scoreManager = FindAnyObjectByType<scoreManager>();
        gameManager = FindAnyObjectByType<gameManager>();
        playerManager = FindAnyObjectByType<playerManager>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        initialZombieHealth = zombieStats.zombieHealth;
    }
    public void Update()
    {
        enemy.SetDestination(playerTransform.position);
        zombieAttack();
    }

    public void takeDamage(float damage)
    {
        initialZombieHealth -= damage;

        if (initialZombieHealth <= 0)
        {
            Destroy(gameObject);
            scoreManager.addScore(2);
            if (dropItems.Length > 0 && Random.Range(0f, 1f) < gameManager.dropItems.dropRate) 
            {

                int randomDrop = Random.Range(0, dropItems.Length);

                Instantiate(dropItems[randomDrop], transform.position, Quaternion.identity);
            }
        }
    }

    public void zombieAttack()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= zombieStats.attackRange && Time.time - lastAttackTime >= zombieStats.attackSpeed)
        {
            Debug.Log("Attack triggered");
            playerManager.playerHealth -= zombieStats.damage;
            lastAttackTime = Time.time;
            isAttaking = true;
        }
        if (isAttaking)
        {
            animator.SetTrigger("zomAttack");
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, zombieStats.attackRange);
    }
}
    