using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
public class CPURunnerPlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float jumpForce;
    public bool isOnGround;
    public bool gameOver;
    private bool gameCompleted;
    private Animator playerAnim;
    public ParticleSystem dirtPart;
    public AudioSource landingFX;
    public AudioSource coinFX;
    private int score;
    private int scoreGoal = 100;

    private GameOverMenu gameOverMenu;
    private MinigameEndMenu minigameEndMenu;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        score = 0;
        gameCompleted = false;
        minigameEndMenu = GameObject.Find("MinigameEndCanvas").GetComponent<MinigameEndMenu>();
        gameOverMenu = GameObject.Find("GameOverCanvas").GetComponent<GameOverMenu>();
        isOnGround = true;
        gameOver = false;
        jumpForce = 1500;
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        Physics.gravity = new Vector3(0, -44, 0);
        playerAnim.SetBool("Grounded", true);
        playerAnim.SetFloat("Speed", 5);
        playerAnim.SetFloat("MotionSpeed", 2);
        dirtPart.Play();
    }

    // Update is called once per frame
    void Update()
    {

        UpdateProgress();

        //se si preme spazio mentre si è a terra e il gioco non è terminato
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            playerAnim.SetBool("Jump", true);
            isOnGround = false;
            dirtPart.Stop();
        }
        //se si perde si attiva il menu di gameover e le particelle si stoppano nel caso in cui il gioco non sia completo
        if (gameOver && !gameCompleted)
        {
            dirtPart.Stop();
            gameOverMenu.ActivateMenu();
        }
        //altrimenti mostra il menu di fine livello
        else if (gameOver && gameCompleted)
        {
            dirtPart.Stop();
            minigameEndMenu.ActivateMenu();
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            playerAnim.SetBool("Grounded", true);
            playerAnim.SetBool("Jump", false);
            dirtPart.Play();
            landingFX.Play();
        }
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            isOnGround = true;
            playerAnim.SetFloat("Speed", 0);
            playerAnim.SetFloat("MotionSpeed", 0);
            playerAnim.SetBool("Grounded", true);
            Debug.Log("Game Over!");
        }
        if (collision.gameObject.CompareTag("CPU"))
        {
            coinFX.Play();
        }
    }

    private void UpdateProgress()
    {
        score = GameObject.Find("ScoreManager").GetComponent<ScoreManager>().score;

        if (score > scoreGoal)
        {
            gameCompleted = true;
        }

    }
}