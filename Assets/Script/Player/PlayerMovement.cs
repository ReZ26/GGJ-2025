using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public Rigidbody2D rb;
    public Animator animator;

    [Header("Parallax Settings")]
    public Material parallaxMaterial;
    public float parallaxMultiplier = 0.5f;

    public int point = 0;
    public Action<int> onGetPoint;

    [Header("Health")]
    [SerializeField]private int health = 3;
    [SerializeField] private int maxHealth;
    public bool isDead = false;
    public Action<int> onGetHealth;
    public Action onDeath;
    private Vector2 offset;
    private bool facingRight = true;
    private Vector3 lastPosition;

    [Header("Ground Check")]
    public Transform groundCheck; // Assign in Inspector, position slightly below the player
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer; // Assign Ground layer in Inspector
    private bool isGrounded = false;
    [SerializeField] private LevelEnd gameover;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        maxHealth = health;
        gameover = LevelEnd.Instance;
        onGetPoint?.Invoke(point);
        onGetHealth?.Invoke(health);
    }

    void Update()
    {
        CheckGrounded(); 

        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        bool isMoving = Mathf.Abs(transform.position.x - lastPosition.x) > 0.001f;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) 
        {
            Jump();
        }

     
        if (!isGrounded)
        {
            animator.SetBool("Jump", true);
            animator.SetBool("Walk", false);
            animator.SetBool("Idle", false);
        }
        else if (moveInput != 0)
        {
            animator.SetBool("Jump", false);
            animator.SetBool("Idle", false);
            animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Jump", false);
            animator.SetBool("Walk", false);
            animator.SetBool("Idle", true);

        }

     
        if (isMoving)
        {
            offset.x += moveInput * Time.deltaTime * moveSpeed * parallaxMultiplier;
            parallaxMaterial.mainTextureOffset = offset;
        }

       
        if (moveInput > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveInput < 0 && facingRight)
        {
            Flip();
        }

        lastPosition = transform.position;
    }

    void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        isGrounded = false; 
    }

    void CheckGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (collision.contacts[0].normal.y == 1)
            {
                transform.SetParent(collision.transform);
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        transform.SetParent(null);
    }
    public void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public void GetPoint()
    {
        point++;
        onGetPoint?.Invoke(point);
    }

    public void Damage()
    {
        if (!isDead)
        {
            animator.SetTrigger("Hurt");
            health--;
            onGetHealth?.Invoke(health);
            if (health <= 0)
            {
                isDead = true;
                animator.SetTrigger("Death");
                onDeath?.Invoke();
                Debug.Log("Player Dead");
                enabled = false;
                Invoke("Try", 1f);
            }
        }
    }
    public void Health()
    {
        health = Mathf.Clamp(health+1, 0, maxHealth);
        onGetHealth?.Invoke(health);
    }
    void Try() { gameover.EndLevel(2);
    }
}
