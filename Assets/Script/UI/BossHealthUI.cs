using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthUI : MonoBehaviour
{
    [SerializeField] private RangedEnemyAI enemy;
    [SerializeField] private Slider healthSlider;
     // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthSlider.value =1;
    }
    private void OnEnable()
    {
        enemy.onDamageDone += SetCurrentPoint;
    }
    void SetCurrentPoint(float score)
    {
        healthSlider.value = score;

    }
}
