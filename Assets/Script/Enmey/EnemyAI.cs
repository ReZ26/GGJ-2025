using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    [Header("Enemy Settings")]
    public float moveSpeed = 2f;
    public float chaseSpeedMultiplier = 1.5f;
    public float detectionRange = 5f;
    public float attackRange = 1f;
    public float patrolEdgeCheckDistance = 1f; // Distance to check for edges
    public float obstacleCheckDistance = 0.5f; // Distance to check for obstacles
    public LayerMask groundLayer;
    public LayerMask playerLayer;
    public LayerMask obstacleLayer; // For detecting obstacles

    [Header("References")]
    public Transform groundCheck;
    public Transform frontCheck; 
    private Transform player;
    private Rigidbody2D rb;
    public float attackDelay = 3f;
    private bool movingRight = false;
    private bool chasingPlayer = true;
    private bool isAttacking = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        if (player == null) return;
        if (player.GetComponent<PlayerMovement>().isDead) return;
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange && !isAttacking)
        {
            StartCoroutine(Attack());
        }
            ChasePlayer();
       
    }

    void Patrol()
    {
        rb.linearVelocity = new Vector2(movingRight ? moveSpeed : -moveSpeed, rb.linearVelocity.y);

        RaycastHit2D obstacleInfo = Physics2D.Raycast(frontCheck.position, movingRight ? Vector2.right : Vector2.left, obstacleCheckDistance, obstacleLayer);

        if (obstacleInfo.collider != null && !obstacleInfo.collider.CompareTag("Player"))
        {
            Flip();
        }
    }

    void ChasePlayer()
    {
        if (player == null) return;

        Vector2 direction = (player.position - transform.position).normalized;

        rb.linearVelocity = new Vector2(direction.x * moveSpeed * chaseSpeedMultiplier, direction.y * moveSpeed * chaseSpeedMultiplier);

        if ((direction.x > 0 && !movingRight) || (direction.x < 0 && movingRight))
        {
            Flip();
        }
    }

    bool CanSeePlayer()
    {
        if (player == null) return false;

        Vector2 direction = movingRight ? Vector2.right : Vector2.left;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, detectionRange, playerLayer);

        return hit.collider != null && hit.collider.CompareTag("Player");
    }

    void Flip()
    {
        movingRight = !movingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    IEnumerator Attack()
    {
        isAttacking = true;
        rb.linearVelocity = Vector2.zero; // Stop moving while attacking
        player.GetComponent<PlayerMovement>().Damage(); // Call player's damage function
        yield return new WaitForSeconds(attackDelay);
        isAttacking = false;
    }
}
