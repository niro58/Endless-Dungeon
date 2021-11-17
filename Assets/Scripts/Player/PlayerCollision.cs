using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;
    private Animator anim;
    private float hitInvulnerability;
    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
      
    }
    private void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "PlayerHit")
        {
            anim.Play("Player-Hit");
            EntityStats entityStats = col.gameObject.GetComponent<EntityStats>();
            GlobalVar.sumStats.Health -= entityStats.onCollisionDamage;
        }
        if (GlobalVar.sumStats.Health == 0)
        {
            Destroy(gameObject);
        }
    }
}
