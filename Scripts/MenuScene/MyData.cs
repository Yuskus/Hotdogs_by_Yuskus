using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class MyData : MonoBehaviour
{
    private int continueGame;
    private int availableLevels;
    private int[] lvlRec;
    private readonly int levelsCount = 50;
    private string path;
    private static GameObject myInstance;
    public int[] LvlRec
    {
        get { return lvlRec; }
        private set { lvlRec = value; }
    }
    public int ContinueGame //55 (обнуление), 87 (подгрузка), 100 (сохранение), 112 (изменение)
    {
        get { return continueGame; }
        set
        {
            if (value <= availableLevels && value < levelsCount) { continueGame = value; }
            else if (value == levelsCount) { RecData.youCompletedTheGame = true; }
        }
    }
    public int AvailableLevels //56 (обнуление), 86 (подгрузка), 97 (сохранение)
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
        path = Path.Combine(Application.persistentDataPath, "MySavedData.dat");
        if (myInstance == null)
        {
            LoadData();
            myInstance = gameObject;
            DontDestroyOnLoad(gameObject);
        }
    }
    public void RecSum(out int sum)
    {
        sum = 0;
        for (int i = 0; i < levelsCount; i++)
        {
            sum += LvlRec[i];
        }
    }
    public void SaveData()
    {
        BinaryFormatter bf = new();
        using FileStream file = File.Create(path);
        SaveMyData data = new(continueGame, availableLevels, LvlRec);
        bf.Serialize(file, data);
    }
    public void LoadData()
    {
        if (File.Exists(path))
        {
            BinaryFormatter bf = new();
            using FileStream file = File.Open(path, FileMode.Open);
            SaveMyData data = (SaveMyData)bf.Deserialize(file);
            (continueGame, availableLevels, LvlRec) = data;
        }
    }
    public void ResetData()
    {
        if (!File.Exists(path))
        {
            File.Delete(path);
            continueGame = 0;
            availableLevels = 0;
            lvlRec = new int[50];
        }
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
