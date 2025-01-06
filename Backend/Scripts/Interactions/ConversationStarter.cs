using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
using Unity.VisualScripting;
using System.Linq;

public class ConversationStarter : MonoBehaviour, IDataPersistence
{


    [SerializeField] private string id;  //serve per il salvataggio
    [ContextMenu("Generate guid for id")] //genera degli id per il salvataggio

    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    [SerializeField] private List<NPCConversation> myConversation;  //Lista contenente le conversazioni. All'indice 0 ci andrà la conversazione che deve essere letta 
    [SerializeField] private int conversationIndex;
    private bool conversazioneFinita;


    void Update()
    {
        if (ConversationManager.Instance != null && ConversationManager.Instance.IsConversationActive)
        {
            conversazioneFinita = ConversationManager.Instance.GetBool("conversazioneFinita");
        }


        UpdateConversationList();
    }



    void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("Player"))
        {

            if (Input.GetKeyDown(KeyCode.E))  //se premi E vicino a un NPC con trigger a terra avvii il dialogo
            {

                ConversationManager.Instance.StartConversation(myConversation[conversationIndex]);

            }

        }

    }



    //rimuoviamo le conversazioni avvenute, a meno che non rimanga una sola conversazione nella lista (conversazione di fine missione)
    void UpdateConversationList()
    {
        if (conversazioneFinita && myConversation.Count > 1)
        {
            myConversation.RemoveAt(conversationIndex);
        }

    }

    public void LoadData(GameData data)
    {
        data.conversazioni.TryGetValue(id, out conversazioneFinita);

    }

    public void SaveData(GameData data)
    {
        if (data.conversazioni.ContainsKey(id))
        {
            data.conversazioni.Remove(id);
        }

        data.conversazioni.Add(id, conversazioneFinita);

    }

}









