using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPool : MonoBehaviour
{
    public enum ItemType { Gun_Modification, Gun};
    public ItemType itemType;

    public void Start()
    {
        switch (itemType)
        {
            case ItemType.Gun_Modification:
                break;
            case ItemType.Gun:
                break;
        }
    }
}
