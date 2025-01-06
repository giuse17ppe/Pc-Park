using System.Runtime.CompilerServices;
using NUnit.Framework.Internal;
using TMPro;
using UnityEngine;

public class MemorySpaceManager : MonoBehaviour
{
    public bool gameOver;
    public int score;
    public TextMeshProUGUI scoreText;
    private GameOverMenu gameOverMenu;
    private MinigameEndMenu minigameEndMenu;
    public bool gameCompleted;
    public int scoreGoal = 20;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameCompleted = false;
        gameOver = false;
        score = 0;
        gameOverMenu = GameObject.Find("GameOverCanvas").GetComponent<GameOverMenu>();
        minigameEndMenu = GameObject.Find("MinigameEndCanvas").GetComponent<MinigameEndMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver && !gameCompleted)
        {
            gameOverMenu.ActivateMenu();
        }
        else if (gameOver && gameCompleted)
        {
            minigameEndMenu.ActivateMenu();
        }
    }

    public void AddScore(int points)
    {
        score = score += points;
        UpdateScoreText();
    }

    public void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }

    public void resetScore()
    {
        score = 0;
        UpdateScoreText();
    }
}
