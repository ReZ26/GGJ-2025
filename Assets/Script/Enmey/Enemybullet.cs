using UnityEngine;

public class Enemybullet : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerMovement>().Damage();
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Bubble"))
        {
            collision.gameObject.GetComponent<BubbleController>().DestoryandClear();
            Destroy(gameObject);
        }
        else if(collision.gameObject.CompareTag("Enemy"))
        {
            return;
        }
        else if (collision.gameObject.CompareTag("Reward"))
        {
            return;
        }
        else
        {
            Destroy(gameObject);

        }
    }
}
