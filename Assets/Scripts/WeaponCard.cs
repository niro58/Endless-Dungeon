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

    private GameObject selectedWeapon;
    private void Start()
    {
        parent = gameObject.transform.parent.gameObject;
        availableItems = GlobalVar.importantPrefabs["Scripts"].GetComponent<AvailableItems>();
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
        availableItems.guns.Remove(selectedWeapon);
        Time.timeScale = 1;
        if (availableItems.guns.Count == 0)
        {
            gameObject.GetComponent<ModCard>().enabled = true;
            gameObject.GetComponent<WeaponCard>().enabled = false;
        }

        parent.gameObject.SetActive(false);
        GlobalVar.player.AddWeapon(selectedWeapon);
    }
    private void generateCard()
    {
        selectedWeapon = availableItems.guns[Random.Range(0, availableItems.guns.Count)];
        
        fillCard(selectedWeapon);
    }
    private void fillCard(GameObject gun)
    {
        Gun gunStats = gun.GetComponent<Gun>();
        Sprite sprite = gun.GetComponent<SpriteRenderer>().sprite;
        string itemName = null;
        string itemStats = null;

        
        foreach(FieldInfo field in gunStats.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance))
        {
            string name = GlobalFunctions.SplitByCapitalLetters(field.Name);
            string value = field.GetValue(gunStats).ToString();

            switch (field.Name)
            {
                case "gunName":
                    itemName = value;
                    break;
                case "bulletObj":
                case "isActive":
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
