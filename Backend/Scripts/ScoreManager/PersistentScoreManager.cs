using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PersistentScoreManager : MonoBehaviour, IDataPersistence
{
    public static PersistentScoreManager instance { get; private set; }
    public int score;
    public TextMeshProUGUI scoreText; // Riferimento al testo UI


    void Awake()
    {
        score = 0;
        if (instance != null)
        {
            Debug.Log("Nella scena sono stati trovati piu di un  Persistance Score Manager. Distruggo il piu nuovo");
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        // Controlla se la scena corrente è quella in cui il GameObject deve essere distrutto
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            score = 0;
            Destroy(gameObject);
        }

        if (SceneManager.GetActiveScene().name == "Mappa Principale")
        {
            scoreText.gameObject.SetActive(true);
            scoreText.text = "Punteggio: " + score;

        }
        else
        {
            scoreText.gameObject.SetActive(false);
        }


    }



    //metodi dell'interfaccia IDataPersistance
    public void LoadData(GameData data)
    {
        this.score = data.score;
    }

    public void SaveData(GameData data)
    {
        data.score = this.score;
    }



    public void AddScore(int progress)
    {
        this.score += progress;
        Debug.Log(score);
    }
}
