using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [Header("Menu Navigation")]
    [SerializeField] private PauseMenu pauseMenu;
    private bool isPaused;

    private bool pauseInput;

    void Start()
    {
        isPaused = false;
    }

    void Update()
    {
        pauseInput = Input.GetKeyUp(KeyCode.Q);
        if (pauseInput)
        {
            ChangePauseStatus();

        }

    }


    public void ChangePauseStatus()
    {
        isPaused = !isPaused;
        UpdateGamePause();
    }

    void UpdateGamePause()
    {
        if (isPaused)  //ferma il tempo
        {
            Time.timeScale = 0;
            Cursor.visible = true; //il cursore diventa visibile
            Cursor.lockState = CursorLockMode.None; //il cursore può muoversi
            pauseMenu.ActivateMenu();

        }
        else  //riavvia il tempo 
        {
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;  //il cursore si blocca
            Cursor.visible = false;  //il cursore non è visibile 
            pauseMenu.DeactivateMenu();

        }

    }
}
