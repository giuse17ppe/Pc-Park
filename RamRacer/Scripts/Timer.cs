using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    //script da inserire nel game manager del minigioco
    //to do - fermare tutto quando finisce timer


    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] public float remainingTime;


    private GameOverMenu gameOverMenu;
    private MinigameEndMenu minigameEndMenu;
    private RamRacerManager ramRacerManager;

    void Start()
    {
        minigameEndMenu = GameObject.Find("MinigameEndCanvas").GetComponent<MinigameEndMenu>();
        gameOverMenu = GameObject.Find("GameOverCanvas").GetComponent<GameOverMenu>();
        ramRacerManager = this.GetComponent<RamRacerManager>();
    }

    void Update()
    {
        ramRacerManager.CheckCompletedGame();

        if (remainingTime > 0)
        {

            remainingTime -= Time.deltaTime;
        }
        else if (remainingTime < 0)
        {
            if (ramRacerManager.gameCompleted)
            {
                minigameEndMenu.ActivateMenu();

            }
            else
            {
                gameOverMenu.ActivateMenu();

            }


            remainingTime = 0;

        }

        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
