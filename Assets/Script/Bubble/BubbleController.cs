using UnityEngine;

public class BubbleController : MonoBehaviour
{
    private float popTime;
    public CircleCollider2D bubbleCollider;
    public GameObject rewardItem;
    public Animator animator;
    public GameObject popEffect;
    private void Awake()
    {
        bubbleCollider = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        ResetTimer();
    }

    public void ResetTimer()
    {
        popTime = Time.time + 5f; // Reset to 5 seconds from now
    }

    void Update()
    {
        if (Time.time >= popTime)
        {
            DestoryandClear();
        }
    }
    public void DestoryandClear()
    {

        if (transform.childCount > 0)
        {
            foreach (Transform child in transform)
            {
                var enemyObject = child;
                enemyObject.SetParent(null);
                enemyObject.GetComponent<Rigidbody2D>().simulated = true;
                if (enemyObject.GetComponent<EnemyAI>() != null)
                {

                    enemyObject.localScale = Vector3.one;
                    enemyObject.GetComponent<EnemyAI>().enabled = true;
                }
                else
                {
                    enemyObject.localScale = Vector3.one* 0.8f;
                    enemyObject.GetComponent<RangedEnemyAI>().enabled = true;
                }
            }

        }
        var gameobject = Instantiate(popEffect, transform.position, Quaternion.identity);
        gameobject.transform.localScale = transform.localScale + Vector3.one * 0.4f;
        Destroy(gameObject); // Pop the bubble
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spike"))
        {
            if(transform.childCount > 0)
            {
                foreach (Transform child in transform)
                {
                    if (child.GetComponent<EnemyAI>() != null)
                    {
                        var randomVector = new Vector3(Random.RandomRange(-0.1f, 0.1f), Random.RandomRange(-0.1f, 0.1f));
                        Instantiate(rewardItem, transform.position+ randomVector, Quaternion.identity);
                    }
                    else
                    {
                        if(child.GetComponent<RangedEnemyAI>().Damage())
                            for(int i=0;i<8;i++)
                            {
                                var randomVector = new Vector3(Random.RandomRange(-0.1f, 0.1f), Random.RandomRange(-0.1f, 0.1f));
                                Instantiate(rewardItem, transform.position+randomVector, Quaternion.identity);
                            }
                        else
                        {
                            child.SetParent(null);
                            child.localScale = Vector3.one * 0.8f;
                            child.GetComponent<Rigidbody2D>().simulated = true;
                            child.GetComponent<RangedEnemyAI>().enabled = true;
                        }
                    }
                }
            }
            var gameobject = Instantiate(popEffect, transform.position, Quaternion.identity);
            gameobject.transform.localScale = transform.localScale + Vector3.one * 0.4f;
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Bubble"))
        {
            BubbleController otherBubble = collision.gameObject.GetComponent<BubbleController>();

            if (otherBubble != null)
            {
                if (transform.localScale.magnitude >= otherBubble.transform.localScale.magnitude)
                {
                    transform.localScale += (otherBubble.transform.localScale - new Vector3(0.3f, 0.3f, 0.3f));
                    transform.localScale = new
                        Vector3(Mathf.Clamp(transform.localScale.x, 0.3f, 0.7f),
                        Mathf.Clamp(transform.localScale.y, 0.3f, 0.7f), 1);
                    ResetTimer();


                    if (otherBubble.transform.childCount > 0)
                    {
                        var enemyObject = otherBubble.transform.GetChild(0);
                        enemyObject.localScale = 2.9f*Vector3.one;
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
                    var gameobject=Instantiate(popEffect, otherBubble.transform.position, Quaternion.identity);
                    gameobject.transform.localScale=transform.localScale+Vector3.one*0.4f;
                    Destroy(otherBubble.gameObject);
                }
            }
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            if (collision.collider.bounds.size.magnitude < bubbleCollider.bounds.size.magnitude)
            {
                if(collision.transform.GetComponent<EnemyAI>()!=null)
                    collision.transform.GetComponent<EnemyAI>().enabled = false;
                else if (collision.transform.GetComponent<RangedEnemyAI>()!=null)
                {
                    collision.transform.GetComponent<RangedEnemyAI>().enabled=false;
                }
                collision.transform.GetComponent<Rigidbody2D>().simulated = false;
                collision.transform.SetParent(transform); collision.transform.localPosition = Vector3.zero;
            }
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (collision.collider.bounds.size.magnitude < bubbleCollider.bounds.size.magnitude)
            {
                if (collision.transform.GetComponent<EnemyAI>() != null)
                    collision.transform.GetComponent<EnemyAI>().enabled = false;
                else if (collision.transform.GetComponent<RangedEnemyAI>() != null)
                {
                    collision.transform.GetComponent<RangedEnemyAI>().enabled = false;
                }
                collision.transform.GetComponent<Rigidbody2D>().simulated = false;
                collision.transform.SetParent(transform); collision.transform.localPosition = Vector3.zero;
            }
        }
    }
}
