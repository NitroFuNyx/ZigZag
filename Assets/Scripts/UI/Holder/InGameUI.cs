using TMPro;
using UnityEngine;

public class InGameUI : UIControllerBase,IUIUpdater
{
    [SerializeField] private TextMeshProUGUI inGameScore;
    [SerializeField] private TextMeshProUGUI crystalScore;
    

    public  void UpdateScore(string text)
    {
        inGameScore.text = text;
    }
    public  void UpdateCrystalScore(string text)
    {
        crystalScore.text = text;
    }
    
    
}
