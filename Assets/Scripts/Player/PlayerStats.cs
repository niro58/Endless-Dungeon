using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Reflection;
public class PlayerStats : MonoBehaviour
{
    [Header("Main Stats")]
    public float health;
    public float damage;
    public float accuracy;
    public float speed;
    public float fireRate;
    public float bulletSpeed;
    public float bulletRange;

    [Header("Reduction Stats")]
    public float damageRed;
    public float accuracyRed;
    public float fireRateRed;
    public float bulletSpeedRed;
    public float bulletRangeRed;

    [HideInInspector]
    public Transform gunParent;
    [HideInInspector]
    public GameObject currentGun;
    [HideInInspector]
    public int currentGunSlot = 0;

    [HideInInspector]
    public GameObject player;

    [HideInInspector]
    public int coins;
    public void Awake()
    {
        gunParent = gameObject.transform.Find("Guns");
        currentGun = gunParent.GetChild(currentGunSlot).gameObject;

        foreach (FieldInfo field in this.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance))
        {
            string name = field.Name;
            switch (name)
            {
                case "health":case "damage":case "speed":case "bulletSpeed":case "bulletRange":
                case "fireRateRed":
                case "accuracyRed":
                    float value = float.Parse(field.GetValue(this).ToString());
                    
                    PlayerPrefs.SetFloat(name, value);// Only for testing, delete later, uncomment field.SetValue

                    if (!PlayerPrefs.HasKey(name))
                    {
                        //PlayerPrefs.SetFloat(name, value);
                    }
                    else
                    {
                        //field.SetValue(this, PlayerPrefs.GetFloat(name, value));
                    }

                    break;
            }

            
        }

        player = gameObject;

        updatePlayerStats();

        GlobalVar.playerStats = gameObject.GetComponent<PlayerStats>();


    }
    private void Update()
    {
        updatePlayerStats();
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.layer == 8 || col.gameObject.layer == 9)
        {
            EnemyStats entityStats = col.gameObject.GetComponent<EnemyStats>();
            health -= (int)Mathf.Ceil(entityStats.onCollisionDamage);
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
        updatePlayerStats();
    }
    public void addWeapon(GameObject weapon)
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
    public void updatePlayerStats()
    {
        Gun gunStats = currentGun.GetComponent<Gun>();// Get Gun stats script

        damageRed = 1 + gunStats.damage;
        fireRateRed = 1 + PlayerPrefs.GetFloat("fireRateRed");
        bulletSpeedRed = 1 + gunStats.bulletSpeed;
        bulletRangeRed = 1 + gunStats.bulletRange;
        accuracyRed = 1 + PlayerPrefs.GetFloat("accuracyRed");
        // Mod stats added to root object
        foreach (Transform mod in currentGun.transform.Find("Mods"))
        {
            GunModDisplay modScript = mod.gameObject.GetComponent<GunModDisplay>();

            damageRed += modScript.damage;
            fireRateRed += modScript.fireRate;
            bulletSpeedRed += modScript.bulletSpeed;
            bulletRangeRed += modScript.bulletRange;
            accuracyRed += modScript.accuracy;
        }

        damage = PlayerPrefs.GetFloat("damage") / damageRed;
        fireRate = gunStats.fireRate / fireRateRed;
        bulletSpeed = PlayerPrefs.GetFloat("bulletSpeed") / bulletSpeedRed;
        bulletRange = PlayerPrefs.GetFloat("bulletRange") / bulletRangeRed;
        accuracy = gunStats.accuracy / accuracyRed;


    }
}
