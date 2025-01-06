using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private Vector3 startPos;
    private Vector3 bounds;
    private float length;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        bounds = GetComponent<MeshCollider>().sharedMesh.bounds.size;
        bounds = Vector3.Scale(bounds, transform.lossyScale);
        length = bounds.x/2;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (transform.position.z < startPos.z - length)
        {
            transform.position = startPos;
        }
    }
}
