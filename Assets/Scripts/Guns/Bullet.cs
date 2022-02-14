using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [HideInInspector]
    public Gun.BulletType bulletType;
    [HideInInspector]
    public float speed;
    [HideInInspector]
    public float damage;
    [HideInInspector]
    public float range;
    private float destroyTime;
    private Vector3 startPos;

    private Rigidbody2D rb;
    void Start()
    {
        damage = GlobalVar.playerStats.Damage;
        speed = GlobalVar.playerStats.BulletSpeed;
        range = GlobalVar.playerStats.BulletRange;
        startPos = transform.position;

        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        destroyTime += Time.deltaTime; 
        if(Vector2.Distance(startPos, transform.position) >= range)
        {
            Destroy(gameObject);
        }
        switch (bulletType)
        {
            case Gun.BulletType.Straight:

                rb.velocity = transform.right * speed;
                break;
        }

    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log(col.gameObject.name);
        if(col.gameObject.layer != 8 || col.gameObject.layer != 9)
        {
            Destroy(gameObject);
        }
    }
}
