using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class ConfirmationPopupMenu : Menu
{

    [Header("Components")]
    [SerializeField] private TextMeshProUGUI displayText;
    [SerializeField] private Button confirmButton;
    [SerializeField] private Button cancelButton;

    public void ActivateMenu(string displayText, UnityAction confirmAction, UnityAction cancelAction)
    {
        this.gameObject.SetActive(true);

        //setta il display text
        this.displayText.text = displayText;

        //rimuove tutti i listeners esistenti per assicurarci che non ce ne siano altri precedentemente attivi
        confirmButton.onClick.RemoveAllListeners();
        cancelButton.onClick.RemoveAllListeners();

        //assegno i listeners onClick()
        confirmButton.onClick.AddListener(() =>
        {

            DeactivateMenu();
            confirmAction();
        });

        cancelButton.onClick.AddListener(() =>
        {
            DeactivateMenu();
            cancelAction();
        });

    }


    private void DeactivateMenu()
    {
        this.gameObject.SetActive(false);
    }
}
