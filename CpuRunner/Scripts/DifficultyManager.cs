using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public float speed = 11f;
    private float speedIncrement;
    private int perScoreTrigger = 70;
    private int score;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        score = GameObject.Find("ScoreManager").GetComponent<ScoreManager>().score;
    }
    // Update is called once per frame
    void Update()
    {

        UpdateSpeed();

    }

    void UpdateSpeed()
    {
        score = GameObject.Find("ScoreManager").GetComponent<ScoreManager>().score;
        if (score == perScoreTrigger)
        {
            speedIncrement = speed * 0.2f;
            speed += speedIncrement;
            perScoreTrigger += 70;
        }
    }
}