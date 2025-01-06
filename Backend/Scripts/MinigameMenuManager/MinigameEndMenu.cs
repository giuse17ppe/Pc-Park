using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MinigameEndMenu : Menu
{

    [Header("MinigameEndMenu")]
    [SerializeField] private GameObject minigameEndMenu;
    [SerializeField] private TextMeshProUGUI minigameEndText;
    [SerializeField] private Button continueButton;

    [Header("Premio per aver portato a termine il minigioco")]
    [SerializeField] private int progress;

    private PersistentScoreManager persistentScoreManager;

    void Start()
    {
        persistentScoreManager = GameObject.Find("PersistentScoreManager").GetComponent<PersistentScoreManager>();
    }




    public void ActivateMenu()
    {
        minigameEndMenu.SetActive(true);
        SetButtonsInteractable(true);
        Cursor.visible = true; //il cursore diventa visibile
        Cursor.lockState = CursorLockMode.None; //il cursore può muoversi
    }

    public void OnContinueClicked()
    {
        persistentScoreManager.AddScore(progress);
        DataPersistenceManager.instance.SaveGame();
        DataPersistenceManager.instance.GetAllProfilesGamaData();
        SceneManager.LoadSceneAsync("Mappa Principale");
    }



    void SetButtonsInteractable(bool b)
    {
        continueButton.interactable = b;
    }

}
