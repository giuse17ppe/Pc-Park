using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using DialogueEditor;
using Unity.VisualScripting;


[System.Serializable]
public class GameData
{
    public long lastUpdated;
    public int score;
    public Vector3 playerPosition;
    public Quaternion playerRotation;
    public SerializableDictionary<string, bool> conversazioni;
    public bool[] stateOfItemsInShop; //la grandezza dell'array va modificata ogni volta manualmente nell'inizializzazione






    //i valori nel costruttore saranno i valori di default
    //con cui il gioco inizia quando non ci sono dati salvati
    public GameData()
    {
        //punteggio --> PersistentScoreManager
        this.score = 0;

        //posizione e rotazione del giocatore --> PlayerController
        playerPosition = new Vector3(567.81f, -37.47f, 425.38f);
        playerRotation = Quaternion.Euler(0f, 115f, 0f);

        //conversazioni --> ConversationStarter
        conversazioni = new SerializableDictionary<string, bool>();

        this.stateOfItemsInShop = new bool[3];



    }

    public int GetPercentageComplete()
    {
        //metodo diverso dal tutorial e da aggiornare, per gestire la percentuale di completamento delle missioni
        int totalScore = 3000;

        int percentageCompleted = -1;
        if (totalScore != 0)
        {
            percentageCompleted = (score * 100 / totalScore);
        }
        return percentageCompleted;

    }

}
