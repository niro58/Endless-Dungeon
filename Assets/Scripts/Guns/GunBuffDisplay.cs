using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBuffDisplay : MonoBehaviour
{
    public List<GunMod> gunBuffs = new List<GunMod>();


    public float damageInc;
    public float fireRateRed;
    public float bulletSpeed;
    public float bulletRange;
    public float accuracyRed;
    void Start()
    {
        if (gunBuffs != null)
        {
            updateBuffs();
        }
    }

    public void updateBuffs()
    {
        damageInc = 0;
        fireRateRed = 0;
        bulletSpeed = 0;
        bulletRange = 0;
        accuracyRed = 0;
        foreach (GunMod buff in gunBuffs)
        {
            damageInc += buff.damageInc;
            fireRateRed += buff.fireRateRed;
            bulletSpeed += buff.bulletSpeed;
            bulletRange += buff.bulletRange;
            accuracyRed += buff.accuracyRed;
        }
    }
}
