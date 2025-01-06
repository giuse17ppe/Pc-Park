using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveSlotsMenu : Menu
{
    [Header("Menu Navigation")]
    [SerializeField] private MenuPrincipale mainMenu;

    [Header("Menu Buttons")]
    [SerializeField] private Button backButton;

    [Header("Confirmation Popup")]
    [SerializeField] private ConfirmationPopupMenu confirmationPopupMenu;

    private SaveSlot[] saveSlots;
    private bool isLoadingGame = false;

    private void Awake()
    {
        saveSlots = this.GetComponentsInChildren<SaveSlot>();
    }


    public void OnSaveSlotClicked(SaveSlot saveSlot)
    {
        //disabilita tutti i bottoni
        DisableMenuButtons();

        //caso - carica gioco
        if (isLoadingGame)
        {
            DataPersistenceManager.instance.ChangeSelectedProfileId(saveSlot.GetProfileId());
            SaveGameAndLoadScene();
        }
        //caso - nuova partita, ma lo slot di salvataggio ha già dati
        else if (saveSlot.hasData)
        {
            confirmationPopupMenu.ActivateMenu(
                "Iniziare una nuova partita su questo slot andrà a sovraccaricare i dati precedenti. Sicuro di voler continuare?",
                //funzione da eseguire se premiamo su si
                () =>
                {
                    DataPersistenceManager.instance.ChangeSelectedProfileId(saveSlot.GetProfileId());
                    DataPersistenceManager.instance.NewGame();
                    SaveGameAndLoadScene();
                },
                //funzione da eseguire se clicchiamo su no
                () =>
                {
                    this.ActivateMenu(isLoadingGame);
                }
            );
        }
        //caso - nuova partita, e lo slot di salvataggio non ha dati
        else
        {
            DataPersistenceManager.instance.ChangeSelectedProfileId(saveSlot.GetProfileId());
            DataPersistenceManager.instance.NewGame();
            SaveGameAndLoadScene();
        }





    }

    private void SaveGameAndLoadScene()
    {
        //salva il gioco ogni volta prima di caricare una nuova scena
        DataPersistenceManager.instance.SaveGame();


        //carica la scena 
        SceneManager.LoadSceneAsync("Mappa Principale");
    }


    public void OnClearClicked(SaveSlot saveSlot)
    {
        DisableMenuButtons();

        confirmationPopupMenu.ActivateMenu(
            "Sei sicuro di voler cancellare i dati?",
            //funzione da eseguire se premiamo si
            () =>
            {
                DataPersistenceManager.instance.DeleteProfileData(saveSlot.GetProfileId());
                ActivateMenu(isLoadingGame);
            },
            //funzione da eseguire se premiamo no
            () =>
            {
                ActivateMenu(isLoadingGame);
            }

        );



    }

    public void OnBackClicked()
    {
        mainMenu.ActivateMenu();
        this.DeactivateMenu();
    }

    public void ActivateMenu(bool isLoadingGame)
    {
        //impostiamo attivo questo menu
        this.gameObject.SetActive(true);

        //imposta la modalità
        this.isLoadingGame = isLoadingGame;

        //carica tutti i profili esistenti
        Dictionary<string, GameData> profilesGameData = DataPersistenceManager.instance.GetAllProfilesGamaData();

        //ci assicuriamo che il bottone indietro è attivo quando attiviamo il menu
        backButton.interactable = true;

        // itera tutti gli slot di salvataggio nell'UI e imposta il contenuto appropriato
        GameObject firstSelected = backButton.gameObject;
        foreach (SaveSlot saveSlot in saveSlots)
        {
            GameData profileData = null;
            profilesGameData.TryGetValue(saveSlot.GetProfileId(), out profileData);
            saveSlot.SetData(profileData);
            if (profileData == null && isLoadingGame)
            {
                saveSlot.SetInteractable(false);
            }
            else
            {
                saveSlot.SetInteractable(true);
                if (firstSelected.Equals(backButton.gameObject))
                {
                    firstSelected = saveSlot.gameObject;
                }
            }
        }
        //imposta il first selected button
        Button firstSelectedButton = firstSelected.GetComponent<Button>();
        this.SetFirstSelected(firstSelectedButton);
    }

    public void DeactivateMenu()
    {
        this.gameObject.SetActive(false);
    }

    private void DisableMenuButtons()
    {
        foreach (SaveSlot saveSlot in saveSlots)
        {
            saveSlot.SetInteractable(false);
        }
        backButton.interactable = false;
    }
}
