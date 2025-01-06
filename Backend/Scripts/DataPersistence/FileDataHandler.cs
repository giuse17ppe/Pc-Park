using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Data.Common;

public class FileDataHandler
{
    private string dataDirPath = "";
    private string dataFileName = "";
    private bool useEncryption = false;
    private readonly string encryptionCodeWord = "Easter Egg";  //chiave di decriptazione 

    public FileDataHandler(string dataDirPath, string dataFileName, bool useEncryption)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
        this.useEncryption = useEncryption;
    }

    public GameData Load(string profileId)
    {

        //se il profileId è null, ritorna
        if (profileId == null)
        {
            return null;
        }

        //utilizza Path.Combine per creare il percorso dove salvare, tenendo conto delle differene tra OS
        string fullPath = Path.Combine(dataDirPath, profileId, dataFileName);
        GameData loadedData = null;

        if (File.Exists(fullPath))
        {
            try
            {
                //carica i dati serializzati dal file
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                //decripta i dati  
                if (useEncryption)
                {
                    dataToLoad = EncryptDecrypt(dataToLoad);
                }

                //deserializza i dati da Json all'oggetto C#
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);

            }
            catch (Exception e)
            {
                Debug.LogError("C'è stato un errore nel caicamento dei dati dal file: " + fullPath + "\n" + e);
            }

        }

        return loadedData;

    }

    public void Save(GameData data, string profileId)
    {

        //se il profileId è null, ritorna
        if (profileId == null)
        {
            return;
        }


        //utilizza Path.Combine per tener conto dei diversi separatori di percorso su diversi SO
        string fullPath = Path.Combine(dataDirPath, profileId, dataFileName);


        try
        {
            //crea il percorso della directory se non esiste 
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            //serializza l'oggetto C# dei dati di gioco in Json
            string dataToStore = JsonUtility.ToJson(data, true);

            //cripta i dati 
            if (useEncryption)
            {
                dataToStore = EncryptDecrypt(dataToStore);
            }

            //scrive i dati serializzati nel file
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }

        }
        catch (Exception e)
        {
            Debug.LogError("C'è stato un errore nel salvataggio dei dati sul file: " + fullPath + "\n" + e);
        }
    }

    public void Delete(string profileId)
    {
        //se l'id è nullo, ritorna
        if (profileId == null)
        {
            return;
        }

        string fullPath = Path.Combine(dataDirPath, profileId, dataFileName);
        try
        {
            //ci assicuriamo che il file esista a questo percorso prima di cancellare la directory
            if (File.Exists(fullPath))
            {
                Directory.Delete(Path.GetDirectoryName(fullPath), true);

            }
            else
            {
                Debug.LogWarning("Abbiamo provato a cancellare i dati del profilo, ma non è stato trovato alcun dato nel percorso: " + fullPath);
            }

        }
        catch (Exception e)
        {
            Debug.LogError("Cancellazione dei dati del profilo fallita. ProfileId: " + profileId + "nel percorso: " + fullPath + "\n" + e);
        }


    }
    public Dictionary<string, GameData> LoadAllProfiles()
    {
        Dictionary<string, GameData> profileDictionary = new Dictionary<string, GameData>();

        //itera tra tutti i nomi delle directory tra il percorso della directory coi dati
        IEnumerable<DirectoryInfo> dirInfos = new DirectoryInfo(dataDirPath).EnumerateDirectories();
        foreach (DirectoryInfo dirInfo in dirInfos)
        {
            string profileId = dirInfo.Name;

            //controlla se i file data esistono
            //se non esistono, allora quella cartella non è un profilo e va saltata
            string fullPath = Path.Combine(dataDirPath, profileId, dataFileName);
            if (!File.Exists(fullPath))
            {
                Debug.LogWarning("Directory saltata nel caricamento di tutti i profili perhe non contiene dati: " + profileId);
                continue;
            }

            //carica i dati di gioco per questo profilo e li inserisce nel dizionario
            GameData profileData = Load(profileId);

            //ci assicuriamo che i dati del profilo non siano nulli
            // se sono nulli qualcosa è andato storto e vorremmo saperlo

            if (profileData != null)
            {
                profileDictionary.Add(profileId, profileData);
            }
            else
            {
                Debug.LogError("Abbiamo provato a caricare il profilo ma qualcosa è andato storto. ProfileId: " + profileId);
            }
        }


        return profileDictionary;
    }

    public string GetMostRecentlyUpdatedProfileId()
    {
        string mostRecentProfileId = null;

        Dictionary<string, GameData> profilesGameData = LoadAllProfiles();
        foreach (KeyValuePair<string, GameData> pair in profilesGameData)
        {
            string profileId = pair.Key;
            GameData gameData = pair.Value;

            //salta questo valore se il gamedata è null
            if (gameData == null)
            {
                continue;
            }

            //se questi sono i primi dati che incontriamo, sono i piu recenti
            if (mostRecentProfileId == null)
            {
                mostRecentProfileId = profileId;
            }
            //altrimenti, controlla qual è il piu recente
            else
            {
                DateTime mostRecentDateTime = DateTime.FromBinary(profilesGameData[mostRecentProfileId].lastUpdated);
                DateTime newDateTime = DateTime.FromBinary(gameData.lastUpdated);
                // il valore piu grande sarà il piu recente
                if (newDateTime > mostRecentDateTime)
                {
                    mostRecentProfileId = profileId;
                }
            }

        }

        return mostRecentProfileId;
    }

    private string EncryptDecrypt(string data)
    {
        string modifiedData = "";
        for (int i = 0; i < data.Length; i++)
        {
            modifiedData += (char)(data[i] ^ encryptionCodeWord[i % encryptionCodeWord.Length]);
        }
        return modifiedData;
    }
}
