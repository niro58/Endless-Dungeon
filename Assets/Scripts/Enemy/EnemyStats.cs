using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [HideInInspector]
    public Animator animator;
    [Header("Entity Stats")]
    public float health = 1;
    public float speed = 1;

    
    [Header("Gun Stats")]
    public float damage = 0;
    public float fireRate = 0;// In seconds
    public float bulletSpeed = 0;
    public float bulletRange = 0;

    [Header("Collision Stats")]
    public float onCollisionDamage = 1;
    public float onCollisionKnockback = 1;

    [Header("Level Multiplier")]
    public float healthMultiplier;
    public float speedMultiplier;
    public float damageMultiplier;
    public float fireRateMultiplier;
    public float bulletSpeedMultiplier;
    public float bulletRangeMultiplier;
    public float onCollisionDamageMultiplier;
    public float onCollisionKnockbackMultiplier;
    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();

        float level = GlobalVar.CurrentLevel;

        health += healthMultiplier * level;
        speed += speedMultiplier * level;
        damage += damageMultiplier * level;
        fireRate += fireRateMultiplier * level;
        bulletSpeed += bulletSpeedMultiplier * level;
        bulletRange += bulletRangeMultiplier * level;
        onCollisionDamage += onCollisionDamageMultiplier * level;
        onCollisionKnockback += onCollisionKnockbackMultiplier * level;
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == 12)// If layer == Bullet
        {

            animator.Play("Enemy-Hit");
            Destroy(col.gameObject);
            health -= GlobalVar.playerStats.Damage;
        }
        if(health <= 0)
        {
            GlobalVar.PlayerCoins += 1;
            Destroy(gameObject);

            int enemiesLeft = GlobalVar.CurrentRoom.transform.Find("Enemies").childCount;
            if(enemiesLeft == 0)
            {
                GlobalVar.RoomCleared = true;

                Transform currRoom = GlobalVar.CurrentRoom.transform.Find("Room_Parts");
                foreach(Transform roomPart in currRoom)
                {
                    Transform doorParent = roomPart.Find("Doors");
                    foreach(Transform door in doorParent)
                    {
                        door.Find("Door_Light").gameObject.SetActive(true);
                    }
                }

            }
        }
        
    }
}
