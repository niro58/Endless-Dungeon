using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public int Health;
    public float Damage;
    public float Accuracy;
    public float Speed;
    public float FireRate;
    public float FireRateReduction;
    public float BulletSpeed;
    public float BulletRange;

    private Animator Anim;

    [HideInInspector]
    public Transform gunParent;
    [HideInInspector]
    public GameObject currentGun;
    [HideInInspector]
    public int currentGunSlot = 0;

    [HideInInspector]
    public GameObject player;
    private Dictionary<string,float> StartStats = new Dictionary<string, float>();

    [HideInInspector]
    public int coins;
    public void Awake()
    {
        gunParent = gameObject.transform.Find("Guns");
        currentGun = gunParent.GetChild(currentGunSlot).gameObject;

        StartStats["Health"] = Health;
        StartStats["Damage"] = Damage;
        StartStats["Speed"] = Speed;
        StartStats["FireRate"] = FireRate;
        StartStats["FireRateReduction"] = FireRateReduction;
        StartStats["BulletSpeed"] = BulletSpeed;
        StartStats["BulletRange"] = BulletRange;

        Anim = gameObject.GetComponent<Animator>();
        player = gameObject;

        updatePlayerStats();

        GlobalVar.playerStats = gameObject.GetComponent<PlayerStats>();


    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.layer == 8 || col.gameObject.layer == 9)
        {

            Anim.Play("Player-Hit");
            EnemyStats entityStats = col.gameObject.GetComponent<EnemyStats>();
            Health -= (int)Mathf.Ceil(entityStats.onCollisionDamage);
            StartCoroutine(onDamageImmunity(3, gameObject.GetComponent<PolygonCollider2D>()));
        }
    }
    IEnumerator onDamageImmunity(float time, Collider2D collider)
    {
        Physics2D.IgnoreLayerCollision(6, 8, true);
        Physics2D.IgnoreLayerCollision(6, 9, true);

        yield return new WaitForSeconds(time);

        Physics2D.IgnoreLayerCollision(6, 8, false);
        Physics2D.IgnoreLayerCollision(6, 9, false);

    }
    public void addMod(GunMod gunMod, Gun.GunName Gun)
    {
        /*Transform modsParent = currentWeapon.GetComponent<Gun>().modsParent;
        Debug.Log(card.modType.ToString());
        GunModDisplay gunModDisplay = modsParent.Find(card.modType.ToString()).gameObject.GetComponent<GunModDisplay>();
        gunModDisplay.gunMod = card;
        gunModDisplay.updateMod();
        updatePlayerStats();*/
    }
    public void addWeapon(GameObject weapon)
    {

    }
    public void updatePlayerStats()
    {
        Gun gunStats = currentGun.GetComponent<Gun>();// Get Gun stats script

        Damage = gunStats.damage + (int)StartStats["Damage"];
        FireRate = gunStats.fireRate + StartStats["FireRate"];
        FireRateReduction = 1 + StartStats["FireRateReduction"];// Get Gun Stats
        BulletSpeed = gunStats.bulletSpeed + StartStats["BulletSpeed"];
        BulletRange = gunStats.bulletRange + StartStats["BulletRange"];
        // Mod stats added to root object
        foreach (Transform mod in currentGun.transform.Find("Mods"))
        {
            GunModDisplay modScript = mod.gameObject.GetComponent<GunModDisplay>();

            FireRateReduction += modScript.fireRateReduction;
            Damage += modScript.damage;
            BulletSpeed += modScript.bulletSpeed;
            BulletRange += modScript.bulletRange;
        }

        Health = (int)StartStats["Health"];
        Speed = StartStats["Speed"];
        FireRate = FireRate / FireRateReduction;

        Transform UIStatsValueParent = GameObject.Find("UI").transform.Find("Stats").transform.Find("Values");
        foreach (Transform child in UIStatsValueParent)
        {
            switch (child.name)
            {
                case "Health_Value":
                    child.gameObject.GetComponent<TextMeshProUGUI>().text = Health.ToString();
                    break;
                case "Speed_Value":
                    child.gameObject.GetComponent<TextMeshProUGUI>().text = Speed.ToString();
                    break;
                case "Damage_Value":
                    child.gameObject.GetComponent<TextMeshProUGUI>().text = Damage.ToString();
                    break;
                case "Firerate_Value":
                    child.gameObject.GetComponent<TextMeshProUGUI>().text = FireRate.ToString();
                    break;
                case "BulletSpeed_Value":
                    child.gameObject.GetComponent<TextMeshProUGUI>().text = BulletSpeed.ToString();
                    break;
                case "BulletRange_Value":
                    child.gameObject.GetComponent<TextMeshProUGUI>().text = BulletRange.ToString();
                    break;

            }
        }

    }
}
