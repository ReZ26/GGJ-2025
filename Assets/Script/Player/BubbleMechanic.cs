using UnityEngine;

public class BubbleMechanic : MonoBehaviour
{
    public GameObject bubblePrefab; // Assign a bubble prefab in the Inspector
    public float bubbleLifetime = 5f; // Time before popping
    public float sizeIncreaseFactor = 1.1f; 
    public LayerMask bubbleLayer; // Layer to detect existing bubbles
    public LayerMask otherLayer; // Layer to detect existing bubbles
    public Animator animator;
    public PlayerMovement playerMovement;
    public AudioSource audio;
    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
        audio=GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        playerMovement.onDeath += onDeathChange;
    }
    private void OnDisable()
    {
        playerMovement.onDeath -= onDeathChange;
    }
    void onDeathChange()
    {
        enabled = false;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
           
            HandleBubbleSpawn();
        }
    }

    private void HandleBubbleSpawn()
    {
        Vector2 spawnPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (transform.position.x > spawnPosition.x)
        {
            if (transform.localScale.x > 0)
                playerMovement.Flip();
        }
        else
        {
            if (transform.localScale.x < 0) playerMovement.Flip();
        }

        Collider2D existingBubble = Physics2D.OverlapPoint(spawnPosition, bubbleLayer);
        Collider2D isThereAnyObject= Physics2D.OverlapPoint(spawnPosition, otherLayer);
        if (isThereAnyObject != null)
        {
            return;
        }
        else if (existingBubble != null)
        {
            audio.Play();
            animator.SetTrigger("Attack");
            existingBubble.transform.localScale += (Vector3.one * sizeIncreaseFactor);
            existingBubble.transform.localScale = new
                Vector3(Mathf.Clamp(existingBubble.transform.localScale.x, 0.3f,0.7f),
                Mathf.Clamp(existingBubble.transform.localScale.y, 0.3f, 0.7f), 1);
            if (existingBubble.transform.childCount > 0)
            {
                foreach (Transform child in existingBubble.transform)
                {
                    var enemyObject = child;
                    enemyObject.localScale = 2.9f * Vector3.one;
                    enemyObject.SetParent(null);
                    if (enemyObject.transform.GetComponent<EnemyAI>() != null)
                        enemyObject.transform.GetComponent<EnemyAI>().enabled = true;
                    else
                    {
                        enemyObject.localScale = Vector3.one * 0.8f;
                        enemyObject.transform.GetComponent<RangedEnemyAI>().enabled = true;
                    }
                    enemyObject.GetComponent<Rigidbody2D>().simulated = true;
                }
            }
            BubbleController bubbleScript = existingBubble.GetComponent<BubbleController>();
            if (bubbleScript != null)
            {
                bubbleScript.ResetTimer();
            }
        }
        else
        {
            audio.Play();
            animator.SetTrigger("Attack");
            GameObject newBubble = Instantiate(bubblePrefab, spawnPosition, Quaternion.identity);
            newBubble.transform.localScale = 0.3f*Vector3.one + Vector3.one * Random.RandomRange(0,0.2f);
        }
    }
}
