using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class ModCard : MonoBehaviour, IPointerClickHandler
{
    private GameObject parent;
    private AvailableItems availableItems;
    public GunMod selectedMod;
    private void Start()
    {
        parent = gameObject.transform.parent.gameObject;
        availableItems = GlobalVar.importantGameObjects["Scripts"].GetComponent<AvailableItems>();
        generateCard();
    }
    private void OnEnable()
    {
        if(availableItems != null)
        {
            generateCard();
        }
    }
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (selectedMod.modPart != GunMod.ModPart.Buff)
        {
            availableItems.mods.Remove(selectedMod);
        }


        parent.gameObject.SetActive(false);
        GlobalVar.playerStats.AddMod(selectedMod);
    }
    private void generateCard()
    {
        selectedMod = availableItems.mods[Random.Range(0, availableItems.guns.Count)];
        fillCard(selectedMod);
    }
    private void fillCard(GunMod gunMod)
    {
        Sprite sprite = gunMod.sprite;
        string itemName = null;
        string itemStats = null;


        foreach (FieldInfo field in gunMod.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance))
        {
            string name = GlobalFunctions.SplitByCapitalLetters(field.Name).ToLower();
            string value = GlobalFunctions.SplitByCapitalLetters(field.GetValue(gunMod).ToString());
            switch (field.Name)
            {
                case "shortName":
                    itemName = value;
                    break;
                case "gunName":
                    itemStats += name + " - " + value + "\n";
                    break;
                case "sprite": case "modType":
                    break;
                default:
                    if (value.ToLower() != "none")
                    {
                        itemStats += name + " - " + value + "\n";
                    }
                    break;

            }
        }
        transform.Find("Image_Side").Find("Item_Image").GetComponent<Image>().sprite = sprite;
        transform.Find("Text_Side").Find("Item_Name").GetComponent<TextMeshProUGUI>().text = itemName;
        transform.Find("Text_Side").Find("Item_Stats").GetComponent<TextMeshProUGUI>().text = itemStats;
    }
}
