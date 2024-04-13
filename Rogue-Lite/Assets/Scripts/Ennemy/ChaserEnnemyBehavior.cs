using System;
using UnityEngine;

public class ChaserEnnemyBehavior : MonoBehaviour
{
    //SerializeField
    [SerializeField] private GameObject player;
    [SerializeField] private float speed = 0.5f;
    [SerializeField] private Detector detector;
    [SerializeField] private Sprite awaken;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private LootManager loot; 
    
    
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
            Destroy(other.gameObject);
        }
    }


    private void ChangeSprite()
    {
        if (detector.isSeeing)
        {
            spriteRenderer.sprite = awaken;
        }
    }
    
}
