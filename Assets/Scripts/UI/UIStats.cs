using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class UIStats : MonoBehaviour
{
    PlayerStats playerStats;
    TextMeshProUGUI textUGUI;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GlobalVar.player.playerStats;
        textUGUI = gameObject.GetComponent<TextMeshProUGUI>();
        UpdateStats();
        InvokeRepeating("UpdateStats", 0.5f, 0.2f);
    }
    void UpdateStats()
    {
        switch (name)
        {
            case "Health_Value":
                textUGUI.text = GlobalVar.player.health.ToString();
                break;
            case "Speed_Value":
                textUGUI.text = playerStats.speed.ToString();
                break;
            case "Damage_Value":
                textUGUI.text = playerStats.damage.ToString();
                break;
            case "Firerate_Value":
                textUGUI.text = playerStats.fireRate.ToString();
                break;
            case "BulletSpeed_Value":
                textUGUI.text = playerStats.bulletSpeed.ToString();
                break;
            case "BulletRange_Value":
                textUGUI.text = playerStats.bulletRange.ToString();
                break;
            case "Accuracy_Value":
                textUGUI.text = playerStats.accuracy.ToString();
                break;
            case "Coin_Value":
                textUGUI.text = playerStats.coins.ToString();
                break;
            case "Level_Value":
                textUGUI.text = GlobalVar.currentLevel.ToString();
                break;
            case "Test_Value":
                textUGUI.text = "Damage Increase : " + GlobalFunctions.RoundByTwoDecimals(playerStats.damageInc).ToString() + Environment.NewLine;
                textUGUI.text += "Fire Rate Reduction : " + GlobalFunctions.RoundByTwoDecimals(playerStats.fireRateRed).ToString() + Environment.NewLine;
                textUGUI.text += "Accuracy Reduction : " + GlobalFunctions.RoundByTwoDecimals(playerStats.accuracyRed).ToString() + Environment.NewLine;
                textUGUI.text += "Current Level : " + GlobalVar.currentLevel.ToString() + Environment.NewLine;
                break;
            case "FPSManager":
                textUGUI.text = GlobalFunctions.RoundByTwoDecimals(1 / Time.unscaledDeltaTime).ToString();
                break;
        }
    }
}
