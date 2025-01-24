using System.Collections;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private GameObject EnemyPrefab;
    [SerializeField] private int maxSpawn=5;
    private float duration=5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnEnemyDelay());
    }
    IEnumerator SpawnEnemyDelay()
    {
        Instantiate(EnemyPrefab,transform.position,Quaternion.identity);
        maxSpawn--;
        EnemyPrefab.GetComponent<EnemyAI>().moveSpeed = Random.RandomRange(0.5f,1f);
        yield return new WaitForSeconds(duration);
        duration = Mathf.Clamp(duration - 0.2f, 1f, 5f);
        if(maxSpawn>0)
            StartCoroutine(SpawnEnemyDelay());
        else if(!LevelEnd.Instance.isEnded)
        {
            yield return new WaitForSeconds(duration*2f);
            StartCoroutine(SpawnEnemyDelay());
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
