using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary; //  change data to Binary and save data with binary

public class GameSaveManager : MonoBehaviour
{
    public Inventory itemBag;

    public void SaveGame()
    {
        Debug.Log(Application.persistentDataPath);

        if(!Directory.Exists(Application.persistentDataPath + "/Save_Data"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/Save_Data");
        }

        BinaryFormatter bf = new BinaryFormatter();//to binary
        FileStream file = File.Create(Application.persistentDataPath + "/Save_Data/gamedata.txt");
        var json = JsonUtility.ToJson(itemBag);
        bf.Serialize(file, json);
        file.Close();
    }

    public void LoadGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        if (File.Exists(Application.persistentDataPath + "/Save_Data/gamedata.txt"))
        {
            FileStream file = File.Open(Application.persistentDataPath + "/Save_Data/gamedata.txt", FileMode.Open);
            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), itemBag);
            file.Close();
        }
    }
}



//    BinaryFormatter formatter2 = new BinaryFormatter();//二进制转化

//    FileStream file2 = File.Create(Application.persistentDataPath + "/game_SaveData/inventory2.txt");// 1 创建存储文件

//    var json2 = JsonUtility.ToJson(thisItem);// 2 能覆写回来

//    formatter.Serialize(file2, json2);//(1,2)
//file2.Close();
/////////////////////////////////////////////
//BinaryFormatter bf2 = new BinaryFormatter();
//if(File.Exists(Application.persistentDataPath + "/game_SaveData/inventory2.txt"))
//{
//FileStream file2 = File.Open(Application.persistentDataPath + "/game_SaveData/inventory2.txt", FileMode.Open);//打开文件

//    JsonUtility.FromJsonOverwrite((string) bf2.Deserialize(file2),thisItem);//反序列化

//file2.Close();

//}
