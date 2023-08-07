using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/WeaponData", order = 1)]
public class WeaponData : ScriptableObject
{
    public int index;

    public Type bulletType;
    public AttackType attackType;

    [SerializeField]
    public GameObject bulletPrefab;

    public float bulletLifeTime = 1.5f;

    public GameObject bulletImage;
}
