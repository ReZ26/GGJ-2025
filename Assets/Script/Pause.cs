using UnityEngine;

public class Pause : MonoBehaviour
{
    public PlayerMovement player;
    public BubbleMechanic bubble;
    public EnemyAI[] enemyAI;
    public RangedEnemyAI rangedEnemy;
    public SpawnEnemy[] spawnEnemy;
    public GameObject tutorialPanel;
    public Animator animator;
    private void Awake()
    {
        player.enabled = false;
        bubble.enabled = false;
        if(enemyAI != null)
        foreach(EnemyAI enemy in enemyAI)
            enemy.enabled = false;
        if (spawnEnemy != null)
            foreach (SpawnEnemy enemy in spawnEnemy) enemy.enabled = false;
        if(rangedEnemy != null)
        {
            rangedEnemy.enabled = false;
            animator.SetTrigger("Boss");
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public void PlayStart()
    {
        player.enabled = true;
        bubble.enabled = true;
        if (enemyAI != null)
            foreach (EnemyAI enemy in enemyAI)
            enemy.enabled = true;
        if (spawnEnemy != null)
            foreach (SpawnEnemy enemy in spawnEnemy) enemy.enabled = true;
        if (rangedEnemy != null)
            rangedEnemy.enabled = true;
        tutorialPanel.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
