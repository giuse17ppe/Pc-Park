using UnityEngine;

public class ControlSpaceShip : MonoBehaviour
{

    public float horizontalInput;
    public float speed = 10.0f;
    public float xRange = 17f;
    private MemorySpaceManager memorySpaceManager;

    public GameObject projectilePrefarb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        memorySpaceManager = GameObject.Find("MemorySpaceManager").GetComponent<MemorySpaceManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!memorySpaceManager.gameOver)
        {
            Movement();
            Shoot();
        }


    }
    private void Movement()
    {
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }
        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);
    }

    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Launch a projectile from the player
            Instantiate(projectilePrefarb, transform.position, projectilePrefarb.transform.rotation);
        }
    }
}
