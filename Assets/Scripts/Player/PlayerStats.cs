using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public int Health;
    public int Damage;
    public float Speed;
    public float FireRate;
    public float FireRateReduction;
    public float BulletSpeed;
    public float BulletRange;

    private Animator Anim;

    private GameObject Player;
    private Dictionary<string,float> StartStats = new Dictionary<string, float>();
    public void Awake()
    {
        StartStats["Health"] = Health;
        StartStats["Damage"] = Damage;
        StartStats["Speed"] = Speed;
        StartStats["FireRate"] = FireRate;
        StartStats["FireRateReduction"] = FireRateReduction;
        StartStats["BulletSpeed"] = BulletSpeed;
        StartStats["BulletRange"] = BulletRange;

        Anim = gameObject.GetComponent<Animator>();
        Player = gameObject;

        updatePlayerStats();

        GlobalVar.Player = gameObject;
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
    public void updatePlayerStats()
    {
        GameObject gun = Player.transform.Find("Guns").GetChild(GlobalVar.CurrentPlayerGunSlot).gameObject;
        GunStats gunStats = gun.GetComponent<GunStats>();// Get Gun stats script

        Damage = gunStats.damage + (int)StartStats["Damage"];
        FireRate = gunStats.fireRate + StartStats["FireRate"];
        FireRateReduction = 1 + StartStats["FireRateReduction"];// Get Gun Stats
        BulletSpeed = gunStats.bulletSpeed + StartStats["BulletSpeed"];
        BulletRange = gunStats.bulletRange + StartStats["BulletRange"];
        // Mod stats added to root object
        foreach (Transform mod in gun.transform.Find("Mods"))
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
