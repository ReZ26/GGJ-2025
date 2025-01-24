using UnityEngine;

public class ItemReward : MonoBehaviour
{
    [SerializeField] private GameObject itemCollectedEffect;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            transform.SetParent(collision.gameObject.transform);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerMovement>().GetPoint();
            Instantiate(itemCollectedEffect,transform.position,Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
