using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveSystemManager : MonoBehaviour
{
    public List<RecordEntry> records = new List<RecordEntry>();

    public delegate void onDataSaved();
    public static onDataSaved OnDataSaved;


    private void Awake()
    {
        ReadRecords();
    }

    public List<RecordEntry> ReadRecords()
    {
        FileStream file;

        if (File.Exists(Application.persistentDataPath + "/save.dat"))
        {
            file = File.OpenRead(Application.persistentDataPath + "/save.dat");
            BinaryFormatter bf = new BinaryFormatter();
            List<RecordEntry> data = (List<RecordEntry>)bf.Deserialize(file);
            records = data;
            records = records.OrderByDescending(x => x.level).ToList();
            file.Close();
            
            return records;
        }
        else
        {
            records = new List<RecordEntry>();
            return records;
        }
    }

    public void SaveRecords()
    {
        FileStream file;

        if (File.Exists(Application.persistentDataPath + "/save.dat")) file = File.OpenWrite(Application.persistentDataPath + "/save.dat");
        else file = File.Create(Application.persistentDataPath + "/save.dat");
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, records);
        file.Close();
        OnDataSaved?.Invoke();
    }

    public List<RecordEntry> GetRecords()
    {
        return records;
    }

    public int GetLowestRecord()
    {
        int lowestRecord = 0;
        foreach (RecordEntry entry in records)
        {
            if(entry.level > lowestRecord)
            {
                lowestRecord = entry.level;
            }
        }
        if( records.Count < 10) // Lowest Record is 0 if less than 10 records exist
        {
            lowestRecord = 0;
        }
        return lowestRecord;
    }

    //Add a new Record and then only take the top 10
    public void SaveNewRecord(string playerName, int levelCount)
    {
        records.Add(new RecordEntry(playerName, levelCount));
        records = records.OrderByDescending(x => x.level).ToList();
        records = records.Take(10).ToList();
        SaveRecords();
    }
}
