using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Reflection;
public class Player : MonoBehaviour
{
    public PlayerStats playerStats;

    [HideInInspector]
    public Transform gunParent;
    [HideInInspector]
    public GameObject currentGun;
    [HideInInspector]
    public int currentGunSlot = 0;

    [HideInInspector]
    public GameObject player;
    public void Awake()
    {
        GlobalFunctions.UpdateImportantGameObjects();

        playerStats = GlobalVar.playerStats;
        gunParent = gameObject.transform.Find("Guns");
        currentGun = gunParent.GetChild(currentGunSlot).gameObject;
        player = gameObject;
        GlobalVar.player = this;

    }
    private void Update()
    {
        UpdatePlayerStats();
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.layer == 8 || col.gameObject.layer == 9)
        {
            EnemyStats entityStats = col.gameObject.GetComponent<EnemyStats>();
            playerStats.health -= (int)Mathf.Ceil(entityStats.onCollisionDamage);
            if(playerStats.health == 0)
            {

            }
            StartCoroutine(OnDamageImmunity(2, gameObject.GetComponent<PolygonCollider2D>()));
        }
    }
    IEnumerator OnDamageImmunity(float time, Collider2D collider)
    {
        Physics2D.IgnoreLayerCollision(6, 8, true);
        Physics2D.IgnoreLayerCollision(6, 9, true);

        LeanTween.alpha(gameObject, 0.3f, time / 2);
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;

        yield return new WaitForSeconds(time / 2);

        LeanTween.color(gameObject, Color.white , time / 2);

        yield return new WaitForSeconds(time / 2);

        Physics2D.IgnoreLayerCollision(6, 8, false);
        Physics2D.IgnoreLayerCollision(6, 9, false);

    }
    public void AddMod(GunMod gunMod)
    {
        Transform gunModParent = gunParent.transform.Find(gunMod.gun.ToString()).GetComponent<Gun>().modsParent;

        GunModDisplay gunModDisplay = gunModParent.Find(gunMod.modPart.ToString()).GetComponent<GunModDisplay>();
        gunModDisplay.gunMod = gunMod;
        gunModDisplay.updateMod();
        UpdatePlayerStats();
    }
    public void AddWeapon(GameObject weapon)
    {
        string weaponName = weapon.GetComponent<Gun>().gunName.ToString();
        for (int slot = 0; slot < gunParent.childCount; slot++)
        {
            Transform child = gunParent.transform.GetChild(slot);
            if(child.name == weaponName)
            {
                child.GetComponent<Gun>().isActive = true;
            }
        }

    }
    public void UpdatePlayerStats()
    {
        playerStats.LoadData();

        Gun gunStats = currentGun.GetComponent<Gun>();// Get Gun stats script
        playerStats.damageIncrease += gunStats.damageRed;
        playerStats.fireRate += gunStats.fireRate;
        playerStats.bulletSpeed += gunStats.bulletSpeed;
        playerStats.bulletRange += gunStats.bulletRange;
        playerStats.accuracy += gunStats.accuracy;
        // Mod stats added to root object
        foreach (Transform mod in currentGun.transform.Find("Mods"))
        {
            GunModDisplay modScript = mod.gameObject.GetComponent<GunModDisplay>();

            playerStats.damageIncrease += modScript.damageRed;
            playerStats.fireRateIncrease += modScript.fireRateRed;
            playerStats.bulletSpeed += modScript.bulletSpeed;
            playerStats.bulletRange += modScript.bulletRange;
            playerStats.accuracy += modScript.accuracy;
        }
    }
}
