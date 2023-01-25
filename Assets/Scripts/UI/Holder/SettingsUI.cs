using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SettingsUI : UIControllerBase
{
    [SerializeField] private GameObject backButton; // it needs to write a custom editor to write button instead of GO

    [SerializeField]
    private GameObject settingsButton; // it needs to write a custom editor to write button instead of GO

    private GameManager _manager;
    private StartGameUI _startGameUI;

    private void Start()
    {
        backButton.GetComponent<Button>().onClick.AddListener(HideUI);
        settingsButton.GetComponent<Button>().onClick.AddListener(ShowUI);
    }

    [Inject]
    private void InjectDependencies(GameManager manager, StartGameUI startGameUI)
    {
        _startGameUI = startGameUI;
        _manager = manager;
    }

    public override void HideUI()
    {
        base.HideUI();
        _startGameUI.ShowUI();
    }

    public override void ShowUI()
    {
        base.ShowUI();
        _startGameUI.HideUI();
    }

    public void EnableCheatMode(bool value)
    {
        _manager.EnableCheatMode(value);
    }
}