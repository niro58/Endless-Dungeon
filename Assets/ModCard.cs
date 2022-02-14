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
    private void Start()
    {
        parent = gameObject.transform.parent.gameObject;
        availableItems = parent.GetComponent<AvailableItems>();
        generateCard();
    }
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        parent.gameObject.SetActive(false);
    }
    private void generateCard()
    {
        GunMod randMod = availableItems.mods[Random.Range(0, availableItems.guns.Count)];
        fillCard(randMod);
    }
    private void fillCard(GunMod gunMod)
    {
        Sprite sprite = gunMod.sprite;
        string itemName = null;
        string itemStats = null;


        foreach (FieldInfo field in gunMod.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance))
        {
            string name = field.Name;
            string value = field.GetValue(gunMod).ToString();
            switch (name)
            {
                case "shortName":
                    itemName = value;
                    break;
                case "gunName":
                    itemStats += name + " - " + value + "\n";
                    break;
                default:
                    if (float.TryParse(value, out float num) && num != 0)
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
