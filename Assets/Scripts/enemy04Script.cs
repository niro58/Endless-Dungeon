using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy04Script : MonoBehaviour
{
    public EnemyGrid roomGrid;
    public Dictionary<Vector2Int, string> roomMapFreeCells;
    
    public Vector2Int direction;

    private Rigidbody2D targetRb;
    private bool predictiveShooting;
    private GameObject target;
    [SerializeField]
    private GameObject bullet;

    private EnemyStats stats;
    // Start is called before the first frame update
    void Start()
    {
        stats = gameObject.GetComponent<EnemyStats>();
        target = GlobalVar.player.player;
        targetRb = target.GetComponent<Rigidbody2D>();

        predictiveShooting = GlobalFunctions.GetRandBoolean(); 
    }

    // Update is called once per frame
    void Update()
    {
        if(roomGrid == null && GlobalVar.roomMap != null && GlobalVar.roomMap.filledCells.Count > 0)
        {
            roomGrid = GlobalVar.roomMap;
            roomMapFreeCells = GlobalVar.roomMap.GetFreeCells();
            transform.position = roomGrid.WorldToCellWorld(transform.position);
            InvokeRepeating("Shoot", 2, stats.fireRate);

        }
        if(roomGrid != null)
        {
            //Movement part
            if (direction != Vector2.zero)
            {
                Vector3 offset = Vector2.Scale(new Vector2(0.32f, 0.32f), direction);
                Vector2Int currCell = roomGrid.WorldToCell(transform.position + offset);
                Vector2Int nextCell = currCell - (direction);
                if (!roomGrid.IsAvailable(nextCell))
                {
                    direction *= -1;
                }
                transform.position = Vector2.MoveTowards(transform.position, roomGrid.CellToWorld(nextCell), stats.speed * Time.deltaTime);
            }
         
        }
        
    }
    void Shoot()
    {
        if (Physics2D.Linecast(transform.position, target.transform.position, 1 << LayerMask.NameToLayer("Obstacle")) == false)
        {
            GameObject createdBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            EnemyBulletPredictive bulletScript = createdBullet.GetComponent<EnemyBulletPredictive>();
            bulletScript.distance = stats.bulletRange;
            if (predictiveShooting)
            {
                bulletScript.targetPos = (new Vector2(target.transform.position.x, target.transform.position.y) + targetRb.velocity) * Random.Range(0.65f, 1);
            }
            else
            {
                bulletScript.targetPos = target.transform.position;
            }
            bulletScript.damage = stats.damage;
            bulletScript.bulletSpeed = stats.bulletSpeed;
            /*if (MyMath.calculateDirection(target.transform.position, transform.position, targetRb.velocity, stats.bulletSpeed, out var direction))
            {
                createdBullet.GetComponent<Rigidbody2D>().velocity = direction * stats.bulletSpeed;
            }
            else
            {
                createdBullet.GetComponent<Rigidbody2D>().velocity = (target.transform.position - transform.position).normalized * stats.bulletSpeed;
            }*/
        }
    }
}
