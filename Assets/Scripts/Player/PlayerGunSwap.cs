using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerGunSwap : MonoBehaviour
{
    private Player stats;
    private Transform gunParent;
    private void Start()
    {
        gunParent = GlobalVar.player.gunParent;
        stats = gameObject.GetComponent<Player>();
        swapPlayerGun(0);
    }
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            for(int slot = 0; slot < gunParent.childCount; slot++)
            {
                if (Input.GetKeyDown((slot + 1).ToString()) && slot != GlobalVar.player.currentGunSlot)
                {
                    swapPlayerGun(slot); // decrease by 1 because getchild starts from 0
                }
            }
        }
    }
    private void swapPlayerGun(int slot)
    {

        GameObject nextWeapon = gunParent.GetChild(slot).gameObject;
        if (nextWeapon.GetComponent<Gun>().isActive == false)
        {
            return;
        }
        GlobalVar.player.currentGun.SetActive(false);
        nextWeapon.SetActive(true);

        GlobalVar.player.currentGun = nextWeapon;
        GlobalVar.player.currentGunSlot = slot;
        stats.UpdatePlayerStats();
    }
}
