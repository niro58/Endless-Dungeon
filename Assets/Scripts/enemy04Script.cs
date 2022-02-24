using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy04Script : MonoBehaviour
{
    public EnemyGrid roomMap;
    public Dictionary<Vector2Int, string> roomMapFreeCells;
    
    public Vector2Int direction;

    private Rigidbody2D targetRb;
    private Rigidbody2D projectileRb;

    private GameObject target;
    [SerializeField]
    private GameObject bullet;

    private EnemyStats stats;
    private int mode = -1;
    // Start is called before the first frame update
    void Start()
    {
        stats = gameObject.GetComponent<EnemyStats>();
        target = GlobalVar.playerStats.player;
        targetRb = target.GetComponent<Rigidbody2D>();
        projectileRb = bullet.GetComponent<Rigidbody2D>();

        direction.x = Random.Range(-1, 1);
        direction.y = Random.Range(-1, 1);
    }

    // Update is called once per frame
    void Update()
    {

        if(roomMap == null && GlobalVar.roomMap != null && GlobalVar.roomMap.filledCells.Count > 0)
        {
            roomMap = GlobalVar.roomMap;
            roomMapFreeCells = GlobalVar.roomMap.getFreeCells();
            transform.position = roomMap.WorldToCellWorld(transform.position);
            InvokeRepeating("Shoot", 2, stats.fireRate);
            /*
            GameObject checker = GameObject.Find("Not Important").transform.Find("Square").gameObject;
            foreach(var item in roomMap.filledCells)
            {
                GameObject checkerPrefab = Instantiate(checker, roomMap.CellToWorld(item.Key), Quaternion.identity, GameObject.Find("Not Important").transform);
                checkerPrefab.name = item.Key.ToString();
                if(item.Value == null)
                {
                    checkerPrefab.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                }
                else
                {
                    checkerPrefab.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                }
            }*/

        }
        if(roomMap != null)
        {
            //Movement part
            if (direction != Vector2.zero)
            {
                Vector2Int currCell = roomMap.WorldToCell(transform.position);
                Vector2Int nextCell = currCell - (direction * mode);
                if (!roomMap.IsAvailable(nextCell))
                {
                    mode *= -1;
                }
                transform.position = Vector2.MoveTowards(transform.position, roomMap.CellToWorld(nextCell), stats.speed * Time.deltaTime);
            }
         
        }
        
    }
    void Shoot()
    {
        if (Physics2D.Linecast(transform.position, target.transform.position, 1 << LayerMask.NameToLayer("Obstacle")) == false)
        {
            GameObject createdBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            Enemy_Bullet bulletScript = createdBullet.GetComponent<Enemy_Bullet>();
            bulletScript.distance = stats.bulletRange;
            bulletScript.damage = stats.damage;
            if (MyMath.calculateDirection(target.transform.position, transform.position, targetRb.velocity, stats.bulletSpeed, out var direction))
            {
                createdBullet.GetComponent<Rigidbody2D>().velocity = direction * stats.bulletSpeed;
            }
            else
            {
                createdBullet.GetComponent<Rigidbody2D>().velocity = (target.transform.position - transform.position).normalized * stats.bulletSpeed;
            }
        }
    }
}
