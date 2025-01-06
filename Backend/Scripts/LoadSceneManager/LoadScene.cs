using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DialogueEditor;
using Unity.VisualScripting;

public class LoadScene : MonoBehaviour
{
    // Questo script va associato a ciascun Conversation Trigger degli NPC.
    //Bisognerà inserire nell'inspector il nome del minigame da far partire, e dunque inserire il metodo all'interno del 
    //Dialogue Editor in corrispondenza del dialogo che deve far avviare il gioco

    [Header("Scene")]
    [SerializeField] private string minigame;


    public void LoadMiniGame()
    {
        DataPersistenceManager.instance.SaveGame();
        SceneManager.LoadSceneAsync(minigame);
    }

}
