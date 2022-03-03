using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletPredictive : MonoBehaviour
{
    public float damage;
    public float bulletSpeed;
    public float distance;


    private Vector2 startPos;
    public Vector2 targetPos;

    private SpriteRenderer spriteRenderer;
    public void Start()
    {
        startPos = transform.position;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
    public void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPos, bulletSpeed * Time.deltaTime);
        if (Vector2.Distance(startPos, transform.position) >= distance)
        {
            distance = Mathf.Infinity;
            gameObject.GetComponent<Rigidbody2D>().velocity /= 25;
            LeanTween.alpha(gameObject, 0, 2);
        }
        if(spriteRenderer.color.a == 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        Destroy(gameObject);
        if (col.transform.tag == "Player")
        {
            GlobalVar.player.playerStats.health -= damage;
        }
    }
}
