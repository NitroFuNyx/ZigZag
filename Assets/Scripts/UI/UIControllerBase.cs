using UnityEngine;
using DG.Tweening;
public abstract class UIControllerBase : MonoBehaviour
{
    [SerializeField] protected CanvasGroup canvasGroup;
    public virtual void HideUI()
    {
        canvasGroup.DOFade(0, .5f);
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
    public virtual void ShowUI()
    {
        canvasGroup.DOFade(1, .5f);
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }
}
