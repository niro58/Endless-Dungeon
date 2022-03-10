using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats
{
    public float health = 2;
    public float speed = 1;
    public float damage;
    public float accuracy;
    public float fireRate;
    public float bulletSpeed;
    public float bulletRange;

    public float damageInc;
    public float fireRateRed;
    public float accuracyRed;

    public int coins;

    private string fileName = "playerStats.dat";
    public PlayerStats()
    {
        if(LoadData() == false)
        {
            SaveData();
        }
    }
    public string ToJson(object obj)
    {
        return JsonUtility.ToJson(obj);
    }
    public void SaveData(string data = "")
    {
        if (data == "")
        {
            data = ToJson(this);
        }
        if(GlobalFunctions.SaveData(data, fileName))
        {
            Debug.Log("Save Succesfull");
        }
    }
    public void AddCoin()
    {
        if (GlobalFunctions.LoadData(fileName, out string output))
        {
            PlayerStats stats = JsonUtility.FromJson<PlayerStats>(output);
            stats.coins += GlobalVar.currentLevel + 1;
            SaveData(ToJson(stats));
        }
    }
    public bool LoadData()
    {
        if(GlobalFunctions.LoadData(fileName, out string output)){
            JsonUtility.FromJsonOverwrite(output, this);
        }
        else
        {
            return false;
        }
        return true;
    }
}
