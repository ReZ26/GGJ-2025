using DG.Tweening;
using UnityEngine;

public class MovingPlatfrom : MonoBehaviour
{
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float offset = 0.5f;
    private const float duration=2f;
    private Sequence seq;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        minX = transform.localPosition.x;
        maxX=minX + offset;
        seq = DOTween.Sequence();
        var value = Random.RandomRange(0.8f, 1f);
        seq.Append(transform.DOLocalMoveX(maxX, duration * value).SetEase(Ease.InOutSine))
            .Append(transform.DOLocalMoveX(minX, duration * value).SetEase(Ease.InOutSine))
            .SetLoops(-1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
