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

    [Header("Level Multiplier")]
    public float healthMultiplier;
    public float speedMultiplier;
    public float damageMultiplier;
    public float fireRateMultiplier;
    public float bulletSpeedMultiplier;
    public float bulletRangeMultiplier;
    public float onCollisionDamageMultiplier;
    [Header("Max Values")]
    public float maxSpeed;
    public float maxFirerate;
    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();

        float level = GlobalVar.currentLevel;
        
        int mobType = Random.Range(0, 4);
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

        if(fireRate <= maxFirerate)
        {
            fireRate = maxFirerate;
        }
        if(speed >= maxSpeed || (maxSpeed < 1 && speed <= maxSpeed))
        {
            speed = maxSpeed;
        }
    }
    public void GetHit()
    {
        health -= GlobalVar.player.playerStats.damage;
        if (health <= 0)
        {
            speed = 0;

            GlobalFunctions.SetAllBehaviour(gameObject, false);
            LeanTween.alpha(gameObject, 0, 0.4f);
            Destroy(gameObject, 1);
            GlobalVar.playerStats.AddCoin();
            GlobalVar.player.playerStats.coins += GlobalVar.currentLevel;
            GlobalVar.enemiesLeft -= 1;
            return;
        }
        else
        {
            StartCoroutine(ColorChangeOnHit(gameObject));
        }
    }
    public static IEnumerator ColorChangeOnHit(GameObject target)
    {
        Color32 startCol = target.GetComponent<SpriteRenderer>().color;
        LeanTween.color(target, new Color32(231, 16, 16, 96), 0.1f);
        yield return new WaitForSeconds(0.12f);
        LeanTween.color(target, startCol, 0.15f);
    } 

}
