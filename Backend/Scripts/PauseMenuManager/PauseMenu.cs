using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PauseMenu : Menu
{

    [Header("Buttons")]
    [SerializeField] private Button riprendiButton;
    [SerializeField] private Button saveButton;
    [SerializeField] private Button exitGameButton;

    [Header("Managers")]
    [SerializeField] private PauseManager pauseManager;

    private string profileId;
    private FileDataHandler dataHandler;


    void Start()
    {

    }





    public void OnRiprendiClicked()
    {
        pauseManager.ChangePauseStatus();
    }

    public void OnSaveGameClicked()
    {
        DataPersistenceManager.instance.SaveGame();
        pauseManager.ChangePauseStatus();
    }


    public void OnExitGameClicked() //per il momento dà qualche problema con i salvataggi: se ritorno al menu principale e avvio un altro salvataggio, si incasina
    {

        pauseManager.ChangePauseStatus();
         //inizializza il profilo selezionato
        DataPersistenceManager.instance.InitializeSelectedProfileId();
        // ricarica il gioco così che i nostri dati corrispondano ai nuovi selezionati
        DataPersistenceManager.instance.LoadGame();

        SceneManager.LoadSceneAsync("MainMenu");

    }

    public void ActivateMenu()
    {
        this.gameObject.SetActive(true);
    }



    public void DeactivateMenu()
    {
        this.gameObject.SetActive(false);
    }


}