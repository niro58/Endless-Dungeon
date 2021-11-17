using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStats : MonoBehaviour
{

    [Header("Entity Stats")]
    public int EntHealth = 1;
    public float EntSpeed = 1;

    
    [Header("Gun Stats")]
    public float Damage = 0;
    public float FireRate = 0;// In seconds
    public float BulletSpeed = 0;
    public float BulletRange = 0;

    [Header("Collision Stats")]
    public int onCollisionDamage = 1;
    public int onCollisionKnockback = 1;
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == 12)// If layer == Bullet
        {
            EntHealth -= GlobalVar.sumStats.Damage;
        }
        if(EntHealth <= 0)
        {
            Destroy(gameObject);
        }
        
    }
}
