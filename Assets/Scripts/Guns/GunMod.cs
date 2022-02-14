using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "GunMod", menuName = "ScriptableObjects/GunMod")]
public class GunMod : ScriptableObject
{
    public string shortName;
    public Gun.GunName gun;
    public Sprite sprite;

    public enum ModType { Buff, Mod };
    public ModType modType;
    public enum ModPart { Muzzle, Optic, Mag, Buff};
    public ModPart modPart;

    public enum FireMode {None ,Single, TripleShot, TripleShot_2};
    public FireMode fireMode;
    [Space(10)]
    public float damage;
    public float fireRateReduction;
    public float bulletSpeed;
    public float bulletRange;
}

