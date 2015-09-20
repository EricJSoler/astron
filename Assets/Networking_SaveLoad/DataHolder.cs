using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class DataHolder : MonoBehaviour
{
    public static DataHolder dataHolderInstance;
    List<int> scenesThatRequireNetworkConnection;
    [System.Serializable]
    struct DataToSave
    {
        public BaseCharacterClass characterClass;
        public string characterPreFab;
        public int lastScene;
    }

    DataToSave myData;
    // Use this for initialization
    void Start()
    {
        if (dataHolderInstance == null) {
            dataHolderInstance = this;
        }
        else
            Destroy(gameObject);
        scenesThatRequireNetworkConnection = new List<int>();
        scenesThatRequireNetworkConnection.Add(3);
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/savedGames.gd");
        bf.Serialize(file, this.myData);
        file.Close();
        Debug.Log("Save Complete");
    }
    public void load()
    {
        if (File.Exists(Application.persistentDataPath + "/savedGames.gd")) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open);
            myData = (DataToSave)bf.Deserialize(file);
            file.Close();
            int lastScene = myData.lastScene;
            if (scenesThatRequireNetworkConnection.Contains(lastScene)) {
                Debug.Log("need to connect to the network before this scene can be played");
            }
            //fix this shit eric
            Application.LoadLevel(2);
                
        }
    }

    public BaseCharacterClass CharacterClass
    {
        get { return myData.characterClass; }
        set { myData.characterClass = value; }
    }

    public string CharacterPrefab
    {
        get { return myData.characterPreFab; }
        set { myData.characterPreFab = value; }
    }

    public int LastScene
    {
        get { return myData.lastScene; }
        set { myData.lastScene = value; }
    }

    
}
