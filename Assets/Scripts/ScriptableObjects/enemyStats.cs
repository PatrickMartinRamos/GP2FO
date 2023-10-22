using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Enemy", menuName = "Zombie/Normal Zombie")]
public class enemyStats : ScriptableObject
{
    public float zombieHealth;
    public float damage;
    public float attackRange;
}
