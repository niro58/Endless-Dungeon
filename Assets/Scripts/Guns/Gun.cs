using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public bool isActive = false;
    public enum GunName { M4, Glock, Mac_10, };
    [Header("Main")]
    public GunName gunName;

    public GunMod.FireMode fireMode = GunMod.FireMode.Single;
    public enum BulletType { Straight, ZigZag };
    [SerializeField]
    private BulletType bulletType;
    public GameObject bulletObj;


    [Header("Stats")]
    public float damageRed;
    public float fireRate;
    public float bulletSpeed;
    public float bulletRange;
    public float accuracy;

    [HideInInspector]
    public Sprite sprite;
    private Player playerStats;
    [HideInInspector]
    public Transform modsParent;
    private GameObject firePoint;
    private float shootCooldown;
    public void Start()
    {
        transform.name = gunName.ToString();
        sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
        modsParent = gameObject.transform.Find("Mods");
        playerStats = GlobalVar.player;
        firePoint = transform.Find("FirePoint").gameObject;
        
    }
    public void Update()
    {
        gameObject.SetActive(isActive);
        if(Input.GetKey(KeyCode.Mouse0) && Time.time >= shootCooldown)
        {
            shootCooldown = Time.time + playerStats.playerStats.fireRate;
            Shoot(fireMode);
        }

    }
    public void Shoot(GunMod.FireMode type)
    {
        switch (type)// Bullet creation, setting bullet script
        {
            case GunMod.FireMode.Single:
                StartCoroutine(SpawnBullet(1));
                break;
            case GunMod.FireMode.TripleShot:
                StartCoroutine(SpawnBullet(3, 0.1f));
                break;
        }
    }
    IEnumerator SpawnBullet(int number, float delay = 0)
    {
        for(int i = 0; i < number; i++)
        {
            getShootingData(out Vector3 pos, out Quaternion rotation);
            GameObject bullet = Instantiate(bulletObj, pos, rotation);

            yield return new WaitForSeconds(delay);
        }

    }
    public void getShootingData(out Vector3 pos, out Quaternion rotation)
    {
        pos = firePoint.transform.position;
        pos.z = 0.2f;
        float randRange = Random.Range(-playerStats.playerStats.accuracy, playerStats.playerStats.accuracy);
        float rotationZ = transform.parent.localEulerAngles.z + randRange;
        rotation = Quaternion.Euler(new Vector3(0, 0, rotationZ));
    }
}
