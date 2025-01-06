using UnityEngine;

public class Rotazione : MonoBehaviour
{
    public float rotationSpeed = 100f; // Velocità di rotazione in gradi al secondo

    private void Update()
    {
        // Ruota la moneta attorno all'asse Y
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
