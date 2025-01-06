using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class GameOverMenu : Menu
{
    [Header("Game Over Menu")]
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private Button retryButton;
    [SerializeField] private Button BackToMenuButton;







    public void ActivateMenu()
    {
        gameOverMenu.SetActive(true);
        this.SetButtonsInteractable(true);
        Cursor.visible = true; //il cursore diventa visibile
        Cursor.lockState = CursorLockMode.None; //il cursore può muoversi
    }


    public void OnRetryClicked()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadSceneAsync(scene.name);
    }

    public void OnBackToMapClicked()
    {
        DataPersistenceManager.instance.SaveGame();
        DataPersistenceManager.instance.GetAllProfilesGamaData();
        SceneManager.LoadSceneAsync("Mappa Principale");
    }

    void SetButtonsInteractable(bool b)
    {
        this.retryButton.interactable = b;
        this.BackToMenuButton.interactable = b;
    }
}
