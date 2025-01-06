using UnityEngine;

public class movimento : MonoBehaviour
{
    private float speed = 20.0f;
    private float turnSpeed = 50.0f;
    private float horizontalInput;
    private float forwardInput;
    private Timer timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = GameObject.Find("GameManager").GetComponent<Timer>();

    }

    // Update is called once per frame
    void Update()
    {
        //se il tempo non è finito ci possiamo muovere
        if (timer.remainingTime != 0)
        {
            //qui prendiamo gli input del giocatore
            forwardInput = Input.GetAxis("Vertical");
            horizontalInput = Input.GetAxis("Horizontal");
            //il giocatore va avanti
            transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
            //la macchina ruota
            transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed * horizontalInput);

        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            Destroy(collision.gameObject);
            GameObject.Find("points").GetComponent<Points>().IncrementPoints();

        }


    }
}
