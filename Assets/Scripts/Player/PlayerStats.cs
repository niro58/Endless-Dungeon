using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int Health;
    public float Speed;
    public float Damage;
    public float FireRateReduction = 1;// Something like FireRate Cooldown Reduction/Increase

    private void Awake()
    {
        GlobalVar.player = gameObject;
    }
}
