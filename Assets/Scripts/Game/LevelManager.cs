using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LevelManager
{
    //Attributes:
    private List<Level> levels;
    private const string filename = "test.json";
    private static LevelManager singleton;

    //Constructor:
    public LevelManager() { levels = new List<Level>(); }

    //Properties:
    public LevelManager Singleton
    {
        get
        {
            return singleton;
        }
    }

    //Methods:
    public bool Load()
    {
        if (!File.Exists(filename)) return false;

        LevelManager data = JsonUtility.FromJson<LevelManager>(File.ReadAllText(filename));
        levels = data.levels;

        return true;
    }

    public void Save()
    {
        File.WriteAllText(filename, JsonUtility.ToJson(this));
    }
}