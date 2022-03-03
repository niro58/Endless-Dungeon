using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats
{
    public float health = 2;
    public float speed;
    public float damage;
    public float accuracy;
    public float fireRate;
    public float bulletSpeed;
    public float bulletRange;

    public float damageIncrease;
    public float fireRateIncrease;
    public float accuracyReduction;

    public int coins;

    private string fileName = "playerStats.dat";
    public PlayerStats()
    {
        LoadData();
    }
    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }
    public void SaveData()
    {
        if(GlobalFunctions.SaveData(ToJson(), fileName))
        {
            Debug.Log("Save Succesfull");
        }
    }
    public void LoadData()
    {
        if(GlobalFunctions.LoadData(fileName, out string output)){
            JsonUtility.FromJsonOverwrite(output, this);
        }
    }
}
