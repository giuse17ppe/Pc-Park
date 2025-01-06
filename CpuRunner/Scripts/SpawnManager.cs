using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public GameObject cpuPrefab;
    private CPURunnerPlayerController playerControllerScript;
    private Vector3 spawnPos = new Vector3(0, 0.5f, 15f);
    private float spawnDelay = 2f;
    private float repeatRate = 2f;
    private float repeatRateCPU = 1.25f;
    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<CPURunnerPlayerController>();
        InvokeRepeating("SpawnObstacle", spawnDelay, repeatRate);
        InvokeRepeating("SpawnCPU", spawnDelay, repeatRateCPU);
    }
    void SpawnObstacle()
    {
        if (!playerControllerScript.gameOver)
        {
            Instantiate(obstaclePrefab, spawnPos, obstaclePrefab.transform.rotation);
        }
    }

    void SpawnCPU()
    {
        if (!playerControllerScript.gameOver)
        {
            // Genera una posizione casuale per la CPU
            Vector3 spawnPosCPU = new Vector3(0, 2.7f, Random.Range(15f, 25f));
            Instantiate(cpuPrefab, spawnPosCPU, cpuPrefab.transform.rotation);
        }
    }
}
