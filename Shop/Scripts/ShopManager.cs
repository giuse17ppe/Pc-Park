using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Linq;

public class ShopManager : MonoBehaviour, IDataPersistence
{
    public GameObject menu; // Assegna il menu nel pannello Inspector
    public int score;
    private PersistentScoreManager persistentScoreManager;
    public TMP_Text scoreTxt;
    public ShopItemSO[] shopItemSO;
    public ShopTemplate[] shopTemplate;
    public GameObject[] shopTemplateGO;
    public Button[] myPurchaseButton;
    private bool[] stateOfItemsSOPurchased;  //true se è comprato, false se è acquistabile.
    void Start()
    {
        persistentScoreManager = GameObject.Find("PersistentScoreManager").GetComponent<PersistentScoreManager>();


        for (int i = 0; i < shopItemSO.Length; i++)
        {
            shopTemplateGO[i].SetActive(true);

        }

        if (!DataPersistenceManager.instance.HasGameData())
        {
            stateOfItemsSOPurchased = new bool[shopItemSO.Length];
            for (int i = 0; i < shopItemSO.Length; i++)
            {
                stateOfItemsSOPurchased[i] = false;
            }

        }

        LoadTemplates();
    }

    void Update()
    {
        if (menu.activeSelf)
        {
            CheckPurchaseble();
            UpdateScore();
        }
    }


    void UpdateScore()
    {
        scoreTxt.text = "Score: " + persistentScoreManager.score; // Momentaneo per testare
    }

    public void OpenMenu()
    {
        menu.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void CloseMenu()
    {
        menu.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void LoadTemplates()
    {
        for (int i = 0; i < shopItemSO.Length; i++)
        {
            shopTemplate[i].titleTxt.text = shopItemSO[i].title;
            shopTemplate[i].descriptionTxt.text = shopItemSO[i].description;
            shopTemplate[i].costTxt.text = shopItemSO[i].cost.ToString();
        }
    }

    public void CheckPurchaseble()
    {
        for (int i = 0; i < shopItemSO.Length; i++)
        {

            bool isPurchased = stateOfItemsSOPurchased[i];

            if (isPurchased)
            {
                myPurchaseButton[i].interactable = false;
                shopTemplate[i].costTxt.text = "Acquistato";
            }
            else
            {
                myPurchaseButton[i].interactable = persistentScoreManager.score >= shopItemSO[i].cost;
            }
        }
    }

    public void PurchaseItem(int buttonNumber)
    {


        if (persistentScoreManager.score >= shopItemSO[buttonNumber].cost && (!stateOfItemsSOPurchased[buttonNumber]))
        {
            persistentScoreManager.score -= shopItemSO[buttonNumber].cost;
            stateOfItemsSOPurchased[buttonNumber] = true; // Segna come acquistato
            UpdateScore();
            CheckPurchaseble();
            DataPersistenceManager.instance.SaveGame();
            Debug.Log($"Oggetto {shopItemSO[buttonNumber].title} acquistato!");
        }
        else
        {
            Debug.Log("Non hai abbastanza punti o l'oggetto è già stato acquistato.");
        }
    }

    public void LoadData(GameData data)
    {
        this.stateOfItemsSOPurchased = data.stateOfItemsInShop;

    }

    public void SaveData(GameData data)
    {
        data.stateOfItemsInShop = this.stateOfItemsSOPurchased;
    }
}
