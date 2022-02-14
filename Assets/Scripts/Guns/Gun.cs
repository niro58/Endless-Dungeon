using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public enum GunName { M4, Glock, Mac_10, };
    [Header("Main")]
    public GunName gunType;

    [HideInInspector]
    public GunMod.FireMode fireMode = GunMod.FireMode.Single;
    public enum BulletType { Straight, ZigZag };
    [SerializeField]
    private BulletType bulletType;
    public GameObject bulletObj;


    [Header("Stats")]
    public int damage;
    public float fireRate;
    public float bulletSpeed;
    public float bulletRange;
    public float accuracy;

    [HideInInspector]
    public Sprite sprite;
    private PlayerStats playerStats;
    [HideInInspector]
    public Transform modsParent;
    private GameObject firePoint;
    private float shootCooldown;
    public void Start()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
        modsParent = gameObject.transform.Find("Mods");
        playerStats = GlobalVar.playerStats;
        firePoint = transform.Find("FirePoint").gameObject;
        
    }
    public void Update()
    {
        if(Input.GetKey(KeyCode.Mouse0) && Time.time >= shootCooldown)
        {
            shootCooldown = Time.time + playerStats.FireRate;
            Shoot(fireMode);
        }

    }
    public void Shoot(GunMod.FireMode type)
    {
        switch (type)// Bullet creation, setting bullet script
        {
            case GunMod.FireMode.Single:
                Vector3 pos = firePoint.transform.position;
                pos.z = 0.2f;
                float randRange = Random.Range(-playerStats.Accuracy, playerStats.Accuracy);
                float rotationZ = transform.parent.localEulerAngles.z + randRange;
                Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, rotationZ));
                GameObject bullet = Instantiate(bulletObj, pos, rotation, transform.Find("bullets"));
                break;
        }
    }
}
