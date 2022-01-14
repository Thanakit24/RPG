using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public static class SaveSystem
{
    public static void SavePlayer (PlayerController player)
    {
        //TODO use static class in memory to also store PlayerData
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/Player.bn";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/Player.bn";
        Debug.Log($"Finding player save in {path}");
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data =  formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogWarning("Save file not found inside" + path);
            return null;
        }
    }
}
