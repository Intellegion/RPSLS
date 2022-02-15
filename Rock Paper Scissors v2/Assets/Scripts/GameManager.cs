using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Hand[] possibleHands;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    [SerializeField]
    private TextMeshProUGUI endScoreText;

    [SerializeField]
    private TextMeshProUGUI highScoreText;

    [SerializeField]
    private TextMeshProUGUI resultText;

    [SerializeField]
    private TextMeshProUGUI enemyText;

    [SerializeField]
    private TextMeshProUGUI sliderText;

    [SerializeField]
    private Slider timerSlider;

    [SerializeField]
    private CanvasGroup menuPanel;

    [SerializeField]
    private CanvasGroup gamePanel;

    [SerializeField]
    private float maxTimer;

    private Hand enemyHand;
    private Hand playerHand;
    private Hand temporaryHand;

    private int score;
    private int highScore;

    private bool didGameStart;

    private void Start()
    {
        highScore = PlayerPrefs.GetInt("highscore");
        highScoreText.text = "High Score = " + highScore;
    }

    private void Update()
    {
        if (didGameStart)
        {
            if (timerSlider.value > 0)
            {
                timerSlider.value -= Time.deltaTime;
                sliderText.text = "Timer : " + timerSlider.value.ToString("0.00") + "s";
                if (timerSlider.value <= 0)
                {
                    Defeat();
                }
            }
        }
    }

    public void Initialize()
    {
        timerSlider.maxValue = maxTimer;
        timerSlider.value = maxTimer;
        score = 0;
        highScoreText.text = "High Score = " + highScore;
        scoreText.text = "Score = 0";
        resultText.text = "";
        SetEnemyHand();
        didGameStart = true;
    }

    public void GetPlayerHand(Hand hand)
    {
        playerHand = hand;
        CompareHands();
    }

    private void CompareHands()
    {
        if (Array.Exists(playerHand.VictoryTypes, element => element == enemyHand.HandType))
        {
            Victory();
        }

        else
        {
            Defeat();
        }
    }

    private void Victory()
    {
        score++;
        scoreText.text = "Score = " +  score;
        ResetTimer();
        SetEnemyHand();
    }

    private void Defeat()
    {
        HidePanel(gamePanel);
        ShowPanel(menuPanel);

        resultText.text = "DEFEAT";
        endScoreText.text = "Score = " + score;

        if (score > highScore)
        {
            PlayerPrefs.SetInt("highscore", score);
            highScore = score;
            highScoreText.text = "High Score = " + score.ToString();
        }
    }
    private void ResetTimer()
    {
        timerSlider.value = maxTimer;
    }

    private void SetEnemyHand()
    {
        enemyHand = GetRandomHand();
        enemyText.text = enemyHand.name;
    }

    private Hand GetRandomHand()
    {
        do
        {
            temporaryHand = possibleHands[UnityEngine.Random.Range(0, possibleHands.Length)];

        } while (temporaryHand == enemyHand);

        return temporaryHand;
    }

    public void ShowPanel(CanvasGroup panel)
    {
        panel.alpha = 1;
        panel.blocksRaycasts = true;
        panel.interactable = true;
    }

    public void HidePanel(CanvasGroup panel)
    {
        panel.alpha = 0;
        panel.blocksRaycasts = false;
        panel.interactable = false;
    }
}
