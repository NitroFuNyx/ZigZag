using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class TextFading : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tapToPlay;

    private void Start()
    {
        StartCoroutine(DoFade());
    }


    private IEnumerator DoFade()
    {
        tapToPlay.DOFade(0, 2f).SetUpdate(true);
        yield return new WaitForSecondsRealtime(2f);
        StartCoroutine(DoUnfade());
    }

    private IEnumerator DoUnfade()
    {
        tapToPlay.DOFade(1, 2f).SetUpdate(true);
        yield return new WaitForSecondsRealtime(2f);

        StartCoroutine(DoFade());
    }
}