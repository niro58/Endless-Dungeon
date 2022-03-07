using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Reflection;
using System;
public class Player : MonoBehaviour
{
    public PlayerStats playerStats;
    public float health;
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
        GlobalVar.enemiesLeft = 0;
        GlobalVar.currentLevel = 0;

        playerStats = GlobalVar.playerStats;
        health = playerStats.health;
        gunParent = gameObject.transform.Find("Guns");
        currentGun = gunParent.GetChild(currentGunSlot).gameObject;
        player = gameObject;
        GlobalVar.player = this;

        //StartCoroutine(GlobalFunctions.MakeTransition("SceneEnter", "Transition_Shrink"));
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GlobalVar.importantPrefabs["Pause"].SetActive(!GlobalVar.importantPrefabs["Pause"].activeSelf);
        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.layer == 8 || col.gameObject.layer == 9)
        {
            EnemyStats entityStats = col.gameObject.GetComponent<EnemyStats>();
            GetHit(entityStats.onCollisionDamage);
        }
    }
    public void GetHit(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            StartCoroutine(GlobalFunctions.MakeTransition("Death", "Transition_Increase"));
            GlobalFunctions.SetAllBehaviour(gameObject, false);
        }
        StartCoroutine(OnDamageImmunity(2, gameObject.GetComponent<PolygonCollider2D>()));
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
    public void AddBuff(GunMod gunBuff)
    {
        Transform gunModParent = gunParent.transform.Find(gunBuff.gun.ToString()).GetComponent<Gun>().modsParent;

        GunBuffDisplay gunModDisplay = gunModParent.Find(gunBuff.modPart.ToString()).GetComponent<GunBuffDisplay>();
        gunModDisplay.gunBuffs.Add(gunBuff);
        gunModDisplay.updateBuffs();
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
                GlobalVar.playerGunCount += 1;
                child.SetSiblingIndex(GlobalVar.playerGunCount);
                child.GetComponent<Gun>().isActive = true;
                GlobalVar.importantPrefabs["UIGuns"].GetComponent<TextMeshProUGUI>().text += Environment.NewLine + GlobalVar.playerGunCount + " - " + child.name;
            }
        }

    }
    public void UpdatePlayerStats()
    {
        playerStats.LoadData();
        Gun gunStats = currentGun.GetComponent<Gun>();// Get Gun stats script
        playerStats.damage += gunStats.damage;
        playerStats.damageInc += gunStats.damageInc;
        playerStats.fireRate += gunStats.fireRate;
        playerStats.bulletSpeed += gunStats.bulletSpeed;
        playerStats.bulletRange += gunStats.bulletRange;
        playerStats.accuracy += gunStats.accuracy;
        // Mod stats added to root object
        foreach (Transform mod in currentGun.transform.Find("Mods"))
        {
            if(mod.name == "Buff")
            {
                GunBuffDisplay buffScript = mod.gameObject.GetComponent<GunBuffDisplay>();

                playerStats.damageInc += buffScript.damageInc;
                playerStats.fireRateRed += buffScript.fireRateRed;
                playerStats.bulletSpeed += buffScript.bulletSpeed;
                playerStats.bulletRange += buffScript.bulletRange;
                playerStats.accuracyRed += buffScript.accuracyRed;
            }
            else
            {
                GunModDisplay modScript = mod.gameObject.GetComponent<GunModDisplay>();

                playerStats.damageInc += modScript.damageInc;
                playerStats.fireRateRed += modScript.fireRateRed;
                playerStats.bulletSpeed += modScript.bulletSpeed;
                playerStats.bulletRange += modScript.bulletRange;
                playerStats.accuracyRed += modScript.accuracyRed;
            }
        }
    }
}
