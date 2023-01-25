using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Button = UnityEngine.UI.Button;

public class GameOverUI : UIControllerBase, IUIUpdater
{
    [SerializeField]
    private GameObject restartButton; // it needs to write a custom editor to write button instead of GO

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI bestScoreText;

    private void Start()
    {
        restartButton.GetComponent<Button>().onClick.AddListener(RestartGame);
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(0);
    }


    public void UpdateScore(string text)
    {
        scoreText.text = text;
    }

    public void UpdateBestScoreText(string text)
    {
        bestScoreText.text = text;
    }
}