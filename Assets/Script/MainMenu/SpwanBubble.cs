using System.Collections;
using UnityEngine;

public class SpwanBubble : MonoBehaviour
{
    [SerializeField] private GameObject bubblePrefab;
    [SerializeField] private float duration=5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnBubbleAtMouse());
    }
    IEnumerator SpawnBubbleAtMouse()
    {
        yield return new WaitForSeconds(duration);
        Vector2 spawnPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GameObject newBubble = Instantiate(bubblePrefab, spawnPosition, Quaternion.identity);
        newBubble.transform.localScale = 0.3f * Vector3.one + Vector3.one * Random.RandomRange(0, 0.2f);
        StartCoroutine(SpawnBubbleAtMouse());
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
