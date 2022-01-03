using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerGunSwap : MonoBehaviour
{
    private PlayerStats stats;
    private GameObject gunParent;
    private void Start()
    {
        gunParent = gameObject.transform.Find("Guns").gameObject;
        stats = gameObject.GetComponent<PlayerStats>();
        swapPlayerGun(0);
    }
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            for(int i = 1; i <= gunParent.transform.childCount; i++)
            {
                int slot = i - 1;
                if (Input.GetKeyDown(i.ToString()) && slot != GlobalVar.CurrentPlayerGunSlot)
                {
                    swapPlayerGun(slot); // decrease by 1 because getchild starts from 0
                }
            }
        }
    }
    private void swapPlayerGun(int slot)
    {
        int currSlot = GlobalVar.CurrentPlayerGunSlot;
        gunParent.transform.GetChild(currSlot).gameObject.SetActive(false);
        gunParent.transform.GetChild(slot).gameObject.SetActive(true);

        GlobalVar.CurrentPlayerGunSlot = slot;
        stats.updatePlayerStats();
    }
}
