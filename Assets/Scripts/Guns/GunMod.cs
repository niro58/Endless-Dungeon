using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Mod", menuName = "ScriptableObjects/Mod")]
public class GunMod : ScriptableObject
{
    public Sprite sprite;
    public enum ModType { Muzzle, Scope, Mag};
    public ModType modType;
    [Space(10)]
    public int damage;
    public float fireRateReduction;
    public float bulletSpeed;
    public float bulletRange;
}

