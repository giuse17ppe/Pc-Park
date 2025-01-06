using UnityEngine;

public class RamRacerManager : MonoBehaviour
{
    //script da inserire nel game manager del minigioco

    public bool gameCompleted;
    private Points pointsManager;
    private int goal;

    void Start()
    {
        pointsManager = GameObject.Find("points").GetComponent<Points>();
    }

    public void CheckCompletedGame()
    {
        if (pointsManager.points >= 10)
        {
            gameCompleted = true;
        }
    }


}
