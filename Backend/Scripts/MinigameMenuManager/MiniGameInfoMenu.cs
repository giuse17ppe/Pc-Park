using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEditor.ShaderKeywordFilter;
public class MiniGameInfoMenu : Menu
{
    [Header("Info Menu")]
    [SerializeField] private GameObject infoMenu;
    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] private Button continueButton;
 




    void Awake()
    {
        infoMenu.SetActive(true);
        Time.timeScale = 0;
        Cursor.visible = true; //il cursore diventa visibile
        Cursor.lockState = CursorLockMode.None; //il cursore può muoversi

    }


    public void OnContinueClicked()
    {
        infoMenu.SetActive(false);
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

}
