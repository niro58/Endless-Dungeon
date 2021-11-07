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
    public float Damage;
    public float FireRate;
    public float BulletSpeed;
    public float BulletRange;

    private enum ShootingType { Classic }; // for shooting
    [Header("Shooting")]
    [SerializeField]
    private ShootingType shootingType;
    private GameObject firePoint;
    private float shootCooldown;

    
    public enum bulletType { Straight, ZigZag };
    [Header("Bullet")]
    [SerializeField]
    private bulletType BulletType;
    [SerializeField]
    private GameObject bulletPrefab;


    public void Start()
    {
        firePoint = transform.Find("FirePoint").gameObject;
        
    }
    public void Update()
    {
        if(Input.GetKey(KeyCode.Mouse0) && Time.time >= shootCooldown)
        {
            shootCooldown = Time.time + GlobalVar.FireRate;

            switch (shootingType)// Bullet creation, setting bullet script
            {
                case ShootingType.Classic:
                    Vector3 pos = firePoint.transform.position;
                    pos.z = -0.8f;

                    GameObject bullet = Instantiate(bulletPrefab, pos, transform.rotation, transform.Find("bullets"));
                    Bullet bulletScript = bullet.GetComponent<Bullet>();
                    bulletScript.BulletType = BulletType; // Setting the movement
                    bulletScript.Speed = GlobalVar.BulletSpeed;
                    break;
            }
        }

    }
    
    private void OnValidate()
    {
        if (BulletSpeed < 0.001f)
        {
            BulletSpeed = 0.001f;
        }
        if (BulletRange < 0.001f)
        {
            BulletRange = 0.001f;
        }
        if (FireRate < 0.001f)
        {
            FireRate = 0.001f;
        }
    }
}
