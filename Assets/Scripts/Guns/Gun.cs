using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private enum GunType { Pistol, Melee, Rifle, SMG, Sniper};
    [Header("Main")]
    [SerializeField]
    private GunType gunType;
    [SerializeField]
    private List<GunMod> availableModifications;

    private enum ShootingType { Classic }; // for shooting
    [Header("Shooting")]
    [SerializeField]
    private ShootingType shootingType;
    private GameObject firePoint;
    private float shootCooldown;

    
    public enum BulletType { Straight, ZigZag };
    [Header("Bullet")]
    [SerializeField]
    private BulletType bulletType;
    [SerializeField]
    private GameObject bulletPrefab;

    private PlayerStats playerStats;
    public void Start()
    {
        playerStats = GlobalVar.playerStats;
        firePoint = transform.Find("FirePoint").gameObject;
        
    }
    public void Update()
    {
        if(Input.GetKey(KeyCode.Mouse0) && Time.time >= shootCooldown)
        {
            shootCooldown = Time.time + playerStats.FireRate;

            switch (shootingType)// Bullet creation, setting bullet script
            {
                case ShootingType.Classic:
                    Vector3 pos = firePoint.transform.position;
                    pos.z = -0.8f;

                    GameObject bullet = Instantiate(bulletPrefab, pos, transform.rotation, transform.Find("bullets"));
                    break;
            }
        }

    }
}
