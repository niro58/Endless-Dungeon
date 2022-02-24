using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIStats : MonoBehaviour
{
    PlayerStats playerStats;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GlobalVar.playerStats;
        UpdateStats();
        InvokeRepeating("UpdateStats", 2.0f, 4);
    }
    void UpdateStats()
    {
        switch (name)
        {
            case "Health_Value":
                gameObject.GetComponent<TextMeshProUGUI>().text = playerStats.health.ToString();
                break;
            case "Speed_Value":
                gameObject.GetComponent<TextMeshProUGUI>().text = playerStats.speed.ToString();
                break;
            case "Damage_Value":
                gameObject.GetComponent<TextMeshProUGUI>().text = playerStats.damage.ToString();
                break;
            case "Firerate_Value":
                gameObject.GetComponent<TextMeshProUGUI>().text = playerStats.fireRate.ToString();
                break;
            case "BulletSpeed_Value":
                gameObject.GetComponent<TextMeshProUGUI>().text = playerStats.bulletSpeed.ToString();
                break;
            case "BulletRange_Value":
                gameObject.GetComponent<TextMeshProUGUI>().text = playerStats.bulletRange.ToString();
                break;
            case "Accuracy_Value":
                gameObject.GetComponent<TextMeshProUGUI>().text = playerStats.accuracy.ToString();
                break;
        }
    }
}
