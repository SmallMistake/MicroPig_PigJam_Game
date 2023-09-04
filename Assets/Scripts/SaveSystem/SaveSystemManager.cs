using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveSystemManager : MonoBehaviour
{
    public List<RecordEntry> records = new List<RecordEntry>();

    private void Awake()
    {
        ReadRecords();
    }

    public List<RecordEntry> ReadRecords()
    {
        FileStream file;

        if (File.Exists(Application.persistentDataPath + "/save.dat")) file = File.OpenRead(Application.persistentDataPath + "/save.dat");
        else
        {
            Debug.LogError("File not found");
            records = new List<RecordEntry>();
            return records;
        }

        BinaryFormatter bf = new BinaryFormatter();
        List<RecordEntry> data = (List<RecordEntry>)bf.Deserialize(file);
        records = data;
        file.Close();

        return records;
    }

    public void SaveRecords()
    {
        FileStream file;

        if (File.Exists(Application.persistentDataPath + "/save.dat")) file = File.OpenWrite(Application.persistentDataPath + "/save.dat");
        else file = File.Create(Application.persistentDataPath + "/save.dat");
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, records);
        file.Close();
    }

    public List<RecordEntry> GetRecords()
    {
        return records;
    }
}
