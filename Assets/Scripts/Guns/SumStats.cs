using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SumStats : MonoBehaviour
{
    public int Health;
    public int Damage;
    public float Speed;
    public float FireRate;
    public float FireRateReduction;
    public float BulletSpeed;
    public float BulletRange;

    private Animator anim;

    private GameObject player;
    private Dictionary<string,float> startStats = new Dictionary<string, float>();
    public void Awake()
    {
        GlobalVar.Player = gameObject;
        startStats["Health"] = Health;
        startStats["Damage"] = Damage;
        startStats["Speed"] = Speed;
        startStats["FireRate"] = FireRate;
        startStats["FireRateReduction"] = FireRateReduction;
        startStats["BulletSpeed"] = BulletSpeed;
        startStats["BulletRange"] = BulletRange;

        anim = gameObject.GetComponent<Animator>();
        player = gameObject;

        updatePlayerStats();
        GlobalVar.sumStats = gameObject.GetComponent<SumStats>();


    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.layer == 8 || col.gameObject.layer == 9)
        {
            Health -= col.gameObject.GetComponent<EntityStats>().onCollisionDamage;

            anim.Play("Player-Hit");
            EntityStats entityStats = col.gameObject.GetComponent<EntityStats>();
            GlobalVar.sumStats.Health -= entityStats.onCollisionDamage;
        }
    }
    public void updatePlayerStats()
    {
        GameObject gun = player.transform.Find("Guns").GetChild(GlobalVar.currentPlayerGunSlot).gameObject;
        GunStats gunStats = gun.GetComponent<GunStats>();// Get Gun stats script

        Damage = gunStats.Damage + (int)startStats["Damage"];
        FireRate = gunStats.FireRate + startStats["FireRate"];
        FireRateReduction = 1 + startStats["FireRateReduction"];// Get Gun Stats
        BulletSpeed = gunStats.BulletSpeed + startStats["BulletSpeed"];
        BulletRange = gunStats.BulletRange + startStats["BulletRange"];
        // Mod stats added to root object
        foreach (Transform mod in gun.transform.Find("Mods"))
        {
            GunModDisplay modScript = mod.gameObject.GetComponent<GunModDisplay>();

            FireRateReduction += modScript.FireRateReduction;
            Damage += modScript.Damage;
            BulletSpeed += modScript.BulletSpeed;
            BulletRange += modScript.BulletRange;
        }

        Health = (int)startStats["Health"];
        Speed = startStats["Speed"];
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
