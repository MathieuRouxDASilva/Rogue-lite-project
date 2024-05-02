using UnityEngine;
using UnityEngine.Serialization;


public class ShooterEnnemyBehavior : MonoBehaviour
{
    //SerializeField
    [SerializeField] private float fireRate;
    [SerializeField] private float ennemyBulletVelocity;
    [SerializeField] private int bulletPerShot;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform barrelPosition;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LootManager loot; 
    [SerializeField] private SpriteRenderer spritendereRenderer;
    [SerializeField] private Sprite hit;
    [SerializeField] private Sprite normal;

    //private
    private float _fireRateTimer;
    private float _timer;
    private float _hitTimer;
    private int _hp = 2;
    private bool _rotated;
    private bool _isHit;
    private Vector2 _aimPosition;

    private void Start()
    {
        _fireRateTimer = fireRate;
        player = GameObject.FindGameObjectWithTag("player");
    }

    private void Update()
    {
        if (ShouldFire())
        {
            ShootAtPlayer();
        }

        if (_hp <= 0)
        {
            if (loot != null)
            {
                loot.GenerateLoot(transform.position);
            }
            Destroy(this.gameObject);
        }
        
        Aiming();
        
        ChangeSprite();
    }


    private bool ShouldFire()
    {
        _fireRateTimer += Time.deltaTime;

        if (_fireRateTimer < fireRate)
        {
            return false;
        }

        return true;
    }

    private void ShootAtPlayer()
    {
        _fireRateTimer = 0;
        barrelPosition.LookAt(player.transform.position);
        for (int nbOfShoot = 0; nbOfShoot < bulletPerShot; nbOfShoot++)
        {
            GameObject actualBullet = Instantiate(bullet, barrelPosition.position,
                new Quaternion(barrelPosition.rotation.x, barrelPosition.rotation.y + 90f, barrelPosition.rotation.z,
                    barrelPosition.rotation.w));
            Rigidbody2D rbe = actualBullet.GetComponent<Rigidbody2D>();
            rbe.AddForce(barrelPosition.forward * (ennemyBulletVelocity * 5), ForceMode2D.Impulse);
        }
    }
    
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("RegularBullet"))
        {
            Destroy(other.gameObject);
            rb.velocity = new Vector2(0, 0);
            _hp--;
            _isHit = true;
        }
        if (other.gameObject.CompareTag("player"))
        {
            rb.velocity = new Vector2(0, 0);
        }
        if (other.gameObject.CompareTag("ChaserEnnemy"))
        {
            rb.velocity = new Vector2(0, 0);
        }
        if (other.gameObject.CompareTag("PatternEnnemy"))
        {
            rb.velocity = new Vector2(0, 0);
        }
        if (other.gameObject.CompareTag("EnnemyBullet"))
        {
            Destroy(other.gameObject);
            rb.velocity = new Vector2(0, 0);
        }
    }

    private void ChangeSprite()
    {
        if (_isHit)
        {
            _hitTimer += Time.deltaTime;
            spritendereRenderer.sprite = hit;
        }

        if (_hitTimer >= 0.2f)
        {
            _hitTimer = 0;
            _isHit = false;
            spritendereRenderer.sprite = normal;
        }
    }
    
    private void Aiming()
    {
        _aimPosition = player.transform.position - transform.position;
        
        if (!_rotated)
        {
            spritendereRenderer.flipX = true;
            _rotated = true;
        }
        
        float angle = Mathf.Atan2(_aimPosition.y, _aimPosition.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
    }
}