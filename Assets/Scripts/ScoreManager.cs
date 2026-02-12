using UnityEngine;
using TMPro;
using System;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public static Action<int> OnScore;

    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI bestText;

    int score;
    int best;
    int shootCount;

    public int CurrentScore => score;
    public int BestScore => best;

    private void Awake()
    {
        Instance = this;

        best = PlayerPrefs.GetInt("BEST", 0);

        UpdateBestUI();
        UpdateScoreUI();
    }

    public void ResetScore()
    {
        score = 0;
        shootCount = 0;

        UpdateScoreUI();
    }

    public void AddPoint()
    {
        score++;
        shootCount++;

        UpdateScoreUI();

        if (score > best)
        {
            best = score;
            PlayerPrefs.SetInt("BEST", best);
            PlayerPrefs.Save();

            UpdateBestUI();
        }

        OnScore?.Invoke(score);
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Score : " + score.ToString();
    }

    void UpdateBestUI()
    {
        if (bestText != null)
            bestText.text = "Best Score : " + best;
    }
}
