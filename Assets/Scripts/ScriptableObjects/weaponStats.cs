using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapons",menuName ="Weapons/Guns")]
public class weaponStats : ScriptableObject
{
    public float weaponDamage;
    public float bulletSpeed;
    public float fireRate;
    public GameObject bulletPrefab;
    public float reloadTime;
    public int magazineSize;
}
