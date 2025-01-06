using UnityEngine;

public class MoveDown : MonoBehaviour
{
    private CPURunnerPlayerController playerControllerScript;
    private float downBound = -20f;
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
            transform.Translate(Vector3.back * Time.deltaTime * difficultyManager.speed);
        }
    }
    void Update()
    {
        if (transform.position.z < downBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}