using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class WeaponCard : MonoBehaviour, IPointerClickHandler
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
        GameObject randGun = availableItems.guns[Random.Range(0, availableItems.guns.Count)];
        fillCard(randGun);
    }
    private void fillCard(GameObject gun)
    {
        Gun gunStats = gun.GetComponent<Gun>();
        Sprite sprite = gun.GetComponent<SpriteRenderer>().sprite;
        string itemName = null;
        string itemStats = null;

        
        foreach(FieldInfo field in gunStats.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance))
        {
            string name = field.Name;

            string value = field.GetValue(gunStats).ToString();
            switch (name)
            {
                case "gunType":
                    itemName = value;
                    break;
                default:
                    if(value != "null")
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
