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
    public List<GameObject> availableItems = new List<GameObject>();
    private GameObject selectedWeapon;

    private bool generated;
    private void Start()
    {
        parent = gameObject.transform.parent.gameObject;
        availableItems = GlobalVar.importantPrefabs["Scripts"].GetComponent<AvailableItems>().guns;
        GenerateCard();
    }
    private void OnEnable()
    {
        if(availableItems.Count > 0)
        {
            GenerateCard();
        }
    }
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        availableItems.Remove(selectedWeapon);
        if (availableItems.Count == 0)
        {
            gameObject.GetComponent<ModCard>().enabled = true;
            gameObject.GetComponent<WeaponCard>().enabled = false;
        }

        parent.gameObject.SetActive(false);
        GlobalVar.player.AddWeapon(selectedWeapon);
    }
    private void GenerateCard()
    {
        selectedWeapon = availableItems[Random.Range(0, availableItems.Count)];

        FillCard(selectedWeapon);
        
    }
    private void FillCard(GameObject gun)
    {
        Gun gunStats = gun.GetComponent<Gun>();
        Sprite sprite = gun.GetComponent<SpriteRenderer>().sprite;
        string itemName = null;
        string itemStats = null;

        itemName = gunStats.name;
        itemStats += "Fire Mode" + " : " + gunStats.fireMode.ToString() + "\n";
        itemStats += "Damage" + " : " + gunStats.damage + "\n";
        itemStats += "Damage Increase %" + " : " + gunStats.damageInc + "\n";
        itemStats += "FireRate" + " : " + gunStats.fireRate + "\n";
        itemStats += "Accuracy" + " : " + gunStats.accuracy + "\n";
        itemStats += "Bullet Speed" + " : " + gunStats.bulletSpeed + "\n";
        itemStats += "Bullet Range" + " : " + gunStats.bulletRange + "\n";

        transform.Find("Image_Side").Find("Item_Image").GetComponent<Image>().sprite = sprite;
        transform.Find("Text_Side").Find("Item_Name").GetComponent<TextMeshProUGUI>().text = itemName;
        transform.Find("Text_Side").Find("Item_Stats").GetComponent<TextMeshProUGUI>().text = itemStats;
    }
}
