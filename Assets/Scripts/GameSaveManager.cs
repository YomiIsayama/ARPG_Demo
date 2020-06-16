using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary; //  change data to Binary and save data with binary
using System.Linq;

public class GameSaveManager : MonoBehaviour
{
    public Inventory itemBag;
    void Start()
    {
    }
    public void SaveGame()
    {

        if(!Directory.Exists(Application.persistentDataPath + "/Save_Data"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/Save_Data");
        }


        Debug.Log(Application.persistentDataPath + "/Save_Data");
        saveJson(itemBag);
        foreach (Item i in itemBag.itemList)
        {
            if (i != null)
            {
                saveJson(i);
            }
        }
    }

    public void LoadGame()
    {
        try
        {
            if (Directory.Exists(Application.persistentDataPath + "/Save_Data"))
            {
                loadJson(itemBag);
                foreach(Item i in itemBag.itemList)
                {
                    if (i != null)
                    {
                        loadJson(i);
                    }
                }
            }
        }
        catch (System.Exception)
        {
            //Debug.Log("null file");
        }
        
    }
    private void saveJson<T>(T t)
    {
        BinaryFormatter bf = new BinaryFormatter();//to binary
        FileStream file = File.Create(Application.persistentDataPath + "/Save_Data/"+ t + ".txt");
        var json = JsonUtility.ToJson(t);
        bf.Serialize(file, json);
        file.Close();
        Debug.Log("save" + t);

    }

    private void loadJson<T>(T t)
    {
        FileStream file = File.Open(Application.persistentDataPath + "/Save_Data/" + t + ".txt", FileMode.Open);
        JsonUtility.FromJsonOverwrite((string)new BinaryFormatter().Deserialize(file), t);
        file.Close();
        Debug.Log("Load" + t);
    }
}

