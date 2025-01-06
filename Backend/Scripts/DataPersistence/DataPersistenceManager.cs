using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("Debugging")]
    [SerializeField] private bool disableDataPersistance = false;
    [SerializeField] private bool initializeDataIfNull = false;
    [SerializeField] private bool overrideSelectedProfile = false;
    [SerializeField] private string testSelectedProfileId = "test";


    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    [SerializeField] private bool useEncryption;

    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;
    private string selectedProfileId = "";
    public static DataPersistenceManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Nella scena sono stati trovati piu di un Data Persistance Manager. Distruggo il piu nuovo");
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);

        if (disableDataPersistance)
        {
            Debug.LogWarning("Data Persistance è attualmente disabilitato!");
        }

        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);

        InitializeSelectedProfileId();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

    }


    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }


    public void ChangeSelectedProfileId(string newProfileId)
    {
        //aggiorna il profilo da usare per salvataggio e caricamento
        this.selectedProfileId = newProfileId;
        //carica il gioco, il che userà quel profilo, aggiornando i nostri dati di gioco di conseguenza
        LoadGame();
    }

    public void DeleteProfileData(string profileId)
    {
        //cancella i dati per questo id di profilo
        dataHandler.Delete(profileId);
        //inizializza il profilo selezionato
        InitializeSelectedProfileId();
        // ricarica il gioco così che i nostri dati corrispondano ai nuovi selezionati
        LoadGame();

    }

    public void InitializeSelectedProfileId()
    {
        this.selectedProfileId = dataHandler.GetMostRecentlyUpdatedProfileId();
        if (overrideSelectedProfile)
        {
            this.selectedProfileId = testSelectedProfileId;
            Debug.LogWarning("Hai effettuato l'override del profile id con un test id: " + testSelectedProfileId);
        }
        Debug.Log(selectedProfileId + " : profilo selezionato");

    }



    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        //effettua il return se il data persistence è disabilitato
        if (disableDataPersistance)
        {
            return;
        }

        //Carica qualsiasi dato salvato da un file usando il data handler
        this.gameData = dataHandler.Load(selectedProfileId);

        //inizia un nuovo gioco se i dati sono null abbiamo configurato di inizializzare i dati per motivi di debugging
        if (this.gameData == null && initializeDataIfNull)
        {
            NewGame();
        }


        //se non è stato trovato alcun dato, non continua
        if (this.gameData == null)
        {

            Debug.Log("Nessun dato trovato. Bisogna creare una nuova partita prima di poterne caricare una!");
            return;
        }

        //invia i dati caricati a tutti gli altri script che li necessitano
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }

    }


    public void SaveGame()
    {
        //effettua il return se il data persistence è disabilitato
        if (disableDataPersistance)
        {
            return;
        }


        //se non abbiamo alcun dato salvato, mostra un warning
        if (this.gameData == null)
        {
            Debug.LogWarning("Nessun dato trovato. Bisogna creare un nuovo gioco prima di poter salvare i dati");
            return;
        }


        //passa i dati ad altri script così da poterli aggiornare
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(gameData);
        }

        //effettua il timestamp dei dati così da capire quale sia l'ultimo
        gameData.lastUpdated = System.DateTime.Now.ToBinary();

        //salva i dati in un file usando il data handler
        dataHandler.Save(gameData, selectedProfileId);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        //FindObjectsOfType prende una booleana aggiuntiva per includere anche i gameobjects inattivi. //FindObjectsOfType<MonoBehaviour>(true).OfType<IDataPersistence>();
        //aggiornato con ByType
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None).OfType<IDataPersistence>();
        //FindObjectsOfType<MonoBehaviour>(true).OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }


    public bool HasGameData()
    {
        return gameData != null;
    }

    public Dictionary<string, GameData> GetAllProfilesGamaData()
    {
        return dataHandler.LoadAllProfiles();
    }
}
