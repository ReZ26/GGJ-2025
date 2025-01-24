    using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private PlayerMovement player;
    [SerializeField] private TextMeshProUGUI txt_CurrentPoint;
    [SerializeField] private TextMeshProUGUI txt_MaxPoint;
    [SerializeField] private int maxScore = 5;
    [SerializeField] private LevelEnd gameover;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameover = LevelEnd.Instance;
        txt_MaxPoint.text = maxScore.ToString();
    }
    private void OnEnable()
    {
        player.onGetPoint += SetCurrentPoint;
    }
    private void OnDisable()
    {
        player.onGetPoint -= SetCurrentPoint;
    }
    void SetCurrentPoint(int score)
    {
        if(score >= maxScore )
        {
            player.GetComponent<PlayerMovement>().isDead=true;
            player.onDeath.Invoke();
            gameover.EndLevel(1);
            txt_CurrentPoint.text = score.ToString();
            return;
        }
        txt_CurrentPoint.text = score.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
