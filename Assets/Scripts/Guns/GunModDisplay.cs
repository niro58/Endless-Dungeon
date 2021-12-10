using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunModDisplay : MonoBehaviour
{
    [SerializeField]
    private GunMod gunMod;

    private Vector3 startPos;

    [HideInInspector]
    public int damage;
    [HideInInspector]
    public float fireRateReduction;// Something like FireRate Cooldown Reduction/Increase
    [HideInInspector]
    public float bulletSpeed;
    [HideInInspector]
    public float bulletRange;
    void Start()
    {
        if (gunMod != null)
        {
            Vector3 offset;
            startPos = transform.localPosition;
            Vector2 spriteSize = new Vector3((gunMod.sprite.rect.width / 100 / 2) * transform.localScale.x, (gunMod.sprite.rect.height / 100 / 2) * transform.localScale.y);
            switch (gunMod.modType) // Mod movement based on what type it is
            {
                case GunMod.ModType.Muzzle:
                    offset = Vector2.Scale(new Vector2(1, 0), spriteSize);
                    transform.localPosition = startPos + offset;
                    break;
                case GunMod.ModType.Mag:
                    offset = Vector2.Scale(new Vector2(0, -1), spriteSize);
                    transform.localPosition = startPos + offset;
                    break;
                case GunMod.ModType.Scope:
                    offset = Vector2.Scale(new Vector2(1, 1), spriteSize);
                    transform.localPosition = startPos + offset;
                    break;
            }
            transform.GetComponent<SpriteRenderer>().sprite = gunMod.sprite;

            damage = gunMod.damage;
            fireRateReduction = gunMod.fireRateReduction;
            bulletSpeed = gunMod.bulletSpeed;
            bulletRange = gunMod.bulletRange;
        }
    }
}
