using UnityEngine;
using DG.Tweening;

/// <summary>
/// 监控摄像头的入口类
/// </summary>
public class CctvBehaviour : MonoBehaviour
{
    public Transform rotatePart;
    public float waitTime;
    public float swipeTime;
    public float swipeHalfAngle;

    void Start()
    {
        SwipeRight();
    }

    void SwipeRight()
    {
        rotatePart.DOKill();
        rotatePart.DOLocalRotate(new Vector3(0, swipeHalfAngle, 0), swipeTime).SetDelay(waitTime).SetEase(Ease.InOutCubic).OnComplete(SwipeLeft);
    }

    void SwipeLeft()
    {
        rotatePart.DOKill();
        rotatePart.DOLocalRotate(new Vector3(0, -swipeHalfAngle, 0), swipeTime).SetDelay(waitTime).SetEase(Ease.InOutCubic).OnComplete(SwipeRight);
    }
}
