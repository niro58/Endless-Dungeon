using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
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
        InvokeRepeating("UpdateStats", 2.0f, 4);
    }
    void UpdateStats()
    {
        switch (name)
        {
            case "Health_Value":
                textUGUI.text = playerStats.health.ToString();
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
        }
    }
}
