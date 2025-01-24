using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private GameObject healthEffect;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<PlayerMovement>().Health();
            Instantiate(healthEffect,transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
