using UnityEngine;

public class RepeatTerrain : MonoBehaviour
{
    private Vector3 startPos;
    private float repeatLength;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        repeatLength = GetComponent<Terrain>().terrainData.size.z / 2;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (transform.position.z < startPos.z - repeatLength)
        {
            transform.position = startPos;
        }
    }
}
