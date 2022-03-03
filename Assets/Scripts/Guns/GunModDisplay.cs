using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunModDisplay : MonoBehaviour
{
    public GunMod gunMod;

    private Vector3 startPos;

    [HideInInspector]
    public float damageRed;
    [HideInInspector]
    public float fireRateRed;
    [HideInInspector]
    public float bulletSpeed;
    [HideInInspector]
    public float bulletRange;
    [HideInInspector]
    public float accuracy;
    void Start()
    {
        startPos = transform.localPosition;
        if (gunMod != null)
        {
            updateMod();
        }
    }
    
    public void updateMod()
    {
        Vector3 offset;
        Vector2 spriteSize = new Vector3((gunMod.sprite.rect.width / 100 / 2) * transform.localScale.x, (gunMod.sprite.rect.height / 100 / 2) * transform.localScale.y);
        switch (gunMod.modPart) // Mod movement based on what type it is
        {
            case GunMod.ModPart.Muzzle:
                offset = Vector2.Scale(new Vector2(1, 0), spriteSize);
                transform.localPosition = startPos + offset;
                break;
            case GunMod.ModPart.Mag:
                offset = Vector2.Scale(new Vector2(0, -1), spriteSize);
                transform.localPosition = startPos + offset;
                break;
            case GunMod.ModPart.Optic:
                offset = Vector2.Scale(new Vector2(1, 1), spriteSize);
                transform.localPosition = startPos + offset;
                break;
        }
        transform.GetComponent<SpriteRenderer>().sprite = gunMod.sprite;

        damageRed = gunMod.damageRed;
        fireRateRed = gunMod.fireRateRed;
        bulletSpeed = gunMod.bulletSpeed;
        bulletRange = gunMod.bulletRange;
        accuracy = gunMod.accuracy;
    }
}
