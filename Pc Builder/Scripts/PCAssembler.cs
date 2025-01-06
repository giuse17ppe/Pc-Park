using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PCAssembler : MonoBehaviour
{
    [Header("Componenti a Sinistra")]
    public Image[] componentImages; // Immagini dei componenti (CPU, HDD, RAM, Alimentatore)

    [Header("Immagini del Case")]
    public Sprite[] caseSprites; // Gli stati del case (vuoto, CPU, CPU+HDD, ecc.)
    public Image caseImage; // L'immagine del case da aggiornare

    [Header("Pulsante Inserisci")]
    public Button inserisciButton; // Riferimento al pulsante "Inserisci"
    public TextMeshProUGUI buttonText;

    private int currentStep = 0; // Traccia il numero di componenti inseriti

    void Start()
    {
        // Associa il pulsante "Inserisci" alla funzione InserisciComponente
        inserisciButton.onClick.AddListener(InserisciComponente);


        // Imposta lo stato iniziale del case (vuoto)
        if (caseSprites.Length > 0)
        {
            caseImage.sprite = caseSprites[0];
        }

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void InserisciComponente()
    {
        // Controlla che ci siano componenti da inserire
        if (currentStep < componentImages.Length && currentStep + 1 < caseSprites.Length)
        {
            // 1. Nascondi il componente corrente a sinistra
            componentImages[currentStep].gameObject.SetActive(false);

            // 2. Aggiorna lo sprite del case
            caseImage.sprite = caseSprites[currentStep + 1];

            // 3. Mostra il sottotitolo corrispondente 
            FindObjectOfType<SubtitleManager>().ShowNextSubtitle();

            // 4. Passa al prossimo step
            currentStep++;


        }
        else if (currentStep == 4)
        {
            Debug.Log("Tutti i componenti sono stati inseriti!");
            buttonText.text = "Torna al menu";
            currentStep++;
        }
        else if (currentStep == 5)
        {
            SceneManager.LoadSceneAsync("Mappa Principale");
        }
    }
}