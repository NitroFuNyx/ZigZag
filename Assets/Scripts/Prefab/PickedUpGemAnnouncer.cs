using DG.Tweening;
using UnityEngine;

public class PickedUpGemAnnouncer : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;

    private void Start()
    {
        transform.DOMoveY(transform.localPosition.y + 3f, 3);
        canvasGroup.DOFade(0, 1.5f);
    }
}