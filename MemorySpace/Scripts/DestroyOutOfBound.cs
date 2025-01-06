
using UnityEngine;

public class DestroyOutOfBound : MonoBehaviour
{
    private float topBound = 35;
    private float lowerBound = -10;
    private MemorySpaceManager memorySpaceManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        memorySpaceManager = GameObject.Find("MemorySpaceManager").GetComponent<MemorySpaceManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z > topBound)
        {
            Destroy(gameObject);
        }
        else if (transform.position.z < lowerBound)
        {
            Destroy(gameObject);
            if (this.CompareTag("Target"))
            {
                if (memorySpaceManager.score > memorySpaceManager.scoreGoal)
                {
                    memorySpaceManager.gameCompleted = true;
                    memorySpaceManager.gameOver = true;
                }
                else
                {
                    memorySpaceManager.gameOver = true;
                }
            }
        }
    }
}

