using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] TargetPrefarbs;
    private float spawnRangeX = 17;
    private float spawnPosZ = 20;
    private float startDelay = 2;
    private float spawnInterval = 1.5f;
    private MemorySpaceManager memorySpaceManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("SpawnRandomTarget", startDelay, spawnInterval);
        memorySpaceManager = GameObject.Find("MemorySpaceManager").GetComponent<MemorySpaceManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void SpawnRandomTarget()
    {
        if (!memorySpaceManager.gameOver)
        {
            int targetIndex = Random.Range(0, TargetPrefarbs.Length);
            Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, spawnPosZ);
            Instantiate(TargetPrefarbs[targetIndex], spawnPos, TargetPrefarbs[targetIndex].transform.rotation);
        }
    }
}
