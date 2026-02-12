using NUnit.Framework.Internal;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Screens")]
    [SerializeField] GameObject mainMenuUI;
    [SerializeField] GameObject gameplayUI;
    [SerializeField] GameObject gameOverUI;

    [Header("Menu UI")]
    [SerializeField] TextMeshProUGUI menuBestScoreText;

    [Header("Gameplay UI")]
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI gameplayBestScoreText;
    [SerializeField] TextMeshProUGUI livesText;

    [Header("GameOver UI")]
    [SerializeField] TextMeshProUGUI finalScoreText;
    [SerializeField] TextMeshProUGUI gameOverBestScoreText;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        ShowMainMenu();
        UpdateBestUI(ScoreManager.Instance.BestScore);
    }

    public void ShowMainMenu()
    {
        mainMenuUI.SetActive(true);
        gameplayUI.SetActive(false);
        gameOverUI.SetActive(false);

        UpdateBestUI(ScoreManager.Instance.BestScore);
    }

    public void ShowGameplay()
    {
        mainMenuUI.SetActive(false);
        gameplayUI.SetActive(true);
        gameOverUI.SetActive(false);
    }

    public void ShowGameOver()
    {
        gameplayUI.SetActive(false);
        gameOverUI.SetActive(true);

        finalScoreText.text = "Score : " + ScoreManager.Instance.CurrentScore;
        UpdateBestUI(ScoreManager.Instance.BestScore);
    }

    public void PlayButton()
    {
        GameManager.Instance.StartGame();
    }

    public void RetryButton()
    {
        GameManager.Instance.StartGame();
    }

    public void UpdateLives(int lives)
    {
        livesText.text = "Lives : " + lives;
    }

    void UpdateScoreUI(int score)
    {
        if (scoreText != null)
            scoreText.text = "Score : " + score.ToString();
    }

    public void UpdateBestUI(int best)
    {
        if (menuBestScoreText != null)
            menuBestScoreText.text = "Best Score : " + best;

        if (gameplayBestScoreText != null)
            gameplayBestScoreText.text = "Best Score : " + best;

        if (gameOverBestScoreText != null)
            gameOverBestScoreText.text = "Best Score : " + best;
    }
}
