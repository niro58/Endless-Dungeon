using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerGunSwap : MonoBehaviour
{
    private void Start()
    {
        swapPlayerGun(0);
    }
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            for(int i = 1; i <= gameObject.transform.GetChild(0).childCount; i++)
            {
                if (Input.GetKeyDown(i.ToString()))
                {
                    swapPlayerGun(i - 1); // decrease by 1 because getchild starts from 0
                }
            }
        }
    }
    private void swapPlayerGun(int slot)
    {
        
        GlobalVar.CurrentPlayerGunSlot = slot;
    }
}
