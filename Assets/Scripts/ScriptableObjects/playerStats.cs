using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Soldier", menuName = "Player/Player Stats")]
public class playerStats : ScriptableObject
{
    public float health;
    public float walkSpeed;
    public float runSpeed;
    public float attackDamage;
}
