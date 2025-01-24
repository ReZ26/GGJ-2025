using TMPro;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private PlayerMovement player;
    [SerializeField] private TextMeshProUGUI txt_CurrentPoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
    private void OnEnable()
    {
        player.onGetHealth += SetCurrentPoint;
    }
    void SetCurrentPoint(int score)
    {
        txt_CurrentPoint.text = score.ToString();
    }
    // Update is called once per frame
    void Update()
    {

    }
}
