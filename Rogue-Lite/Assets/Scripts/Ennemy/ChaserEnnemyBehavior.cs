using System;
using UnityEngine;

public class ChaserEnnemyBehavior : MonoBehaviour
{
    //private
    private float _timer;
    private bool _isHit;
    
    //SerializeField
    [SerializeField] private float speed = 0.5f;
    [SerializeField] private GameObject player;
    [SerializeField] private Detector detector;
    [SerializeField] private LootManager loot; 
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite awaken;
    [SerializeField] private Sprite asleep;
    [SerializeField] private Sprite hitSprite;
    
    
    //private
    private int _hp = 3;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
    }

    private void Update()
    {
        //if enemy is seeing player then chase
        if (detector.isSeeing)
        {
            FollowPlayer();
        }

        if (_hp <= 0)
        {
            if (loot != null)
            {
                loot.GenerateLoot(transform.position);
            }
            Destroy(this.gameObject);
        }
        
        ChangeSprite();
    }

    private void FollowPlayer()
    {
        Vector3 playerPosition = player.transform.position;
        Vector3 path = playerPosition - transform.position;
        
        transform.Translate(path * (speed * Time.deltaTime), Space.World);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("RegularBullet"))
        {
            _hp--;
            _isHit = true;
            Destroy(other.gameObject);
        }
    }


    private void ChangeSprite()
    {
        if (detector.isSeeing)
        {
            spriteRenderer.sprite = awaken;
        }

        if (_isHit)
        {
            spriteRenderer.sprite = hitSprite;
            _timer += Time.deltaTime;
        }

        if (_timer >= 0.2f)
        {
            spriteRenderer.sprite = asleep;
            _timer = 0;
            _isHit = false;
        }
    }
    
}
