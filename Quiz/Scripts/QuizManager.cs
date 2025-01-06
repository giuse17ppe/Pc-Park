
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class QuizManager : MonoBehaviour
{
    [System.Serializable]
    public class Question
    {
        public string questionText; // Domanda
        public string[] options; // Opzioni per scelta multipla
        public int correctAnswer; // Indice della risposta corretta
        public QuestionType questionType; // Tipo di domanda
    }

    public enum QuestionType
    {
        MultipleChoice, TrueFalse
    }

    public GameObject quizPanel; // Pannello del quiz
    public Question[] questions;
    private int currentQuestionIndex = 0;
    public TextMeshProUGUI questionText;
    public Button[] answerButtons;
    public Button trueButton;
    public Button falseButton;
    public GameObject confirmationPanel;

    // Popup variables
    public GameObject popup; // Riferimento al popup
    public Canvas canvas; // Canvas del popup
    public float popupDuration = 2f; // Durata del popup
    private RectTransform popupTransform;

    void Start()
    {
        confirmationPanel.SetActive(false);
        // Inizializza il popup
        if (popup != null)
        {
            popupTransform = popup.GetComponent<RectTransform>();
            popup.SetActive(false);
        }
        else
        {
            Debug.LogError("Popup non assegnato!");
        }
        DisplayQuestion();
    }

    void DisplayQuestion()
    {
        questionText.text = questions[currentQuestionIndex].questionText;

        // Gestisci domande in base al tipo
        if (questions[currentQuestionIndex].questionType == QuestionType.MultipleChoice)
        {
            // Mostra pulsanti di scelta multipla
            foreach (var button in answerButtons)
                button.gameObject.SetActive(true);

            trueButton.gameObject.SetActive(false);
            falseButton.gameObject.SetActive(false);

            for (int i = 0; i < answerButtons.Length; i++)
            {
                answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = questions[currentQuestionIndex].options[i];
                answerButtons[i].onClick.RemoveAllListeners();
                int index = i; // Per evitare problemi con i listener
                answerButtons[i].onClick.AddListener(() => HandleAnswerClick(index));
            }
        }
        else if (questions[currentQuestionIndex].questionType == QuestionType.TrueFalse)
        {
            // Mostra pulsanti vero/falso
            foreach (var button in answerButtons)
                button.gameObject.SetActive(false);

            trueButton.gameObject.SetActive(true);
            falseButton.gameObject.SetActive(true);

            trueButton.onClick.RemoveAllListeners();
            falseButton.onClick.RemoveAllListeners();

            trueButton.onClick.AddListener(() => HandleAnswerClick(0));  // "True" è l'indice 0
            falseButton.onClick.AddListener(() => HandleAnswerClick(1)); // "False" è l'indice 1
        }
    }

    void HandleAnswerClick(int index)
    {
        // Mostra il popup con il risultato
        if (index == questions[currentQuestionIndex].correctAnswer)
        {
            ShowPopup("Risposta Giusta!");
        }
        else
        {
            ShowPopup("Risposta Sbagliata!");
        }

        // Passa alla domanda successiva
        currentQuestionIndex++;
        if (currentQuestionIndex < questions.Length)
        {
            DisplayQuestion();
        }
        else
        {
            Debug.Log("Quiz Completato!");
            quizPanel.SetActive(false); // Nasconde il pannello del quiz
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void ShowPopup(string message)
    {
        if (popup != null)
        {
            popup.SetActive(true);

            // Aggiorna il testo del popup
            var textComponent = popup.GetComponentInChildren<TextMeshProUGUI>();
            if (textComponent != null)
            {
                textComponent.text = message;
            }
            // Fai seguire il popup al cursore
            StartCoroutine(PopupFollowCursorWithOffset());
        }
    }

    IEnumerator PopupFollowCursorWithOffset()
    {
        float timer = 0f;

        while (timer < popupDuration)
        {
            timer += Time.deltaTime;

            // Calcola la posizione del cursore con offset
            Vector2 cursorPosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle
            (
                canvas.transform as RectTransform,
                Input.mousePosition,
                canvas.worldCamera,
                out cursorPosition
            );

            // Aggiungi un offset (esempio: 110 pixel a destra, 60 pixel in alto)
            Vector2 offset = new Vector2(110f, 60f);
            popupTransform.anchoredPosition = cursorPosition + offset;

            yield return null; // Aspetta il frame successivo
        }

        popup.SetActive(false); // Nascondi il popup
    }

    public void EnterQuiz()
    {
        quizPanel.SetActive(true); // Mostra il pannello del quiz
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void AttemptToExitQuiz()
    {
        if (confirmationPanel != null)
        {
            confirmationPanel.SetActive(true); // Mostra il pannello di conferma
        }
    }

    public void ConfirmExitQuiz()
    {
        Debug.Log("Uscendo dal Quiz... Annullando i progressi.");
        confirmationPanel.SetActive(false); // Nasconde il pannello di conferma
        quizPanel.SetActive(false); // Nasconde il pannello del quiz
        ResetQuiz();
    }

    public void CancelExitQuiz()
    {
        if (confirmationPanel != null)
        {
            confirmationPanel.SetActive(false); // Nasconde il pannello di conferma
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void ResetQuiz()
    {
        currentQuestionIndex = 0; // Resetta il progresso
        DisplayQuestion(); // Torna alla prima domanda
    }
}