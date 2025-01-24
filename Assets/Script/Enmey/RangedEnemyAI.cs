using UnityEngine;
using System.Collections;
using System;

public class RangedEnemyAI : MonoBehaviour
{
    [Header("Enemy Settings")]
    public float moveSpeed = 2f;
    public float chaseSpeedMultiplier = 1.2f;
    public float detectionRange = 7f;
    public float attackRange = 4f; // Shoots from a distance
    public float obstacleCheckDistance = 0.5f;
    public LayerMask playerLayer;
    public LayerMask obstacleLayer;

    int health = 100;
    [Header("Shooting Settings")]
    public GameObject projectilePrefab; // Assign projectile in Inspector
    public Transform shootPoint; // Assign shoot point from where projectiles spawn
    public float fireRate = 1.5f; // Time between shots
    public float projectileSpeed = 5f;

    [Header("References")]
    public Transform frontCheck;
    private Transform player;
    private Rigidbody2D rb;
    private bool movingRight = true;
    private bool isShooting = false;
    public Action<float> onDamageDone;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        if (player == null) return;
        if (player.GetComponent<PlayerMovement>().isDead) return;
        float distanceToPlayer = Mathf.Abs(transform.position.x - player.position.x);

        if (distanceToPlayer <= attackRange && !isShooting)
        {
            float directionX = Mathf.Sign(player.position.x - transform.position.x); // Only moves in X direction
            if ((directionX > 0 && !movingRight) || (directionX < 0 && movingRight))
            {
                Flip();
            }
            StartCoroutine(Shoot());
        }
        else
        {
            if(distanceToPlayer>attackRange)
                ChasePlayer();
        }
    }

    void ChasePlayer()
    {
        if (player == null) return;

        float directionX = Mathf.Sign(player.position.x - transform.position.x); // Only moves in X direction

        rb.linearVelocity = new Vector2(directionX * moveSpeed * chaseSpeedMultiplier, rb.linearVelocity.y);

        if ((directionX > 0 && !movingRight) || (directionX < 0 && movingRight))
        {
            Flip();
        }

        RaycastHit2D obstacleInfo = Physics2D.Raycast(frontCheck.position, movingRight ? Vector2.right : Vector2.left, obstacleCheckDistance, obstacleLayer);
        if (obstacleInfo.collider != null)
        {
            Flip();
        }
    }

    IEnumerator Shoot()
    {
        isShooting = true;
        rb.linearVelocity = Vector2.zero; 
        yield return new WaitForSeconds(0.5f); 

        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
        Rigidbody2D projRb = projectile.GetComponent<Rigidbody2D>();

        float direction = movingRight ? 1f : -1f;
        projRb.linearVelocity = new Vector2(direction * projectileSpeed, 0); // Shoots only in X direction

        yield return new WaitForSeconds(fireRate); // Wait before next shot
        isShooting = false;
    }

    void Flip()
    {
        movingRight = !movingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
    public bool Damage()
    {
        health -= 10;
        onDamageDone?.Invoke((float)health/ (float)100);
        if (health <= 0)
        {
            return true;
        }
        return false;
    }
}
