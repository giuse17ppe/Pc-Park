using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class MenuPrincipale : Menu
{
    [Header("Menu Navigation")]
    [SerializeField] private SaveSlotsMenu saveSlotsMenu;
    [SerializeField] private InfoMenu infoMenu;

    [Header("Menu Buttons")]
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button continueGameButton;
    [SerializeField] private Button loadGameButton;
    [SerializeField] private Button infoButton;
    [SerializeField] private Button exitGameButton;



    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        DisableButtonsDependingOnData();
    }

    private void DisableButtonsDependingOnData()
    {
        if (!DataPersistenceManager.instance.HasGameData())
        {
            continueGameButton.interactable = false;
            loadGameButton.interactable = false;
        }
    }



    public void OnNewGameClicked()
    {
        saveSlotsMenu.ActivateMenu(false);
        this.DeactivateMenu();

    }

    public void OnLoadGameClicked()
    {
        saveSlotsMenu.ActivateMenu(true);
        this.DeactivateMenu();
    }

    public void OnContinueGameClicked()
    {
        DisableMenuButtons();

        //Salva il gioco ogni volta prima di caricare una nuova scena
        DataPersistenceManager.instance.SaveGame();
        DataPersistenceManager.instance.GetAllProfilesGamaData();

        //Carica la scena salvata -  il salvataggio è gestito da OnSceneLoaded() nel DataPersistenceManager
        SceneManager.LoadSceneAsync("Mappa Principale");
    }

    public void OnExitGameClicked()
    {
        Application.Quit();
    }

    public void OnInfoClicked()
    {
        infoMenu.ActivateMenu();
        this.DeactivateMenu();
    }

    private void DisableMenuButtons()
    {
        newGameButton.interactable = false;
        continueGameButton.interactable = false;
    }

    public void ActivateMenu()
    {
        this.gameObject.SetActive(true);
        DisableButtonsDependingOnData();
    }

    public void DeactivateMenu()
    {
        this.gameObject.SetActive(false);
    }
}
