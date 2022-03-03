using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "GunMod", menuName = "ScriptableObjects/GunMod")]
public class GunMod : ScriptableObject
{
    public string shortName;
    public Gun.GunName gun;
    public Sprite sprite;

    public enum ModPart { Muzzle, Optic, Mag, Buff};
    [Header("Mod Type")]
    public ModPart modPart;

    public enum FireMode {None ,Single, TripleShot, TripleShot_2};
    public FireMode fireMode;

    [Header("Mod Stats")]
    [Space(10)]
    public float damageRed;
    public float fireRateRed;
    public float bulletSpeed;
    public float bulletRange;
    public float accuracy;
}

