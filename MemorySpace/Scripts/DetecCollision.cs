using System;
using UnityEngine;

public class DetecCollision : MonoBehaviour
{
    public MemorySpaceManager memorySpaceManager;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        memorySpaceManager = GameObject.Find("MemorySpaceManager").GetComponent<MemorySpaceManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Target") && this.CompareTag("Shot"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
            memorySpaceManager.AddScore(1);
        }
        else if (this.CompareTag("Player") && other.CompareTag("HDD"))
        {
            Destroy(other.gameObject);
            memorySpaceManager.AddScore(5);
        }
        else if (this.CompareTag("Player") && other.CompareTag("Target"))
        {
            memorySpaceManager.gameOver = true;
            if (memorySpaceManager.score >= memorySpaceManager.scoreGoal)
            {
                memorySpaceManager.gameCompleted = true;
            }

        }

    }
}
