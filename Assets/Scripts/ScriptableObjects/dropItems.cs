using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "items",menuName = "items/dropItems")]
public class dropItems : ScriptableObject
{
    public float dropRate;
    public float healthPack;
    public int ammoPack;
}
