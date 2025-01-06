using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoMenu : Menu
{
    [Header("Menu Navigation")]
    [SerializeField] private MenuPrincipale mainMenu;
    public void OnBackClicked()
    {
        mainMenu.ActivateMenu();
        this.DeactivateMenu();
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
