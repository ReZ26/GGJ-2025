using DG.Tweening;
using UnityEngine;

public class UpDownPlatform : MonoBehaviour
{
    [SerializeField] private float minY;
    [SerializeField] private float maxY;
    [SerializeField] private float offset = 0.5f;
    private const float duration = 2f;
    private Sequence seq;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        minY = transform.localPosition.y;
        maxY = minY + offset;
        seq = DOTween.Sequence();
        var value = Random.RandomRange(0.8f, 1f);
        seq.Append(transform.DOLocalMoveY(maxY, duration * value).SetEase(Ease.InOutSine))
            .Append(transform.DOLocalMoveY(minY, duration * value).SetEase(Ease.InOutSine))
            .SetLoops(-1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
