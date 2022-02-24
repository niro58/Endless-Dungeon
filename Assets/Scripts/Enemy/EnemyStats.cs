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
        
        int mobType = Random.Range(0, 5);
        Color32 color = new Color32();
        switch (mobType)
        {
            case 0:
                color = Color.green;
                health += healthMultiplier * 0.2f * level;
                break;
            case 1:
                color = Color.blue;
                speed += speedMultiplier * 0.2f * level;
                break;
            case 2:
                color = Color.red;
                damage += damageMultiplier * 0.2f * level;
                break;
            case 3:
                color = Color.yellow;
                fireRate += fireRateMultiplier * 0.2f * level;
                break;
        }
        gameObject.GetComponent<SpriteRenderer>().color = color;
        health += healthMultiplier * level;
        speed += speedMultiplier * level;
        damage += damageMultiplier * level;
        fireRate += fireRateMultiplier * level;
        bulletSpeed += bulletSpeedMultiplier * level;
        bulletRange += bulletRangeMultiplier * level;
        onCollisionDamage += onCollisionDamageMultiplier * level;
        onCollisionKnockback += onCollisionKnockbackMultiplier * level;
    }
    public void getHit()
    {

        health -= GlobalVar.playerStats.damage;
        if (health <= 0)
        {
            speed = 0;

            GlobalFunctions.SetAllBehaviour(gameObject, false);
            LeanTween.alpha(gameObject, 0, 2);
            Destroy(gameObject, 2);

            GlobalVar.playerStats.coins += 1;
            GlobalVar.enemiesLeft -= 1;
            return;
        }
        else
        {
            animator.Play("Enemy-Hit");
        }
    }

}
