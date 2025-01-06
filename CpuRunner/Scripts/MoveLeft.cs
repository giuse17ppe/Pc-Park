using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private CPURunnerPlayerController playerControllerScript;
    private float leftBound = -11f;
    public int pointsCPU = 10;
    DifficultyManager difficultyManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<CPURunnerPlayerController>();
        difficultyManager = FindFirstObjectByType<DifficultyManager>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        difficultyManager = FindFirstObjectByType<DifficultyManager>();
        if (playerControllerScript.gameOver == false)
        {
            transform.Translate(Vector3.left * Time.deltaTime * difficultyManager.speed);
        }
    }
    void Update()
    {
        if (transform.position.z < leftBound && gameObject.CompareTag("CPU"))
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            ScoreManager scoreManager = FindFirstObjectByType<ScoreManager>();
            if (scoreManager != null)
            {
                scoreManager.AddScore(pointsCPU);
            }
        }
    }
}