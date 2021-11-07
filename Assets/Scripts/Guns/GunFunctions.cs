using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GunFunctions : MonoBehaviour
{
    public static void swapPlayerGun(GameObject entity, int slot)
    {
        entity.transform.Find("Guns").GetChild(GlobalVar.currentPlayerGunSlot).gameObject.SetActive(false);// Disable current gun
        // Get entity Stats script
        PlayerStats playerStats = entity.GetComponent<PlayerStats>();

        GameObject gun = entity.transform.Find("Guns").GetChild(slot).gameObject;
        Gun gunStats = gun.GetComponent<Gun>();// Get Gun stats script

        float Damage = gunStats.Damage + playerStats.Damage;
        float FireRate = gunStats.FireRate;
        float FireRateReduction = 1 + playerStats.FireRateReduction;// Get Gun Stats
        float BulletSpeed = gunStats.BulletSpeed;
        float BulletRange = gunStats.BulletRange;
        // Mod stats added to root object
        foreach (Transform mod in gun.transform.Find("Mods"))
        {
            GunModDisplay modScript = mod.gameObject.GetComponent<GunModDisplay>();

            FireRateReduction += modScript.FireRateReduction;
            Damage += modScript.Damage;
            BulletSpeed += modScript.BulletSpeed;
            BulletRange += modScript.BulletRange;
        }

        GlobalVar.Health = playerStats.Health;
        GlobalVar.Speed = playerStats.Speed;
        GlobalVar.Damage = Damage;
        GlobalVar.FireRate = FireRate / FireRateReduction;
        GlobalVar.BulletSpeed = BulletSpeed;
        GlobalVar.BulletRange = BulletRange;

        Transform UIStatsValueParent = GameObject.Find("UI").transform.Find("Stats").transform.Find("Values");
        foreach (Transform child in UIStatsValueParent)
        {
            switch (child.name)
            {
                case "Health_Value":
                    child.gameObject.GetComponent<TextMeshProUGUI>().text = GlobalVar.Health.ToString();
                    break;
                case "Speed_Value":
                    child.gameObject.GetComponent<TextMeshProUGUI>().text = GlobalVar.Speed.ToString();
                    break;
                case "Damage_Value":
                    child.gameObject.GetComponent<TextMeshProUGUI>().text = GlobalVar.Damage.ToString();
                    break;
                case "Firerate_Value":
                    child.gameObject.GetComponent<TextMeshProUGUI>().text = GlobalVar.FireRate.ToString();
                    break;
                case "BulletSpeed_Value":
                    child.gameObject.GetComponent<TextMeshProUGUI>().text = GlobalVar.BulletSpeed.ToString();
                    break;
                case "BulletRange_Value":
                    child.gameObject.GetComponent<TextMeshProUGUI>().text = GlobalVar.BulletRange.ToString();
                    break;

            }
        }
        //Global Var
        gun.SetActive(true);
        GlobalVar.currentPlayerGunSlot = slot;
    }
}
