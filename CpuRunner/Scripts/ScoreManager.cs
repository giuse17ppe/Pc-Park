using TMPro; // Per utilizzare TextMeshPro
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int score = 0; // Punteggio iniziale
    public TextMeshProUGUI scoreText; // Riferimento al testo UI

    private void Start()
    {
        UpdateScoreText();
    }

    // Metodo per aggiungere punti
    public void AddScore(int points)
    {
        score += points;
        UpdateScoreText();
    }

    // Metodo per aggiornare il testo UI
    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }

    // Metodo per resettare il punteggio
    public void ResetScore()
    {
        score = 0;
        UpdateScoreText();
    }
}