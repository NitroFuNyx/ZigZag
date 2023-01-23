
using TMPro;
using UnityEngine;

public class StartGameUI : UIControllerBase,IUIUpdater
{
    [SerializeField] private TextMeshProUGUI bestScoreText;
    [SerializeField] private TextMeshProUGUI gamesPlayedText;

    public  void UpdateScore(string text)
    {
        bestScoreText.text = text;
    }
    public  void UpdateGamesPlayed(string text)
    {
        gamesPlayedText.text = text;
    }
    
}
