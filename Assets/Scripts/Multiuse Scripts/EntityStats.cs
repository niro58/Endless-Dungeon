using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStats : MonoBehaviour
{

    [Header("Entity Stats")]
    public int EntHealth;
    public float EntSpeed;
    public float EntDamage;

    [Header("Gun Stats")]
    public float GunFireRate = 1;// In seconds
    public float GunBulletSpeed;
    public float GunBulletRange;

    [Header("Collision Stats")]
    public int onCollisionDamage;
    public int onCollisionKnockback;
}
