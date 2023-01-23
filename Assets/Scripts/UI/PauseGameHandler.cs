using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PauseGameHandler : UIControllerBase
{
    [SerializeField] private GameObject pauseButton; // it needs to write a custom editor to write button instead of GO

    public bool IsGamePaused { get; private set; }

    private void Start()
    {
        pauseButton.GetComponent<Button>().onClick.AddListener(Pause);
    }

    public override void ShowUI()
    {
        canvasGroup.DOFade(1, .5f).SetUpdate(true);
    }

    public void Pause()
    {
        if (!IsGamePaused)
            OnPauseGameCommand();
        else
            OnUnPauseGameCommand();

        IsGamePaused = !IsGamePaused;
    }

    private void OnPauseGameCommand()
    {
        Time.timeScale = 0;
        ShowUI();
        pauseButton.SetActive(false);
    }


    private void OnUnPauseGameCommand()
    {
        Time.timeScale = 1;
        pauseButton.SetActive(true);

        HideUI();
    }
}