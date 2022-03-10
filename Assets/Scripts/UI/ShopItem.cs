using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;
using TMPro;
using UnityEngine.UI;
public class ShopItem : MonoBehaviour
{
    private PlayerStats playerStats;
    private FieldInfo chosenAttributeField;
    public enum Attribute {health,speed,damage,bulletSpeed,bulletRange,damageInc,fireRateRed,accuracyRed};
    public Attribute attribute;

    public float startValue;
    private float attributeValue;
    public float attributeIncreaseOnUpgrade;
    public int priceIncreaseOnUpgrade;
    
    public int maxValue;
    private int currentPrice;

    private TextMeshProUGUI uiAttributeValue;
    private TextMeshProUGUI uiUpgradePrice;
    private Button uiAttributeUpgradeButton;
    // Start is called before the first frame update
    void Start()
    {
        playerStats = GlobalVar.playerStats;

        Type playerStatsType = typeof(PlayerStats);
        chosenAttributeField = playerStatsType.GetField(attribute.ToString());
        attributeValue = (float)chosenAttributeField.GetValue(playerStats);
        currentPrice = priceIncreaseOnUpgrade + priceIncreaseOnUpgrade * (int)Mathf.Ceil((attributeValue - startValue) / attributeIncreaseOnUpgrade);

        uiAttributeValue = transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
        uiUpgradePrice = transform.GetChild(2).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        uiAttributeUpgradeButton = transform.GetChild(2).gameObject.GetComponent<Button>();

        UpdateItemText();
    }
    public void UpgradeAttribute()
    {
        attributeValue += attributeIncreaseOnUpgrade;
        attributeValue = GlobalFunctions.RoundByTwoDecimals(attributeValue);
        playerStats.coins -= currentPrice;
        currentPrice += priceIncreaseOnUpgrade;
        chosenAttributeField.SetValue(playerStats, attributeValue);
        playerStats.SaveData();

        UpdateItemText();
    }
    public void UpdateItemText()
    {
        uiAttributeValue.text = attributeValue.ToString();
        switch (attribute)
        {
            case Attribute.damageInc:
            case Attribute.fireRateRed:
            case Attribute.accuracyRed:
                uiAttributeValue.text += "%";
                break;
        }
        uiUpgradePrice.text = currentPrice.ToString();
        
        foreach(Transform item in transform.parent)
        {
            Button button = item.GetChild(2).gameObject.GetComponent<Button>();
            if (playerStats.coins < item.GetComponent<ShopItem>().currentPrice && attributeValue <= maxValue)
            {
                button.interactable = false;
            }
        }
        transform.parent.parent.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Coins : " + playerStats.coins.ToString();
    }
}
