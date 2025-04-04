using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class MyData : MonoBehaviour
{
    private int continueGame = 0;
    private int availableLevels = 0;
    private int[] lvlRec = new int[50];
    private readonly int levelsCount = 50;

    private string fileName = "MySavedData.dat";
    private string path;
    private static GameObject myInstance;

    public int[] LvlRec
    {
        get { return lvlRec; }
        private set { lvlRec = value; }
    }

    public int ContinueGame
    {
        get { return continueGame; }
        set
        {
            if (value <= availableLevels && value < levelsCount) { continueGame = value; }
            else if (value == levelsCount) { RecData.youCompletedTheGame = true; }
        }
    }

    public int AvailableLevels
    {
        get { return availableLevels; }
        set
        {
            if (value < levelsCount) { availableLevels = value; }
            else if (value == levelsCount) { RecData.youCompletedTheGame = true; }
        }
    }

    public void Start()
    {
        path = Path.Combine(Application.persistentDataPath, fileName);
        if (myInstance == null)
        {
            CreateData();
            LoadData();
            myInstance = gameObject;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void CreateData()
    {
        if (!File.Exists(path))
        {
            BinaryFormatter bf = new();
            using FileStream file = File.Create(path);
            SaveMyData data = new(0, 0, new int[50]);
            bf.Serialize(file, data);
        }
    }

    public void LoadData()
    {
        if (File.Exists(path))
        {
            BinaryFormatter bf = new();
            using FileStream file = File.Open(path, FileMode.Open);
            SaveMyData data = (SaveMyData)bf.Deserialize(file);
            (ContinueGame, AvailableLevels, LvlRec) = data;
        }
    }

    public void SaveData()
    {
        BinaryFormatter bf = new();
        using FileStream file = File.Create(path);
        SaveMyData data = new(ContinueGame, AvailableLevels, LvlRec);
        bf.Serialize(file, data);
    }

    public void ResetData()
    {
        if (File.Exists(path))
        {
            File.Delete(path);
        }

        CreateData();
    }
}

[Serializable]
public class SaveMyData
{
    public int continueGame;
    public int availableLevels;
    public int[] lvlRec;

    public SaveMyData(int playNow, int playAvailable, int[] rec)
    {
        continueGame = playNow;
        availableLevels = playAvailable;
        lvlRec = rec;
    }
    public void Deconstruct(out int playNow, out int playAvailable, out int[] rec)
    {
        playNow = continueGame;
        playAvailable = availableLevels;
        rec = lvlRec;
    }
}
// MenuButtons (109) - ResetData()
// MenuButtons (46) - LoadData()
// Game () - SaveData()
// 
