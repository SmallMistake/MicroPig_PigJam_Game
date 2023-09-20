
using System;

[Serializable]
public class RecordEntry
{
    public string playerName;
    public int level;

    public RecordEntry()
    {
        playerName = "------";
    }

    public RecordEntry(string playerName, int levelCount)
    {
        this.playerName = playerName;
        this.level = levelCount;
    }
}
